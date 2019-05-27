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
using System.Text.RegularExpressions;

namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// A highlighting span is a region with start+end expression that has a different RuleSet inside
	/// and colors the region.
	/// </summary>
	[Serializable]
	public class HighlightingSpan
	{
		/// <summary>
		/// Gets/Sets the start expression.
		/// </summary>
		public Regex StartExpression { get; set; }
		
		/// <summary>
		/// Gets/Sets the end expression.
		/// </summary>
		public Regex EndExpression { get; set; }
		
		/// <summary>
		/// Gets/Sets the rule set that applies inside this span.
		/// </summary>
		public HighlightingRuleSet RuleSet { get; set; }
		
		/// <summary>
		/// Gets the color used for the text matching the start expression.
		/// </summary>
		public HighlightingColor StartColor { get; set; }
		
		/// <summary>
		/// Gets the color used for the text between start and end.
		/// </summary>
		public HighlightingColor SpanColor { get; set; }
		
		/// <summary>
		/// Gets the color used for the text matching the end expression.
		/// </summary>
		public HighlightingColor EndColor { get; set; }
		
		/// <summary>
		/// Gets/Sets whether the span color includes the start.
		/// The default is <c>false</c>.
		/// </summary>
		public bool SpanColorIncludesStart { get; set; }
		
		/// <summary>
		/// Gets/Sets whether the span color includes the end.
		/// The default is <c>false</c>.
		/// </summary>
		public bool SpanColorIncludesEnd { get; set; }
		
		/// <inheritdoc/>
		public override string ToString()
		{
			return "[" + GetType().Name + " Start=" + StartExpression + ", End=" + EndExpression + "]";
		}
	}
}
