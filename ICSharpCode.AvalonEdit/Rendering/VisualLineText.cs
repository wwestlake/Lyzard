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
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// VisualLineElement that represents a piece of text.
	/// </summary>
	public class VisualLineText : VisualLineElement
	{
		VisualLine parentVisualLine;
		
		/// <summary>
		/// Gets the parent visual line.
		/// </summary>
		public VisualLine ParentVisualLine {
			get { return parentVisualLine; }
		}
		
		/// <summary>
		/// Creates a visual line text element with the specified length.
		/// It uses the <see cref="ITextRunConstructionContext.VisualLine"/> and its
		/// <see cref="VisualLineElement.RelativeTextOffset"/> to find the actual text string.
		/// </summary>
		public VisualLineText(VisualLine parentVisualLine, int length) : base(length, length)
		{
			if (parentVisualLine == null)
				throw new ArgumentNullException("parentVisualLine");
			this.parentVisualLine = parentVisualLine;
		}
		
		/// <summary>
		/// Override this method to control the type of new VisualLineText instances when
		/// the visual line is split due to syntax highlighting.
		/// </summary>
		protected virtual VisualLineText CreateInstance(int length)
		{
			return new VisualLineText(parentVisualLine, length);
		}
		
		/// <inheritdoc/>
		public override TextRun CreateTextRun(int startVisualColumn, ITextRunConstructionContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			
			int relativeOffset = startVisualColumn - VisualColumn;
			StringSegment text = context.GetText(context.VisualLine.FirstDocumentLine.Offset + RelativeTextOffset + relativeOffset, DocumentLength - relativeOffset);
			return new TextCharacters(text.Text, text.Offset, text.Count, this.TextRunProperties);
		}
		
		/// <inheritdoc/>
		public override bool IsWhitespace(int visualColumn)
		{
			int offset = visualColumn - this.VisualColumn + parentVisualLine.FirstDocumentLine.Offset + this.RelativeTextOffset;
			return char.IsWhiteSpace(parentVisualLine.Document.GetCharAt(offset));
		}
		
		/// <inheritdoc/>
		public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int visualColumnLimit, ITextRunConstructionContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			
			int relativeOffset = visualColumnLimit - VisualColumn;
			StringSegment text = context.GetText(context.VisualLine.FirstDocumentLine.Offset + RelativeTextOffset, relativeOffset);
			CharacterBufferRange range = new CharacterBufferRange(text.Text, text.Offset, text.Count);
			return new TextSpan<CultureSpecificCharacterBufferRange>(range.Length, new CultureSpecificCharacterBufferRange(this.TextRunProperties.CultureInfo, range));
		}
		
		/// <inheritdoc/>
		public override bool CanSplit {
			get { return true; }
		}
		
		/// <inheritdoc/>
		public override void Split(int splitVisualColumn, IList<VisualLineElement> elements, int elementIndex)
		{
			if (splitVisualColumn <= VisualColumn || splitVisualColumn >= VisualColumn + VisualLength)
				throw new ArgumentOutOfRangeException("splitVisualColumn", splitVisualColumn, "Value must be between " + (VisualColumn + 1) + " and " + (VisualColumn + VisualLength - 1));
			if (elements == null)
				throw new ArgumentNullException("elements");
			if (elements[elementIndex] != this)
				throw new ArgumentException("Invalid elementIndex - couldn't find this element at the index");
			int relativeSplitPos = splitVisualColumn - VisualColumn;
			VisualLineText splitPart = CreateInstance(DocumentLength - relativeSplitPos);
			SplitHelper(this, splitPart, splitVisualColumn, relativeSplitPos + RelativeTextOffset);
			elements.Insert(elementIndex + 1, splitPart);
		}
		
		/// <inheritdoc/>
		public override int GetRelativeOffset(int visualColumn)
		{
			return this.RelativeTextOffset + visualColumn - this.VisualColumn;
		}
		
		/// <inheritdoc/>
		public override int GetVisualColumn(int relativeTextOffset)
		{
			return this.VisualColumn + relativeTextOffset - this.RelativeTextOffset;
		}
		
		/// <inheritdoc/>
		public override int GetNextCaretPosition(int visualColumn, LogicalDirection direction, CaretPositioningMode mode)
		{
			int textOffset = parentVisualLine.StartOffset + this.RelativeTextOffset;
			int pos = TextUtilities.GetNextCaretPosition(parentVisualLine.Document, textOffset + visualColumn - this.VisualColumn, direction, mode);
			if (pos < textOffset || pos > textOffset + this.DocumentLength)
				return -1;
			else
				return this.VisualColumn + pos - textOffset;
		}
	}
}
