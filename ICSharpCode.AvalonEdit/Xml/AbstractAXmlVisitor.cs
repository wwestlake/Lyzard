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
	/// Derive from this class to create visitor for the XML tree
	/// </summary>
	public abstract class AbstractAXmlVisitor : IAXmlVisitor
	{
		/// <summary> Visit RawDocument </summary>
		public virtual void VisitDocument(AXmlDocument document)
		{
			foreach(AXmlObject child in document.Children) child.AcceptVisitor(this);
		}
		
		/// <summary> Visit RawElement </summary>
		public virtual void VisitElement(AXmlElement element)
		{
			foreach(AXmlObject child in element.Children) child.AcceptVisitor(this);
		}
		
		/// <summary> Visit RawTag </summary>
		public virtual void VisitTag(AXmlTag tag)
		{
			foreach(AXmlObject child in tag.Children) child.AcceptVisitor(this);
		}
		
		/// <summary> Visit RawAttribute </summary>
		public virtual void VisitAttribute(AXmlAttribute attribute)
		{
			
		}
		
		/// <summary> Visit RawText </summary>
		public virtual void VisitText(AXmlText text)
		{
			
		}
	}
}
