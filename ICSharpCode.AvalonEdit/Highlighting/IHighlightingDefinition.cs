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
using System.ComponentModel;

namespace ICSharpCode.AvalonEdit.Highlighting
{
	/// <summary>
	/// A highlighting definition.
	/// </summary>
	[TypeConverter(typeof(HighlightingDefinitionTypeConverter))]
	public interface IHighlightingDefinition
	{
		/// <summary>
		/// Gets the name of the highlighting definition.
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// Gets the main rule set.
		/// </summary>
		HighlightingRuleSet MainRuleSet { get; }
		
		/// <summary>
		/// Gets a rule set by name.
		/// </summary>
		/// <returns>The rule set, or null if it is not found.</returns>
		HighlightingRuleSet GetNamedRuleSet(string name);
		
		/// <summary>
		/// Gets a named highlighting color.
		/// </summary>
		/// <returns>The highlighting color, or null if it is not found.</returns>
		HighlightingColor GetNamedColor(string name);
		
		/// <summary>
		/// Gets the list of named highlighting colors.
		/// </summary>
		IEnumerable<HighlightingColor> NamedHighlightingColors { get; }
	}
	
	/// <summary>
	/// Extension of IHighlightingDefinition to avoid breaking changes in the API.
	/// </summary>
	public interface IHighlightingDefinition2 : IHighlightingDefinition
	{
		/// <summary>
		/// Gets the list of properties.
		/// </summary>
		IDictionary<string, string> Properties { get; }
	}
}
