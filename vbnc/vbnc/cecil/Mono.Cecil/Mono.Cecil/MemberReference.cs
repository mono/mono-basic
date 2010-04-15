//
// MemberReference.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// (C) 2005 Jb Evain
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

	using System.Collections;

	using Mono.Cecil.Metadata;

	public abstract class MemberReference : IMemberReference {

		string m_name;
		TypeReference m_decType;
		MetadataToken m_token;
		IDictionary m_annotations;

		public virtual string Name {
			get { return m_name; }
			set { m_name = value; }
		}

		public virtual TypeReference DeclaringType {
			get { return m_decType; }
			set { m_decType = value; }
		}

		public MetadataToken MetadataToken {
			get { return m_token; }
			set { m_token = value; }
		}

		public IDictionary Annotations
		{
			get { return ((IAnnotationProvider) this).Annotations; }
		}

		IDictionary IAnnotationProvider.Annotations {
			get {
				if (m_annotations == null)
					m_annotations = new Hashtable ();
				return m_annotations;
			}
		}

		public MemberReference (string name)
		{
			m_name = name;
		}

		public override string ToString ()
		{
			if (m_decType == null)
				return m_name;

			return string.Concat (m_decType.FullName, "::", m_name);
		}

		public virtual void Accept (IReflectionVisitor visitor)
		{
		}

		internal static TypeReference ResolveType (TypeReference original, GenericParameterCollection parameters, GenericArgumentCollection arguments)
		{
			TypeSpecification spec = original as TypeSpecification;
			ArrayType array = original as ArrayType;
			ReferenceType reference = original as ReferenceType;
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
					return new ReferenceType (resolved);
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

	}
}
