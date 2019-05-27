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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// The root object of the XML document
	/// </summary>
	public class AXmlDocument: AXmlContainer
	{
		/// <summary> Parser that produced this document </summary>
		internal AXmlParser Parser { get; set; }
		
		/// <summary> Occurs when object is added to any part of the document </summary>
		public event EventHandler<NotifyCollectionChangedEventArgs> ObjectInserted;
		/// <summary> Occurs when object is removed from any part of the document </summary>
		public event EventHandler<NotifyCollectionChangedEventArgs> ObjectRemoved;
		/// <summary> Occurs before local data of any object in the document changes </summary>
		public event EventHandler<AXmlObjectEventArgs> ObjectChanging;
		/// <summary> Occurs after local data of any object in the document changed </summary>
		public event EventHandler<AXmlObjectEventArgs> ObjectChanged;
		
		internal void OnObjectInserted(int index, AXmlObject obj)
		{
			if (ObjectInserted != null)
				ObjectInserted(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new AXmlObject[] { obj }.ToList(), index));
		}
		
		internal void OnObjectRemoved(int index, AXmlObject obj)
		{
			if (ObjectRemoved != null)
				ObjectRemoved(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new AXmlObject[] { obj }.ToList(), index));
		}
		
		internal void OnObjectChanging(AXmlObject obj)
		{
			if (ObjectChanging != null)
				ObjectChanging(this, new AXmlObjectEventArgs() { Object = obj } );
		}
		
		internal void OnObjectChanged(AXmlObject obj)
		{
			if (ObjectChanged != null)
				ObjectChanged(this, new AXmlObjectEventArgs() { Object = obj } );
		}
		
		/// <inheritdoc/>
		public override void AcceptVisitor(IAXmlVisitor visitor)
		{
			visitor.VisitDocument(this);
		}
		
		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0} Chld:{1}]", base.ToString(), this.Children.Count);
		}
	}
}
