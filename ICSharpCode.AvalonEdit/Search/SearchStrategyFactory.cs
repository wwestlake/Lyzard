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
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Provides factory methods for ISearchStrategies.
	/// </summary>
	public class SearchStrategyFactory
	{
		/// <summary>
		/// Creates a default ISearchStrategy with the given parameters.
		/// </summary>
		public static ISearchStrategy Create(string searchPattern, bool ignoreCase, bool matchWholeWords, SearchMode mode)
		{
			if (searchPattern == null)
				throw new ArgumentNullException("searchPattern");
			RegexOptions options = RegexOptions.Compiled | RegexOptions.Multiline;
			if (ignoreCase)
				options |= RegexOptions.IgnoreCase;
			
			switch (mode) {
				case SearchMode.Normal:
					searchPattern = Regex.Escape(searchPattern);
					break;
				case SearchMode.Wildcard:
					searchPattern = ConvertWildcardsToRegex(searchPattern);
					break;
			}
			try {
				Regex pattern = new Regex(searchPattern, options);
				return new RegexSearchStrategy(pattern, matchWholeWords);
			} catch (ArgumentException ex) {
				throw new SearchPatternException(ex.Message, ex);
			}
		}
		
		static string ConvertWildcardsToRegex(string searchPattern)
		{
			if (string.IsNullOrEmpty(searchPattern))
				return "";
			
			StringBuilder builder = new StringBuilder();
			
			foreach (char ch in searchPattern) {
				switch (ch) {
					case '?':
						builder.Append(".");
						break;
					case '*':
						builder.Append(".*");
						break;
					default:
						builder.Append(Regex.Escape(ch.ToString()));
						break;
				}
			}
			
			return builder.ToString();
		}
	}
}
