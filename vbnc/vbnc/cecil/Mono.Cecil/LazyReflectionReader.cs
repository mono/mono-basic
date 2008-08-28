//
// ReflectionReader.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// (C) 2005 - 2007 Jb Evain
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace Mono.Cecil
{

	using System;
	using System.IO;
	using System.Text;

	using Mono.Cecil.Binary;
	using Mono.Cecil.Cil;
	using Mono.Cecil.Metadata;
	using Mono.Cecil.Signatures;

	internal class LazyReflectionReader : ReflectionReader
	{		
		private MetadataResolver m_metaResolver;
		private MetadataExtendedTables m_extTables;		

		public LazyReflectionReader (ModuleDefinition module)
			: base (module) {
			m_extTables = new MetadataExtendedTables (m_tHeap.Tables);
			m_metaResolver = new MetadataResolver (this, m_extTables);
		}

		#region ReflectionReader overriden method
		public override TypeReference GetTypeRefAt (uint rid) {
			TypeReference typeRef = m_typeRefs [rid - 1];
			if (typeRef == null) {
				AddTypeRef (TableReader.GetTypeRefTable (), (int) rid - 1);
				typeRef = m_typeRefs [rid - 1];
			}
			return typeRef;
		}

		public override FieldDefinition GetFieldDefAt (uint rid) {
			FieldTable fldTable = m_tableReader.GetFieldTable ();
			if (m_fields == null) {
				if (fldTable != null)
					m_fields = new FieldDefinition [fldTable.Rows.Count];
				else
					return null;
			}
			FieldDefinition f = m_fields [rid - 1];
			if (f != null)
				return f;
			FieldRow frow = fldTable [(int) rid - 1];
			FieldSig fsig = m_sigReader.GetFieldSig (frow.Signature);
			MetadataToken fieldToken = MetadataToken.FromMetadataRow (TokenType.Field, (int) rid - 1);
			TypeReference declaring = m_metaResolver.ResolveDeclaringType (fieldToken);
			GenericContext context = new GenericContext (declaring);
			FieldDefinition fdef = new FieldDefinition (
				m_root.Streams.StringsHeap [frow.Name],
				GetTypeRefFromSig (fsig.Type, context), frow.Flags);
			fdef.MetadataToken = fieldToken;

			if (fsig.CustomMods.Length > 0)
				fdef.FieldType = GetModifierType (fsig.CustomMods, fdef.FieldType);
			fdef.MetaResolver = this.m_metaResolver;
			m_fields [(int) rid - 1] = fdef;
			return fdef;
		}

		public override MethodDefinition GetMethodDefAt (uint rid) {
			if (m_meths == null)
				m_meths = new MethodDefinition [TableReader.GetMethodTable ().Rows.Count];
			MethodDefinition method = m_meths [rid - 1];
			if (method != null)
				return method;

			MethodRow mRow = (MethodRow) TableReader.GetMethodTable ().Rows [(int) rid - 1];
			MethodDefinition meth = new MethodDefinition (
				m_root.Streams.StringsHeap [mRow.Name],
				mRow.Flags);
			meth.MetaResolver = this.m_metaResolver;
			meth.RVA = mRow.RVA;
			meth.ImplAttributes = mRow.ImplFlags;
			meth.MetadataToken = MetadataToken.FromMetadataRow (TokenType.Method, (int) rid - 1);
			MethodSig msig = m_sigReader.GetMethodDefSig (mRow.Signature);
			meth.CallingConvention = msig.MethCallConv;
			m_meths [rid - 1] = meth;
			return meth;
		}

		public override ParameterDefinition GetParamDefAt (uint rid) {
			ParamTable paramTable = TableReader.GetParamTable ();
			if (m_parameters == null)
				m_parameters = new ParameterDefinition [paramTable.Rows.Count];
			if (m_parameters [(int) rid - 1] != null)
				return m_parameters [(int) rid - 1];
			ParamRow pRow = paramTable [(int) rid - 1];
			int ownerMethodIndex = m_extTables.Parameters [(int) rid - 1].m_methodOwner;
			MethodRow methodRow = TableReader.GetMethodTable () [ownerMethodIndex];
			MethodDefinition methodDef = GetMethodDefAt ((uint)ownerMethodIndex+1);
			MethodDefSig msig = m_sigReader.GetMethodDefSig (methodRow.Signature);
			GenericContext context = new GenericContext (methodDef);

			ParameterDefinition pdef = null;
			if (pRow.Sequence == 0) { // ret type
				pdef = new ParameterDefinition (
					m_root.Streams.StringsHeap [pRow.Name],
					0,
					pRow.Flags,
					null);
				methodDef.ReturnType = GetMethodReturnType (msig, context);
				MethodReturnType mrt = methodDef.ReturnType;
				mrt.Method = methodDef;
				mrt.Parameter = pdef;
				mrt.Parameter.ParameterType = mrt.ReturnType;
			}
			else {
				Param psig = msig.Parameters [(int) pRow.Sequence - 1];
				pdef = BuildParameterDefinition (
					m_root.Streams.StringsHeap [pRow.Name],
					pRow.Sequence, pRow.Flags, psig, context);
				pdef.MetadataToken = MetadataToken.FromMetadataRow (TokenType.Param, (int) rid - 1);
				if (m_metaResolver.HasConstant (pdef.MetadataToken))
					pdef.Constant = m_metaResolver.ResolveConstant (pdef.MetadataToken);
			}
			pdef.Method = methodDef;
			object constant = m_metaResolver.ResolveConstant (pdef.MetadataToken);
			if (constant != null)
				pdef.Constant = constant;
			m_parameters [(int) rid - 1] = pdef;
			m_metaResolver.ResolveCustomAttributes (pdef.MetadataToken, pdef.CustomAttributes);
			return pdef;
		}

		public override GenericParameter GetGenericParameterAt (uint rid) {
			if (m_genericParameters == null)
				m_genericParameters = new GenericParameter [TableReader.GetGenericParamTable ().Rows.Count];
			GenericParameter genParam = m_genericParameters [rid - 1];
			if (genParam != null)
				return genParam;

			GenericParamRow gpRow = TableReader.GetGenericParamTable () [(int) rid - 1];
			IGenericParameterProvider owner;
			if (gpRow.Owner.TokenType == TokenType.Method)
				owner = GetMethodDefAt (gpRow.Owner.RID);
			else if (gpRow.Owner.TokenType == TokenType.TypeDef)
				owner = GetTypeDefAt (gpRow.Owner.RID);
			else
				throw new ReflectionException ("Unknown owner type for generic parameter");

			GenericParameter gp = new GenericParameter (gpRow.Number, owner);
			gp.Attributes = gpRow.Flags;
			gp.Name = MetadataRoot.Streams.StringsHeap [gpRow.Name];
			gp.MetadataToken = MetadataToken.FromMetadataRow (TokenType.GenericParam, (int) rid - 1);
			
			m_genericParameters [rid - 1] = gp;
			return gp;
		}

		public override EventDefinition GetEventDefAt (uint rid) {
			EventDefinition current = base.GetEventDefAt (rid);
			if (current == null) {
				EventRow erow = TableReader.GetEventTable () [(int) rid - 1];
				EventExtRow extRow = m_extTables.Events [(int) rid - 1];
				int ownerRow = extRow.m_declaringType;
				TypeDefinition owner = GetTypeDefAt ((uint) ownerRow + 1);
				current = new EventDefinition (
					m_root.Streams.StringsHeap [erow.Name],
					GetTypeDefOrRef (erow.EventType, new GenericContext (owner)), erow.EventFlags);
				current.MetadataToken = MetadataToken.FromMetadataRow (TokenType.Event, (int) rid - 1);
				current.AddMethod = GetMethodDefAt ((uint) extRow.m_addMethod + 1);
				current.RemoveMethod = GetMethodDefAt ((uint) extRow.m_removeMethod + 1);
				m_metaResolver.ResolveCustomAttributes (current.MetadataToken, current.CustomAttributes);
				m_events [(int) rid - 1] = current;
			}
			return current;
		}

		public override PropertyDefinition GetPropertyDefAt (uint rid) {
			PropertyDefinition current = base.GetPropertyDefAt (rid);
			if (current == null) {
				PropertyRow prow = TableReader.GetPropertyTable () [(int)rid - 1];
				int ownersIndex = m_extTables.Properties[(int)rid - 1].m_declaringType;
				TypeDefinition owner = GetTypeDefAt ((uint) ownersIndex + 1);
				PropertySig psig = m_sigReader.GetPropSig (prow.Type);
				current = new PropertyDefinition (
					m_root.Streams.StringsHeap [prow.Name],
					GetTypeRefFromSig (psig.Type, new GenericContext(owner)),
				prow.Flags);
				current.MetaResolver = this.m_metaResolver;
				current.MetadataToken = MetadataToken.FromMetadataRow (TokenType.Property, (int) rid - 1);

				if (psig.CustomMods != null && psig.CustomMods.Length > 0)
					current.PropertyType = GetModifierType (psig.CustomMods, current.PropertyType);

				int getMethodIdx = m_extTables.Properties [(int) rid - 1].m_getMethod;
				int setMethodIdx = m_extTables.Properties [(int) rid - 1].m_setMethod;
				if (getMethodIdx >= 0)
					current.GetMethod = GetMethodDefAt ((uint) getMethodIdx + 1);
				if (setMethodIdx >= 0)
					current.SetMethod = GetMethodDefAt ((uint) setMethodIdx + 1);				
				m_properties [(int) rid - 1] = current;
			}
			return current;
		}

		public override void VisitTypeDefinitionCollection (TypeDefinitionCollection types) {
			// typedef's collection
			BuildTypesCollection (types);			
			InitSpecsAndRefs ();			
			InitNetstedTypes ();

			ReadClassLayoutInfos ();
			ReadFieldLayoutInfos ();
			//ReadSemantics ();
			ReadExternTypes ();
			ReadMarshalSpecs ();
			ReadEntryPoint ();
			BuildModuleCustomAttributes ();
		}


		private void ReadEntryPoint () {
			if (TableReader.GetMethodTable () == null)
				return;
			uint eprid = CodeReader.GetRid ((int) m_reader.Image.CLIHeader.EntryPointToken);
			if (eprid > 0 && eprid <= TableReader.GetMethodTable().Rows.Count)
				Module.Assembly.EntryPoint = GetMethodDefAt (eprid);
		}


		internal object ReadConstantValue (uint pos, ElementType elemType) {
			return GetConstant (pos, elemType);
		}

		internal byte [] ReadFieldInitialValue (MetadataToken token) {
			FieldDefinition field = GetFieldDefAt (token.RID);
			int size = 0;
			TypeReference fieldType = field.FieldType;
			switch (fieldType.FullName) {
			case Constants.Byte:
			case Constants.SByte:
				size = 1;
				break;
			case Constants.Int16:
			case Constants.UInt16:
			case Constants.Char:
				size = 2;
				break;
			case Constants.Int32:
			case Constants.UInt32:
			case Constants.Single:
				size = 4;
				break;
			case Constants.Int64:
			case Constants.UInt64:
			case Constants.Double:
				size = 8;
				break;
			default:
				fieldType = fieldType.GetOriginalType ();

				TypeDefinition fieldTypeDef = fieldType as TypeDefinition;

				if (fieldTypeDef != null)
					size = (int) fieldTypeDef.ClassSize;
				break;
			}

			if (size > 0 && field.RVA != RVA.Zero) {
				BinaryReader br = m_reader.MetadataReader.GetDataReader (field.RVA);
				return br == null ? new byte [size] : br.ReadBytes (size);
			}
			else
				return new byte [0];
		}

		internal SecurityDeclaration ReadSecurityDecleration (DeclSecurityRow row) {
			return BuildSecurityDeclaration (row);
		}

		internal PInvokeInfo ReadPInvokeInfo (ImplMapRow imRow) {
			if (imRow.MemberForwarded.TokenType == TokenType.Method) { // should always be true
				MethodDefinition meth = GetMethodDefAt (imRow.MemberForwarded.RID);
				return new PInvokeInfo (
					meth, imRow.MappingFlags, MetadataRoot.Streams.StringsHeap [imRow.ImportName],
					Module.ModuleReferences [(int) imRow.ImportScope - 1]);
			}
			return null;
		}

		internal CustomAttribute ReadCustomAttribute (MethodReference ctor, CustomAttrib sig) {
			return BuildCustomAttribute (ctor, null, sig);
		}

		#endregion

		#region private methods taken from AggressiveReflectionReader

		private void BuildTypesCollection (TypeDefinitionCollection types) {
			TypeDefTable typesTable = m_tableReader.GetTypeDefTable ();
			m_typeDefs = new TypeDefinition [typesTable.Rows.Count];
			for (int i = 0; i < typesTable.Rows.Count; i++) {
				TypeDefRow type = typesTable [i];
				TypeDefinition t = new TypeDefinition (
					m_root.Streams.StringsHeap [type.Name],
					m_root.Streams.StringsHeap [type.Namespace],
					type.Flags);
				//t.MetaResolver = this.MetaResolver;
				t.MetadataToken = MetadataToken.FromMetadataRow (TokenType.TypeDef, i);

				m_typeDefs [i] = t;
				t.MetaResolver = this.m_metaResolver;
				if (!IsDeleted (t))
					types.Add (t);				
			}						
		}

		void InitNetstedTypes () {
			// nested types
			if (m_tHeap.HasTable (NestedClassTable.RId)) {
				NestedClassTable nested = m_tableReader.GetNestedClassTable ();
				for (int i = 0; i < nested.Rows.Count; i++) {
					NestedClassRow row = nested [i];

					TypeDefinition parent = GetTypeDefAt (row.EnclosingClass);
					TypeDefinition child = GetTypeDefAt (row.NestedClass);

					if (!IsDeleted (child))
						parent.NestedTypes.Add (child);
				}
			}
		}

		void InitSpecsAndRefs () {
			//TypeSpecs
			if (m_tHeap.HasTable (TypeSpecTable.RId)) {
				TypeSpecTable tsTable = m_tableReader.GetTypeSpecTable ();
				m_typeSpecs = new TypeReference [tsTable.Rows.Count];
			}

			//MethodSpecs
			if (m_tHeap.HasTable (MethodSpecTable.RId)) {
				MethodSpecTable msTable = m_tableReader.GetMethodSpecTable ();
				m_methodSpecs = new GenericInstanceMethod [msTable.Rows.Count];
			}

			//MemberRef
			if (m_tHeap.HasTable (MemberRefTable.RId)) {
				MemberRefTable mrefTable = m_tableReader.GetMemberRefTable ();
				m_memberRefs = new MemberReference [mrefTable.Rows.Count];
			}

			// typeRef
			if (m_tHeap.HasTable (TypeRefTable.RId)) {
				TypeRefTable typesRef = m_tableReader.GetTypeRefTable ();
				m_typeRefs = new TypeReference [typesRef.Rows.Count];
				for (int i = 0; i < typesRef.Rows.Count; i++)
					AddTypeRef (typesRef, i);
			}

			//events
			if (m_tHeap.HasTable (EventTable.RId)) {				
				m_events = new EventDefinition [m_tableReader.GetEventTable ().Rows.Count];
			}

			//properties
			if (m_tHeap.HasTable (PropertyTable.RId)) {
				m_properties = new PropertyDefinition [m_tableReader.GetPropertyTable ().Rows.Count];
			}
		}

		void BuildModuleCustomAttributes () {
			if (!m_tHeap.HasTable (CustomAttributeTable.RId))
				return;

			CustomAttributeTable caTable = m_tableReader.GetCustomAttributeTable ();
			for (int i = 0; i < caTable.Rows.Count; i++) {
				CustomAttributeRow caRow = caTable [i];
				if (caRow.Parent.TokenType == TokenType.Module)
					Module.CustomAttributes.Add ( m_metaResolver.CreateCustomAttribute(caRow) );
				else if (caRow.Parent.TokenType == TokenType.Assembly)
					Module.Assembly.CustomAttributes.Add (m_metaResolver.CreateCustomAttribute (caRow));
			}
		}

		void ReadClassLayoutInfos () {
			if (!m_tHeap.HasTable (ClassLayoutTable.RId))
				return;

			ClassLayoutTable clTable = m_tableReader.GetClassLayoutTable ();
			for (int i = 0; i < clTable.Rows.Count; i++) {
				ClassLayoutRow clRow = clTable [i];
				TypeDefinition type = GetTypeDefAt (clRow.Parent);
				type.PackingSize = clRow.PackingSize;
				type.ClassSize = clRow.ClassSize;
			}
		}

		void ReadFieldLayoutInfos () {
			if (!m_tHeap.HasTable (FieldLayoutTable.RId))
				return;

			FieldLayoutTable flTable = m_tableReader.GetFieldLayoutTable ();
			for (int i = 0; i < flTable.Rows.Count; i++) {
				FieldLayoutRow flRow = flTable [i];
				FieldDefinition field = GetFieldDefAt (flRow.Field);
				field.Offset = flRow.Offset;
			}
		}

		void ReadPInvokeInfos () {
			if (!m_tHeap.HasTable (ImplMapTable.RId))
				return;

			ImplMapTable imTable = m_tableReader.GetImplMapTable ();
			for (int i = 0; i < imTable.Rows.Count; i++) {
				ImplMapRow imRow = imTable [i];
				if (imRow.MemberForwarded.TokenType == TokenType.Method) { // should always be true
					MethodDefinition meth = GetMethodDefAt (imRow.MemberForwarded.RID);
					meth.PInvokeInfo = new PInvokeInfo (
						meth, imRow.MappingFlags, MetadataRoot.Streams.StringsHeap [imRow.ImportName],
						Module.ModuleReferences [(int) imRow.ImportScope - 1]);
				}
			}
		}

		void ReadSemantics () {
			if (!m_tHeap.HasTable (MethodSemanticsTable.RId))
				return;

			MethodSemanticsTable semTable = m_tableReader.GetMethodSemanticsTable ();
			for (int i = 0; i < semTable.Rows.Count; i++) {
				MethodSemanticsRow semRow = semTable [i];
				MethodDefinition semMeth = GetMethodDefAt (semRow.Method);
				semMeth.SemanticsAttributes = semRow.Semantics;
				switch (semRow.Association.TokenType) {
				case TokenType.Event:
					EventDefinition evt = GetEventDefAt (semRow.Association.RID);
					if ((semRow.Semantics & MethodSemanticsAttributes.AddOn) != 0)
						evt.AddMethod = semMeth;
					else if ((semRow.Semantics & MethodSemanticsAttributes.Fire) != 0)
						evt.InvokeMethod = semMeth;
					else if ((semRow.Semantics & MethodSemanticsAttributes.RemoveOn) != 0)
						evt.RemoveMethod = semMeth;
					break;
				case TokenType.Property:
					PropertyDefinition prop = GetPropertyDefAt (semRow.Association.RID);
					if ((semRow.Semantics & MethodSemanticsAttributes.Getter) != 0)
						prop.GetMethod = semMeth;
					else if ((semRow.Semantics & MethodSemanticsAttributes.Setter) != 0)
						prop.SetMethod = semMeth;
					break;
				}
			}
		}				

		void ReadExternTypes () {
			base.VisitExternTypeCollection (Module.ExternTypes);
		}

		void ReadMarshalSpecs () {
			if (!m_tHeap.HasTable (FieldMarshalTable.RId))
				return;

			FieldMarshalTable fmTable = m_tableReader.GetFieldMarshalTable ();
			for (int i = 0; i < fmTable.Rows.Count; i++) {
				FieldMarshalRow fmRow = fmTable [i];

				IHasMarshalSpec owner = null;
				switch (fmRow.Parent.TokenType) {
				case TokenType.Field:
					owner = GetFieldDefAt (fmRow.Parent.RID);
					break;
				case TokenType.Param:
					owner = GetParamDefAt (fmRow.Parent.RID);
					break;
				}

				owner.MarshalSpec = BuildMarshalDesc (
					m_sigReader.GetMarshalSig (fmRow.NativeType), owner);
			}
		}

		#endregion
	}
}
