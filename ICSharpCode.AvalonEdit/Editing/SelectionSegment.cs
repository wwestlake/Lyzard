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
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Editing
{
	/// <summary>
	/// Represents a selected segment.
	/// </summary>
	public class SelectionSegment : ISegment
	{
		readonly int startOffset, endOffset;
		readonly int startVC, endVC;
		
		/// <summary>
		/// Creates a SelectionSegment from two offsets.
		/// </summary>
		public SelectionSegment(int startOffset, int endOffset)
		{
			this.startOffset = Math.Min(startOffset, endOffset);
			this.endOffset = Math.Max(startOffset, endOffset);
			this.startVC = this.endVC = -1;
		}
		
		/// <summary>
		/// Creates a SelectionSegment from two offsets and visual columns.
		/// </summary>
		public SelectionSegment(int startOffset, int startVC, int endOffset, int endVC)
		{
			if (startOffset < endOffset || (startOffset == endOffset && startVC <= endVC)) {
				this.startOffset = startOffset;
				this.startVC = startVC;
				this.endOffset = endOffset;
				this.endVC = endVC;
			} else {
				this.startOffset = endOffset;
				this.startVC = endVC;
				this.endOffset = startOffset;
				this.endVC = startVC;
			}
		}
		
		/// <summary>
		/// Gets the start offset.
		/// </summary>
		public int StartOffset {
			get { return startOffset; }
		}
		
		/// <summary>
		/// Gets the end offset.
		/// </summary>
		public int EndOffset {
			get { return endOffset; }
		}
		
		/// <summary>
		/// Gets the start visual column.
		/// </summary>
		public int StartVisualColumn {
			get { return startVC; }
		}
		
		/// <summary>
		/// Gets the end visual column.
		/// </summary>
		public int EndVisualColumn {
			get { return endVC; }
		}
		
		/// <inheritdoc/>
		int ISegment.Offset {
			get { return startOffset; }
		}
		
		/// <inheritdoc/>
		public int Length {
			get { return endOffset - startOffset; }
		}
		
		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Format("[SelectionSegment StartOffset={0}, EndOffset={1}, StartVC={2}, EndVC={3}]", startOffset, endOffset, startVC, endVC);
		}
	}
}
