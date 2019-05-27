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
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Document
{
	/// <summary>
	/// This class stacks the last x operations from the undostack and makes
	/// one undo/redo operation from it.
	/// </summary>
	sealed class UndoOperationGroup : IUndoableOperationWithContext
	{
		IUndoableOperation[] undolist;
		
		public UndoOperationGroup(Deque<IUndoableOperation> stack, int numops)
		{
			if (stack == null)  {
				throw new ArgumentNullException("stack");
			}
			
			Debug.Assert(numops > 0 , "UndoOperationGroup : numops should be > 0");
			Debug.Assert(numops <= stack.Count);
			
			undolist = new IUndoableOperation[numops];
			for (int i = 0; i < numops; ++i) {
				undolist[i] = stack.PopBack();
			}
		}
		
		public void Undo()
		{
			for (int i = 0; i < undolist.Length; ++i) {
				undolist[i].Undo();
			}
		}
		
		public void Undo(UndoStack stack)
		{
			for (int i = 0; i < undolist.Length; ++i) {
				stack.RunUndo(undolist[i]);
			}
		}
		
		public void Redo()
		{
			for (int i = undolist.Length - 1; i >= 0; --i) {
				undolist[i].Redo();
			}
		}
		
		public void Redo(UndoStack stack)
		{
			for (int i = undolist.Length - 1; i >= 0; --i) {
				stack.RunRedo(undolist[i]);
			}
		}
	}
}
