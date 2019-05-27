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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace ICSharpCode.AvalonEdit.Snippets
{
	sealed class SnippetInputHandler : TextAreaStackedInputHandler
	{
		readonly InsertionContext context;
		
		public SnippetInputHandler(InsertionContext context)
			: base(context.TextArea)
		{
			this.context = context;
		}
		
		public override void Attach()
		{
			base.Attach();
			
			SelectElement(FindNextEditableElement(-1, false));
		}
		
		public override void Detach()
		{
			base.Detach();
			context.Deactivate(new SnippetEventArgs(DeactivateReason.InputHandlerDetached));
		}
		
		public override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			if (e.Key == Key.Escape) {
				context.Deactivate(new SnippetEventArgs(DeactivateReason.EscapePressed));
				e.Handled = true;
			} else if (e.Key == Key.Return) {
				context.Deactivate(new SnippetEventArgs(DeactivateReason.ReturnPressed));
				e.Handled = true;
			} else if (e.Key == Key.Tab) {
				bool backwards = e.KeyboardDevice.Modifiers == ModifierKeys.Shift;
				SelectElement(FindNextEditableElement(TextArea.Caret.Offset, backwards));
				e.Handled = true;
			}
		}
		
		void SelectElement(IActiveElement element)
		{
			if (element != null) {
				TextArea.Selection = Selection.Create(TextArea, element.Segment);
				TextArea.Caret.Offset = element.Segment.EndOffset;
			}
		}
		
		IActiveElement FindNextEditableElement(int offset, bool backwards)
		{
			IEnumerable<IActiveElement> elements = context.ActiveElements.Where(e => e.IsEditable && e.Segment != null);
			if (backwards) {
				elements = elements.Reverse();
				foreach (IActiveElement element in elements) {
					if (offset > element.Segment.EndOffset)
						return element;
				}
			} else {
				foreach (IActiveElement element in elements) {
					if (offset < element.Segment.Offset)
						return element;
				}
			}
			return elements.FirstOrDefault();
		}
	}
}
