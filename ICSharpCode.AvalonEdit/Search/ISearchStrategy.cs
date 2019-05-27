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
using System.Runtime.Serialization;

using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Basic interface for search algorithms.
	/// </summary>
	public interface ISearchStrategy : IEquatable<ISearchStrategy>
	{
		/// <summary>
		/// Finds all matches in the given ITextSource and the given range.
		/// </summary>
		/// <remarks>
		/// This method must be implemented thread-safe.
		/// All segments in the result must be within the given range, and they must be returned in order
		/// (e.g. if two results are returned, EndOffset of first result must be less than or equal StartOffset of second result).
		/// </remarks>
		IEnumerable<ISearchResult> FindAll(ITextSource document, int offset, int length);
		
		/// <summary>
		/// Finds the next match in the given ITextSource and the given range.
		/// </summary>
		/// <remarks>This method must be implemented thread-safe.</remarks>
		ISearchResult FindNext(ITextSource document, int offset, int length);
	}
	
	/// <summary>
	/// Represents a search result.
	/// </summary>
	public interface ISearchResult : ISegment
	{
		/// <summary>
		/// Replaces parts of the replacement string with parts from the match. (e.g. $1)
		/// </summary>
		string ReplaceWith(string replacement);
	}
	
	/// <summary>
	/// Defines supported search modes.
	/// </summary>
	public enum SearchMode
	{
		/// <summary>
		/// Standard search
		/// </summary>
		Normal,
		/// <summary>
		/// RegEx search
		/// </summary>
		RegEx,
		/// <summary>
		/// Wildcard search
		/// </summary>
		Wildcard
	}
	
	/// <inheritdoc/>
	public class SearchPatternException : Exception, ISerializable
	{
		/// <inheritdoc/>
		public SearchPatternException()
		{
		}
		
		/// <inheritdoc/>
		public SearchPatternException(string message) : base(message)
		{
		}
		
		/// <inheritdoc/>
		public SearchPatternException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		/// <inheritdoc/>
		protected SearchPatternException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
