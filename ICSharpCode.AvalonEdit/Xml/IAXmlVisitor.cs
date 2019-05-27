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
	/// Visitor for the XML tree
	/// </summary>
	public interface IAXmlVisitor
	{
		/// <summary> Visit RawDocument </summary>
		void VisitDocument(AXmlDocument document);
		
		/// <summary> Visit RawElement </summary>
		void VisitElement(AXmlElement element);
		
		/// <summary> Visit RawTag </summary>
		void VisitTag(AXmlTag tag);
		
		/// <summary> Visit RawAttribute </summary>
		void VisitAttribute(AXmlAttribute attribute);
		
		/// <summary> Visit RawText </summary>
		void VisitText(AXmlText text);
	}
}
