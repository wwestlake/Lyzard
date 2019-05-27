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
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// Inserts the previously selected text at the selection marker.
	/// </summary>
	[Serializable]
	public class SnippetSelectionElement : SnippetElement
	{
		/// <summary>
		/// Gets/Sets the new indentation of the selected text.
		/// </summary>
		public int Indentation { get; set; }
		
		/// <inheritdoc/>
		public override void Insert(InsertionContext context)
		{
			StringBuilder tabString = new StringBuilder();
			
			for (int i = 0; i < Indentation; i++) {
				tabString.Append(context.Tab);
			}
			
			string indent = tabString.ToString();
			
			string text = context.SelectedText.TrimStart(' ', '\t');
			
			text = text.Replace(context.LineTerminator,
			                             context.LineTerminator + indent);
			
			context.Document.Insert(context.InsertionPosition, text);
			context.InsertionPosition += text.Length;
			
			if (string.IsNullOrEmpty(context.SelectedText))
				SnippetCaretElement.SetCaret(context);
		}
	}
}
