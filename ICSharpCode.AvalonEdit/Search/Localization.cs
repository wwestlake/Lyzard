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
using System.ComponentModel;

namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Holds default texts for buttons and labels in the SearchPanel. Override properties to add other languages.
	/// </summary>
	public class Localization
	{
		/// <summary>
		/// Default: 'Match case'
		/// </summary>
		public virtual string MatchCaseText {
			get { return "Match case"; }
		}
		
		/// <summary>
		/// Default: 'Match whole words'
		/// </summary>
		public virtual string MatchWholeWordsText {
			get { return "Match whole words"; }
		}
		
		
		/// <summary>
		/// Default: 'Use regular expressions'
		/// </summary>
		public virtual string UseRegexText {
			get { return "Use regular expressions"; }
		}
		
		/// <summary>
		/// Default: 'Find next (F3)'
		/// </summary>
		public virtual string FindNextText {
			get { return "Find next (F3)"; }
		}
		
		/// <summary>
		/// Default: 'Find previous (Shift+F3)'
		/// </summary>
		public virtual string FindPreviousText {
			get { return "Find previous (Shift+F3)"; }
		}
		
		/// <summary>
		/// Default: 'Error: '
		/// </summary>
		public virtual string ErrorText {
			get { return "Error: "; }
		}
		
		/// <summary>
		/// Default: 'No matches found!'
		/// </summary>
		public virtual string NoMatchesFoundText {
			get { return "No matches found!"; }
		}
	}
}
