//
// MemberReference.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2011 Jb Evain
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

namespace Mono.Cecil {

	public abstract class MemberReference : IMetadataTokenProvider {

		string name;
		TypeReference declaring_type;
		System.Collections.IDictionary m_annotations;

		internal MetadataToken token;

		internal static TypeReference ResolveType (TypeReference original, Mono.Collections.Generic.Collection<GenericParameter> parameters, Mono.Collections.Generic.Collection<TypeReference> arguments)
		{
			TypeSpecification spec = original as TypeSpecification;
			ArrayType array = original as ArrayType;
			ByReferenceType reference = original as ByReferenceType;
			GenericInstanceType genericType = original as GenericInstanceType;

			if (parameters.Count != arguments.Count)
				throw new System.ArgumentException ("Parameters and Arguments must have the same number of elements.");

			if (spec != null) {
				TypeReference resolved = ResolveType (spec.ElementType, parameters, arguments);

				if (genericType != null) {
					GenericInstanceType result = new GenericInstanceType (genericType.ElementType);
					bool found;
					for (int i = 0; i < genericType.ElementType.GenericParameters.Count; i++) {
						found = false;
						for (int k = 0; k < parameters.Count; k++) {
							if (genericType.ElementType.GenericParameters [i].Name == parameters [k].Name) {
								found = true;
								result.GenericArguments.Add (arguments [k]);
								break;
							}
						}
						if (!found)
							result.GenericArguments.Add (genericType.ElementType.GenericParameters [i]);
					}
					return result;
				}

				if (resolved == spec.ElementType)
					return spec;

				if (array != null) {
					return new ArrayType (resolved, array.Dimensions.Count);
				} else if (reference != null) {
					return new ByReferenceType (resolved);
				} else {
					throw new System.NotImplementedException ();
				}
			} else {
				for (int i = 0; i < parameters.Count; i++) {
					if (parameters [i] == original) {
						return arguments [i];
					}
				}
				return original;
			}
		}

		public System.Collections.IDictionary Annotations {
			get {
				if (m_annotations == null)
					m_annotations = new System.Collections.Hashtable ();
				return m_annotations;
			}
		}

		public virtual string Name {
			get { return name; }
			set { name = value; }
		}

		public abstract string FullName {
			get;
		}

		public virtual TypeReference DeclaringType {
			get { return declaring_type; }
			set { declaring_type = value; }
		}

		public MetadataToken MetadataToken {
			get { return token; }
			set { token = value; }
		}

		internal bool HasImage {
			get {
				var module = Module;
				if (module == null)
					return false;

				return module.HasImage;
			}
		}

		public virtual ModuleDefinition Module {
			get { return declaring_type != null ? declaring_type.Module : null; }
		}

		public virtual bool IsDefinition {
			get { return false; }
		}

		public virtual bool ContainsGenericParameter {
			get { return declaring_type != null && declaring_type.ContainsGenericParameter; }
		}

		internal MemberReference ()
		{
		}

		internal MemberReference (string name)
		{
			this.name = name ?? string.Empty;
		}

		internal string MemberFullName ()
		{
			if (declaring_type == null)
				return name;

			return declaring_type.FullName + "::" + name;
		}

		public override string ToString ()
		{
			return FullName;
		}
	}
}
