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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Search
{
	class RegexSearchStrategy : ISearchStrategy
	{
		readonly Regex searchPattern;
		readonly bool matchWholeWords;
		
		public RegexSearchStrategy(Regex searchPattern, bool matchWholeWords)
		{
			if (searchPattern == null)
				throw new ArgumentNullException("searchPattern");
			this.searchPattern = searchPattern;
			this.matchWholeWords = matchWholeWords;
		}
		
		public IEnumerable<ISearchResult> FindAll(ITextSource document, int offset, int length)
		{
			int endOffset = offset + length;
			foreach (Match result in searchPattern.Matches(document.Text)) {
				int resultEndOffset = result.Length + result.Index;
				if (offset > result.Index || endOffset < resultEndOffset)
					continue;
				if (matchWholeWords && (!IsWordBorder(document, result.Index) || !IsWordBorder(document, resultEndOffset)))
					continue;
				yield return new SearchResult { StartOffset = result.Index, Length = result.Length, Data = result };
			}
		}
		
		static bool IsWordBorder(ITextSource document, int offset)
		{
			return TextUtilities.GetNextCaretPosition(document, offset - 1, LogicalDirection.Forward, CaretPositioningMode.WordBorder) == offset;
		}
		
		public ISearchResult FindNext(ITextSource document, int offset, int length)
		{
			return FindAll(document, offset, length).FirstOrDefault();
		}
		
		public bool Equals(ISearchStrategy other)
		{
			var strategy = other as RegexSearchStrategy;
			return strategy != null &&
				strategy.searchPattern.ToString() == searchPattern.ToString() &&
				strategy.searchPattern.Options == searchPattern.Options &&
				strategy.searchPattern.RightToLeft == searchPattern.RightToLeft;
		}
	}
	
	class SearchResult : TextSegment, ISearchResult
	{
		public Match Data { get; set; }
		
		public string ReplaceWith(string replacement)
		{
			return Data.Result(replacement);
		}
	}
}
