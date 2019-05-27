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
using System.Runtime.Serialization;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// Sets the caret position after interactive mode has finished.
	/// </summary>
	[Serializable]
	public class SnippetCaretElement : SnippetElement
	{
		[OptionalField]
		bool setCaretOnlyIfTextIsSelected;
		
		/// <summary>
		/// Creates a new SnippetCaretElement.
		/// </summary>
		public SnippetCaretElement()
		{
		}
		
		/// <summary>
		/// Creates a new SnippetCaretElement.
		/// </summary>
		/// <param name="setCaretOnlyIfTextIsSelected">
		/// If set to true, the caret is set only when some text was selected.
		/// This is useful when both SnippetCaretElement and SnippetSelectionElement are used in the same snippet.
		/// </param>
		public SnippetCaretElement(bool setCaretOnlyIfTextIsSelected)
		{
			this.setCaretOnlyIfTextIsSelected = setCaretOnlyIfTextIsSelected;
		}
		
		/// <inheritdoc/>
		public override void Insert(InsertionContext context)
		{
			if (!setCaretOnlyIfTextIsSelected || !string.IsNullOrEmpty(context.SelectedText))
				SetCaret(context);
		}
		
		internal static void SetCaret(InsertionContext context)
		{
			TextAnchor pos = context.Document.CreateAnchor(context.InsertionPosition);
			pos.MovementType = AnchorMovementType.BeforeInsertion;
			pos.SurviveDeletion = true;
			context.Deactivated += (sender, e) => {
				if (e.Reason == DeactivateReason.ReturnPressed || e.Reason == DeactivateReason.NoActiveElements) {
					context.TextArea.Caret.Offset = pos.Offset;
				}
			};
		}
	}
}
