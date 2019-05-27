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
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Editing
{
	/// <summary>
	/// Implementation for <see cref="IReadOnlySectionProvider"/> that stores the segments
	/// in a <see cref="TextSegmentCollection{T}"/>.
	/// </summary>
	public class TextSegmentReadOnlySectionProvider<T> : IReadOnlySectionProvider where T : TextSegment
	{
		readonly TextSegmentCollection<T> segments;
		
		/// <summary>
		/// Gets the collection storing the read-only segments.
		/// </summary>
		public TextSegmentCollection<T> Segments {
			get { return segments; }
		}
		
		/// <summary>
		/// Creates a new TextSegmentReadOnlySectionProvider instance for the specified document.
		/// </summary>
		public TextSegmentReadOnlySectionProvider(TextDocument textDocument)
		{
			segments = new TextSegmentCollection<T>(textDocument);
		}
		
		/// <summary>
		/// Creates a new TextSegmentReadOnlySectionProvider instance using the specified TextSegmentCollection.
		/// </summary>
		public TextSegmentReadOnlySectionProvider(TextSegmentCollection<T> segments)
		{
			if (segments == null)
				throw new ArgumentNullException("segments");
			this.segments = segments;
		}
		
		/// <summary>
		/// Gets whether insertion is possible at the specified offset.
		/// </summary>
		public virtual bool CanInsert(int offset)
		{
			foreach (TextSegment segment in segments.FindSegmentsContaining(offset)) {
				if (segment.StartOffset < offset && offset < segment.EndOffset)
					return false;
			}
			return true;
		}
		
		/// <summary>
		/// Gets the deletable segments inside the given segment.
		/// </summary>
		public virtual IEnumerable<ISegment> GetDeletableSegments(ISegment segment)
		{
			if (segment == null)
				throw new ArgumentNullException("segment");
			
			int readonlyUntil = segment.Offset;
			foreach (TextSegment ts in segments.FindOverlappingSegments(segment)) {
				int start = ts.StartOffset;
				int end = start + ts.Length;
				if (start > readonlyUntil) {
					yield return new SimpleSegment(readonlyUntil, start - readonlyUntil);
				}
				if (end > readonlyUntil) {
					readonlyUntil = end;
				}
			}
			int endOffset = segment.EndOffset;
			if (readonlyUntil < endOffset) {
				yield return new SimpleSegment(readonlyUntil, endOffset - readonlyUntil);
			}
		}
	}
}
