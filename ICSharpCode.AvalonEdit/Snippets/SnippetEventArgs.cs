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

namespace ICSharpCode.AvalonEdit.Snippets
{
	/// <summary>
	/// Provides information about the event that occured during use of snippets.
	/// </summary>
	public class SnippetEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the reason for deactivation.
		/// </summary>
		public DeactivateReason Reason { get; private set; }
		
		/// <summary>
		/// Creates a new SnippetEventArgs object, with a DeactivateReason.
		/// </summary>
		public SnippetEventArgs(DeactivateReason reason)
		{
			this.Reason = reason;
		}
	}
	
	/// <summary>
	/// Describes the reason for deactivation of a <see cref="SnippetElement" />.
	/// </summary>
	public enum DeactivateReason
	{
		/// <summary>
		/// Unknown reason.
		/// </summary>
		Unknown,
		/// <summary>
		/// Snippet was deleted.
		/// </summary>
		Deleted,
		/// <summary>
		/// There are no active elements in the snippet.
		/// </summary>
		NoActiveElements,
		/// <summary>
		/// The SnippetInputHandler was detached.
		/// </summary>
		InputHandlerDetached,
		/// <summary>
		/// Return was pressed by the user.
		/// </summary>
		ReturnPressed,
		/// <summary>
		/// Escape was pressed by the user.
		/// </summary>
		EscapePressed
	}
}
