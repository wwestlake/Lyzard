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
using System.Windows.Documents;

using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// An element inside a snippet.
	/// </summary>
	[Serializable]
	public abstract class SnippetElement
	{
		/// <summary>
		/// Performs insertion of the snippet.
		/// </summary>
		public abstract void Insert(InsertionContext context);
		
		/// <summary>
		/// Converts the snippet to text, with replaceable fields in italic.
		/// </summary>
		public virtual Inline ToTextRun()
		{
			return null;
		}
	}
}
