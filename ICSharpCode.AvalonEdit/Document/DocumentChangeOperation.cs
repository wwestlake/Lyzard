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
using System.Diagnostics;

namespace ICSharpCode.AvalonEdit.Document
{
	/// <summary>
	/// Describes a change to a TextDocument.
	/// </summary>
	sealed class DocumentChangeOperation : IUndoableOperationWithContext
	{
		TextDocument document;
		DocumentChangeEventArgs change;
		
		public DocumentChangeOperation(TextDocument document, DocumentChangeEventArgs change)
		{
			this.document = document;
			this.change = change;
		}
		
		public void Undo(UndoStack stack)
		{
			Debug.Assert(stack.state == UndoStack.StatePlayback);
			stack.RegisterAffectedDocument(document);
			stack.state = UndoStack.StatePlaybackModifyDocument;
			this.Undo();
			stack.state = UndoStack.StatePlayback;
		}
		
		public void Redo(UndoStack stack)
		{
			Debug.Assert(stack.state == UndoStack.StatePlayback);
			stack.RegisterAffectedDocument(document);
			stack.state = UndoStack.StatePlaybackModifyDocument;
			this.Redo();
			stack.state = UndoStack.StatePlayback;
		}
		
		public void Undo()
		{
			OffsetChangeMap map = change.OffsetChangeMapOrNull;
			document.Replace(change.Offset, change.InsertionLength, change.RemovedText, map != null ? map.Invert() : null);
		}
		
		public void Redo()
		{
			document.Replace(change.Offset, change.RemovalLength, change.InsertedText, change.OffsetChangeMapOrNull);
		}
	}
}
