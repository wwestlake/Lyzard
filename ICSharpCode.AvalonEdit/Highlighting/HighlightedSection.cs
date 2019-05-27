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

namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// A text section with syntax highlighting information.
	/// </summary>
	public class HighlightedSection : ISegment
	{
		/// <summary>
		/// Gets/sets the document offset of the section.
		/// </summary>
		public int Offset { get; set; }
		
		/// <summary>
		/// Gets/sets the length of the section.
		/// </summary>
		public int Length { get; set; }
		
		int ISegment.EndOffset {
			get { return this.Offset + this.Length; }
		}
		
		/// <summary>
		/// Gets the highlighting color associated with the highlighted section.
		/// </summary>
		public HighlightingColor Color { get; set; }
	}
}
