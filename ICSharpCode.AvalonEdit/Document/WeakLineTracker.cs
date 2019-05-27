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

namespace ICSharpCode.AvalonEdit.Document
{
	/// <summary>
	/// Allows registering a line tracker on a TextDocument using a weak reference from the document to the line tracker.
	/// </summary>
	public sealed class WeakLineTracker : ILineTracker
	{
		TextDocument textDocument;
		WeakReference targetObject;
		
		private WeakLineTracker(TextDocument textDocument, ILineTracker targetTracker)
		{
			this.textDocument = textDocument;
			this.targetObject = new WeakReference(targetTracker);
		}
		
		/// <summary>
		/// Registers the <paramref name="targetTracker"/> as line tracker for the <paramref name="textDocument"/>.
		/// A weak reference to the target tracker will be used, and the WeakLineTracker will deregister itself
		/// when the target tracker is garbage collected.
		/// </summary>
		public static WeakLineTracker Register(TextDocument textDocument, ILineTracker targetTracker)
		{
			if (textDocument == null)
				throw new ArgumentNullException("textDocument");
			if (targetTracker == null)
				throw new ArgumentNullException("targetTracker");
			WeakLineTracker wlt = new WeakLineTracker(textDocument, targetTracker);
			textDocument.LineTrackers.Add(wlt);
			return wlt;
		}
		
		/// <summary>
		/// Deregisters the weak line tracker.
		/// </summary>
		public void Deregister()
		{
			if (textDocument != null) {
				textDocument.LineTrackers.Remove(this);
				textDocument = null;
			}
		}
		
		void ILineTracker.BeforeRemoveLine(DocumentLine line)
		{
			ILineTracker targetTracker = targetObject.Target as ILineTracker;
			if (targetTracker != null)
				targetTracker.BeforeRemoveLine(line);
			else
				Deregister();
		}
		
		void ILineTracker.SetLineLength(DocumentLine line, int newTotalLength)
		{
			ILineTracker targetTracker = targetObject.Target as ILineTracker;
			if (targetTracker != null)
				targetTracker.SetLineLength(line, newTotalLength);
			else
				Deregister();
		}
		
		void ILineTracker.LineInserted(DocumentLine insertionPos, DocumentLine newLine)
		{
			ILineTracker targetTracker = targetObject.Target as ILineTracker;
			if (targetTracker != null)
				targetTracker.LineInserted(insertionPos, newLine);
			else
				Deregister();
		}
		
		void ILineTracker.RebuildDocument()
		{
			ILineTracker targetTracker = targetObject.Target as ILineTracker;
			if (targetTracker != null)
				targetTracker.RebuildDocument();
			else
				Deregister();
		}
	}
}
