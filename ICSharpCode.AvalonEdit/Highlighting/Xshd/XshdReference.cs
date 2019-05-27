/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace ICSharpCode.AvalonEdit.Highlighting.Xshd
{
	/// <summary>
	/// A reference to an xshd color, or an inline xshd color.
	/// </summary>
	[Serializable]
	public struct XshdReference<T> : IEquatable<XshdReference<T>> where T : XshdElement
	{
		string referencedDefinition;
		string referencedElement;
		T inlineElement;
		
		/// <summary>
		/// Gets the reference.
		/// </summary>
		public string ReferencedDefinition {
			get { return referencedDefinition; }
		}
		
		/// <summary>
		/// Gets the reference.
		/// </summary>
		public string ReferencedElement {
			get { return referencedElement; }
		}
		
		/// <summary>
		/// Gets the inline element.
		/// </summary>
		public T InlineElement {
			get { return inlineElement; }
		}
		
		/// <summary>
		/// Creates a new XshdReference instance.
		/// </summary>
		public XshdReference(string referencedDefinition, string referencedElement)
		{
			if (referencedElement == null)
				throw new ArgumentNullException("referencedElement");
			this.referencedDefinition = referencedDefinition;
			this.referencedElement = referencedElement;
			this.inlineElement = null;
		}
		
		/// <summary>
		/// Creates a new XshdReference instance.
		/// </summary>
		public XshdReference(T inlineElement)
		{
			if (inlineElement == null)
				throw new ArgumentNullException("inlineElement");
			this.referencedDefinition = null;
			this.referencedElement = null;
			this.inlineElement = inlineElement;
		}
		
		/// <summary>
		/// Applies the visitor to the inline element, if there is any.
		/// </summary>
		public object AcceptVisitor(IXshdVisitor visitor)
		{
			if (inlineElement != null)
				return inlineElement.AcceptVisitor(visitor);
			else
				return null;
		}
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<XshdColorReference>" declaration.
		
		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (obj is XshdReference<T>)
				return Equals((XshdReference<T>)obj); // use Equals method below
			else
				return false;
		}
		
		/// <summary>
		/// Equality operator.
		/// </summary>
		public bool Equals(XshdReference<T> other)
		{
			// add comparisions for all members here
			return this.referencedDefinition == other.referencedDefinition
				&& this.referencedElement == other.referencedElement
				&& this.inlineElement == other.inlineElement;
		}
		
		/// <inheritdoc/>
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return GetHashCode(referencedDefinition) ^ GetHashCode(referencedElement) ^ GetHashCode(inlineElement);
		}
		
		static int GetHashCode(object o)
		{
			return o != null ? o.GetHashCode() : 0;
		}
		
		/// <summary>
		/// Equality operator.
		/// </summary>
		public static bool operator ==(XshdReference<T> left, XshdReference<T> right)
		{
			return left.Equals(right);
		}
		
		/// <summary>
		/// Inequality operator.
		/// </summary>
		public static bool operator !=(XshdReference<T> left, XshdReference<T> right)
		{
			return !left.Equals(right);
		}
		#endregion
	}
}
