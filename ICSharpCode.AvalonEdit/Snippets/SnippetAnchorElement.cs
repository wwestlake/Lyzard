﻿/* 
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
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// Creates a named anchor that can be accessed by other SnippetElements.
	/// </summary>
	public sealed class SnippetAnchorElement : SnippetElement
	{
		/// <summary>
		/// Gets or sets the name of the anchor.
		/// </summary>
		public string Name { get; private set; }
		
		/// <summary>
		/// Creates a SnippetAnchorElement with the supplied name.
		/// </summary>
		public SnippetAnchorElement(string name)
		{
			this.Name = name;
		}
		
		/// <inheritdoc />
		public override void Insert(InsertionContext context)
		{
			TextAnchor start = context.Document.CreateAnchor(context.InsertionPosition);
			start.MovementType = AnchorMovementType.BeforeInsertion;
			start.SurviveDeletion = true;
			AnchorSegment segment = new AnchorSegment(start, start);
			context.RegisterActiveElement(this, new AnchorElement(segment, Name, context));
		}
	}
	
	/// <summary>
	/// AnchorElement created by SnippetAnchorElement.
	/// </summary>
	public sealed class AnchorElement : IActiveElement
	{
		/// <inheritdoc />
		public bool IsEditable {
			get { return false; }
		}
		
		AnchorSegment segment;
		InsertionContext context;
		
		/// <inheritdoc />
		public ISegment Segment {
			get { return segment; }
		}
		
		/// <summary>
		/// Creates a new AnchorElement.
		/// </summary>
		public AnchorElement(AnchorSegment segment, string name, InsertionContext context)
		{
			this.segment = segment;
			this.context = context;
			this.Name = name;
		}
		
		/// <summary>
		/// Gets or sets the text at the anchor.
		/// </summary>
		public string Text {
			get { return context.Document.GetText(segment); }
			set {
				int offset = segment.Offset;
				int length = segment.Length;
				context.Document.Replace(offset, length, value);
				if (length == 0) {
					// replacing an empty anchor segment with text won't enlarge it, so we have to recreate it
					segment = new AnchorSegment(context.Document, offset, value.Length);
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the anchor.
		/// </summary>
		public string Name { get; private set; }
		
		/// <inheritdoc />
		public void OnInsertionCompleted()
		{
		}
		
		/// <inheritdoc />
		public void Deactivate(SnippetEventArgs e)
		{
		}
	}
}
