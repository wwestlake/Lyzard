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
using System.Text;

namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// Converts the XML tree back to text.
	/// The text should exactly match the original.
	/// </summary>
	public class PrettyPrintAXmlVisitor: AbstractAXmlVisitor
	{
		StringBuilder sb = new StringBuilder();
		
		/// <summary>
		/// Gets the pretty printed text
		/// </summary>
		public string Output {
			get {
				return sb.ToString();
			}
		}
		
		/// <summary> Create XML text from a document </summary>
		public static string PrettyPrint(AXmlDocument doc)
		{
			PrettyPrintAXmlVisitor visitor = new PrettyPrintAXmlVisitor();
			visitor.VisitDocument(doc);
			return visitor.Output;
		}
		
		/// <summary> Visit RawDocument </summary>
		public override void VisitDocument(AXmlDocument document)
		{
			base.VisitDocument(document);
		}
		
		/// <summary> Visit RawElement </summary>
		public override void VisitElement(AXmlElement element)
		{
			base.VisitElement(element);
		}
		
		/// <summary> Visit RawTag </summary>
		public override void VisitTag(AXmlTag tag)
		{
			sb.Append(tag.OpeningBracket);
			sb.Append(tag.Name);
			base.VisitTag(tag);
			sb.Append(tag.ClosingBracket);
		}
		
		/// <summary> Visit RawAttribute </summary>
		public override void VisitAttribute(AXmlAttribute attribute)
		{
			sb.Append(attribute.Name);
			sb.Append(attribute.EqualsSign);
			sb.Append(attribute.QuotedValue);
		}
		
		/// <summary> Visit RawText </summary>
		public override void VisitText(AXmlText text)
		{
			sb.Append(text.EscapedValue);
		}
	}
}
