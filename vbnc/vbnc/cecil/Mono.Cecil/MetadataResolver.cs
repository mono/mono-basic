using System;
using System.Collections;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Cecil.Binary;
using Mono.Cecil.Signatures;

namespace Mono.Cecil
{
	internal class MetadataResolver
	{
		private LazyReflectionReader m_reader;
		TypeDefExtendedTable m_typeDefsTable;
		MethodDefExtendedTable m_methodDefTable;
		FieldDefExtendedTable m_fieldDefTable;
		PropertyDefExtendedTable m_propertyDefTable;
		ParamDefExtendedTable m_paramDefTable;
		EventDefExtendedTable m_eventDefTable;

		public MetadataResolver (LazyReflectionReader reader, MetadataExtendedTables tables) {
			m_reader = reader;
			m_typeDefsTable = tables.Types;
			m_methodDefTable = tables.Methods;
			m_fieldDefTable = tables.Fields;
			m_propertyDefTable = tables.Properties;
			m_paramDefTable = tables.Parameters;
			m_eventDefTable = tables.Events;
		}		

		#region Lazy helper methods

		public RVA ResolveFieldRVA (MetadataToken token) {
			FieldExtRow fieldRow = m_fieldDefTable [(int) token.RID - 1];
			if (fieldRow == null || fieldRow.m_RVA < 0)
				return RVA.Zero;
			return m_reader.TableReader.GetFieldRVATable () [fieldRow.m_RVA].RVA;		
		}

		public ConstantRow ResolveConstantRow (MetadataToken token) {
			int constIndex = -1;
			switch (token.TokenType) {
			case TokenType.Field:
				if (m_fieldDefTable [(int) token.RID - 1] == null)
					return null;
				constIndex = m_fieldDefTable [(int) token.RID - 1].m_constant;
				break;
			case TokenType.Property:
				if (m_propertyDefTable [(int) token.RID - 1] == null)
					return null;
				constIndex = m_propertyDefTable [(int) token.RID - 1].m_constant;
				break;
			case TokenType.Param:
				if (m_paramDefTable [(int) token.RID - 1] == null)
					return null;
				constIndex = m_paramDefTable [(int) token.RID - 1].m_constant;
				break;
			default:
				return null;
			}
			if (constIndex == -1)
				return null;
			return m_reader.TableReader.GetConstantTable () [constIndex];
		}

		public void ResolveCustomAttributes (MetadataToken token, CustomAttributeCollection col) {

			ArrayList attributes = null;
			switch (token.TokenType) {
			case TokenType.TypeDef:
				if (m_typeDefsTable [(int) token.RID - 1] == null)
					return;
				attributes = m_typeDefsTable [(int) token.RID - 1].m_customAttributes;
				break;
			case TokenType.Method:
				if (m_methodDefTable [(int) token.RID - 1] == null)
					return;
				attributes = m_methodDefTable [(int) token.RID - 1].m_customAttributes;
				break;
			case TokenType.Field:
				if (m_fieldDefTable [(int) token.RID - 1] == null)
					return;
				attributes = m_fieldDefTable [(int) token.RID - 1].m_customAttributes;
				break;
			case TokenType.Property:
				if (m_propertyDefTable [(int) token.RID - 1] == null)
					return;
				attributes = m_propertyDefTable [(int) token.RID - 1].m_customAttributes;
				break;
			case TokenType.Event:
				if (m_eventDefTable [(int) token.RID - 1] == null)
					return;
				attributes = m_eventDefTable [(int) token.RID - 1].m_customAttributes;
				break;
			case TokenType.Param:
				if (m_paramDefTable [(int) token.RID - 1] == null)
					return;
				attributes = m_paramDefTable [(int) token.RID - 1].m_customAttributes;
				break;
			default:
				break;
			}
			if (attributes != null) {
				foreach (int i in attributes) {
					col.Add (CreateCustomAttribute (m_reader.TableReader.GetCustomAttributeTable() [i]));
				}
			}					
		}

		public void ResolveGenericParameters (MetadataToken token, GenericParameterCollection col) {

			ArrayList genParams = null;
			switch (token.TokenType) {
			case TokenType.TypeDef:
				if (m_typeDefsTable [(int) token.RID - 1] == null)
					return;
				genParams = m_typeDefsTable [(int) token.RID - 1].m_genericParameters;
				break;
			case TokenType.Method:
				if (m_methodDefTable [(int) token.RID - 1] == null)
					return;
				genParams = m_methodDefTable [(int) token.RID - 1].m_genericParameters;
				break;
			default:
				break;
			}
			if (genParams != null) {
				foreach (int i in genParams) {
					GenericParameter genParameter = m_reader.GetGenericParameterAt ((uint)i+1);
					col.Add (genParameter);
				}
			}

			//TODO constraints
			//if (constraints != null) {
			//    for (int j = 0; j < constraints.Length; ++j)
			//        genParameter.Constraints.Add (m_reflectionReader.GetTypeDefOrRef (constraints [j], new GenericContext (genParameter)));
			//}
			//col.Add (genParameter);

		}

		public void ResolveParameters (MetadataToken token, ParameterDefinitionCollection col) {

			if (m_methodDefTable [(int) token.RID - 1] == null)
				return;
			int start = m_methodDefTable [(int) token.RID - 1].m_parameterStart;
			int end = m_methodDefTable [(int) token.RID - 1].m_parameterEnd;
			ParamTable paramTable = m_reader.TableReader.GetParamTable ();
			if (paramTable == null || paramTable.Rows.Count <= start)
				return;
			ParamRow pRetRow = m_reader.TableReader.GetParamTable () [start];
			if (pRetRow != null && pRetRow.Sequence == 0) // ret type				
				start++;
			for (int i = start; i < end; ++i) {
				col.Add (m_reader.GetParamDefAt ((uint) (i + 1)));
			}			
		}

		public void ResolveSecurityDeclarations (MetadataToken token, SecurityDeclarationCollection col) {
			ArrayList list = null;
			switch (token.TokenType) {
			case TokenType.TypeDef:
				if (m_typeDefsTable [(int) token.RID - 1] == null)
					return;
				list = m_typeDefsTable [(int) token.RID - 1].m_securityDeclerations;
				break;
			case TokenType.Method:
				if (m_methodDefTable [(int) token.RID - 1] == null)
					return;
				list = m_methodDefTable [(int) token.RID - 1].m_securityDeclerations;
				break;
			}
			if (list != null) {
				foreach (int i in list) {
					DeclSecurityRow row = m_reader.TableReader.GetDeclSecurityTable () [i];
					col.Add (m_reader.ReadSecurityDecleration (row));
				}
			}
		}

		public PInvokeInfo ResolvePInvoke (MetadataToken token) {

			if (m_methodDefTable [(int) token.RID - 1] == null)
				return null;
			int pInvokeIndex = m_methodDefTable [(int) token.RID - 1].m_pInvoke;
			if (pInvokeIndex == -1)
				return null;
			return m_reader.ReadPInvokeInfo (m_reader.TableReader.GetImplMapTable () [pInvokeIndex]);			
		}

		public void ResolveOverrides (MetadataToken token, OverrideCollection overridesCol) {

			if (m_methodDefTable [(int) token.RID - 1] == null)
				return;
			MethodDefinition owner = m_reader.GetMethodDefAt (token.RID);
			ArrayList memberOverrides = m_methodDefTable [(int) token.RID - 1].m_memberOverrides;
			ArrayList methodOverrides = m_methodDefTable [(int) token.RID - 1].m_methodsOverrides;

			if (memberOverrides != null) {
				foreach (int i in memberOverrides) {
					overridesCol.Add ((MethodReference) m_reader.GetMemberRefAt ((uint) i + 1, new GenericContext (owner)));
				}
			}
			if (methodOverrides != null) {
				foreach (int i in methodOverrides) {
					overridesCol.Add (m_reader.GetMethodDefAt ((uint) i + 1));
				}
			}
		}

		public void ResolveInterfaces (MetadataToken token, InterfaceCollection col) {

			if (m_typeDefsTable [(int) token.RID - 1] == null)
				return;
			TypeDefinition owner = m_reader.GetTypeDefAt (token.RID);
			ArrayList list = m_typeDefsTable [(int) token.RID - 1].m_interfaces;
			if (list != null) {
				foreach (MetadataToken t in list) {
					col.Add (m_reader.GetTypeDefOrRef (t, new GenericContext (owner)));
				}
			}			
		}

		public TypeReference ResolveBaseType (MetadataToken token) {
			TypeDefRow typeRow = m_reader.TableReader.GetTypeDefTable() [(int) token.RID - 1];
			if (typeRow.Extends.RID == 0)
				return null;
			TypeReference child = (TypeReference) m_reader.LookupByToken (token);
			return m_reader.GetTypeDefOrRef (typeRow.Extends, new GenericContext (child));
		}

		public void ResolveMethods (MetadataToken token, MethodDefinitionCollection methods, ConstructorCollection ctors) {

			int rowIndex = (int) token.RID - 1;
			int methodStart = (int) m_reader.TableReader.GetTypeDefTable () [rowIndex].MethodList - 1;
			TypeDefTable typesTable = m_reader.TableReader.GetTypeDefTable ();
			MethodTable methodsTable = m_reader.TableReader.GetMethodTable ();
			if (methodsTable == null)
				return;
			int methodEnd = rowIndex == typesTable.Rows.Count - 1 ?
				methodsTable.Rows.Count : (int) typesTable [rowIndex + 1].MethodList - 1;			
			for (int i = methodStart; i < methodEnd; ++i) {
				MethodDefinition method = m_reader.GetMethodDefAt ((uint) i + 1);
				if (method.IsConstructor)
					ctors.Add (method);
				else
					methods.Add (method);
			}			
		}

		public void ResolveFields (MetadataToken token, FieldDefinitionCollection col) {

			int rowIndex = (int) token.RID - 1;
			int fieldsStart = (int) m_reader.TableReader.GetTypeDefTable () [rowIndex].FieldList - 1;			
			TypeDefTable typesTable = m_reader.TableReader.GetTypeDefTable ();
			FieldTable fieldsTable = m_reader.TableReader.GetFieldTable ();
			if (fieldsTable == null)
				return;
			int fieldsEnd = rowIndex == typesTable.Rows.Count - 1 ?
				fieldsTable.Rows.Count : (int) typesTable [rowIndex + 1].FieldList - 1;

			for (int i = fieldsStart; i < fieldsEnd; ++i) {
				col.Add (m_reader.GetFieldDefAt ((uint) i + 1));
			}
		}

		public void ResolveEvents (MetadataToken token, EventDefinitionCollection col) {

			if (m_typeDefsTable [(int) token.RID - 1] == null)
				return;
			int rowIndex = (int) token.RID - 1;
			int eventStart = m_typeDefsTable [rowIndex].m_eventStart;
			int eventEnd = m_typeDefsTable [rowIndex].m_eventEnd;
			for (int i = eventStart; i < eventEnd; ++i) {
				col.Add (m_reader.GetEventDefAt ((uint) i + 1));
			}
		}

		public void ResolveProperties (MetadataToken token, PropertyDefinitionCollection col) {

			if (m_typeDefsTable [(int) token.RID - 1] == null)
				return;
			int rowIndex = (int) token.RID - 1;
			int propStart = m_typeDefsTable [rowIndex].m_propertyStart;
			int propEnd = m_typeDefsTable [rowIndex].m_propertyEnd;
			for (int i = propStart; i < propEnd; ++i) {
				col.Add (m_reader.GetPropertyDefAt ((uint) i + 1));
			}
		}

		public object ResolveConstant (MetadataToken token) {
			ConstantRow csRow = ResolveConstantRow (token);
			if (csRow == null)
				return null;
			return m_reader.ReadConstantValue (csRow.Value, csRow.Type);
		}

		public bool HasConstant (MetadataToken token) {
			ConstantRow csRow = ResolveConstantRow (token);
			return csRow != null;
		}

		public byte [] ResolveFieldInitialValue (MetadataToken token) {
			return m_reader.ReadFieldInitialValue (token);
		}

		public MethodReturnType ResolveMethodReturnType (MethodDefinition mdef) {
			MethodRow mdefRow = m_reader.TableReader.GetMethodTable() [(int) mdef.MetadataToken.RID - 1];
			MethodDefSig mdefSig = m_reader.SigReader.GetMethodDefSig (mdefRow.Signature);
			return m_reader.GetMethodReturnType (mdefSig, new GenericContext (mdef));
		}

		public TypeReference ResolveDeclaringType (MetadataToken token) {
			//MetadataToken declaringToken = new MetadataToken ();
			int declaringRow = -1;
			int rowIndex = (int) token.RID - 1;
			switch (token.TokenType) {
			case TokenType.Method:
				declaringRow = m_methodDefTable [rowIndex].m_declaringType;
				break;
			case TokenType.TypeDef:
				if (m_typeDefsTable [rowIndex] == null)
					return null;
				declaringRow = m_typeDefsTable [rowIndex].m_declaringType;
				break;
			case TokenType.Field:
				declaringRow = m_fieldDefTable [rowIndex].m_declaringType;
				break;
			default:
				throw new NotSupportedException ("not supported token type");
			}
			if (declaringRow < 0)
				return null;
			return m_reader.GetTypeDefAt ((uint) declaringRow + 1);			
		}

		#endregion	
	

		internal CustomAttribute CreateCustomAttribute (CustomAttributeRow caRow) {
			MethodReference ctor = null;
			if (caRow.Type.TokenType == TokenType.Method)
				ctor = m_reader.GetMethodDefAt (caRow.Type.RID);
			else
				ctor = m_reader.GetMemberRefAt (caRow.Type.RID, new GenericContext ()) as MethodReference;

			CustomAttrib ca = m_reader.SigReader.GetCustomAttrib (caRow.Value, ctor);
			CustomAttribute cattr;
			if (!ca.Read) {
				cattr = new CustomAttribute (ctor);
				cattr.Resolved = false;
				cattr.Blob = m_reader.MetadataRoot.Streams.BlobHeap.Read (caRow.Value);
			}
			else
				cattr = m_reader.ReadCustomAttribute (ctor, ca);
			return cattr;
		}
	}
}
