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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Folding
{
	/// <summary>
	/// Helper class used for <see cref="FoldingManager.UpdateFoldings"/>.
	/// </summary>
	public class NewFolding : ISegment
	{
		/// <summary>
		/// Gets/Sets the start offset.
		/// </summary>
		public int StartOffset { get; set; }
		
		/// <summary>
		/// Gets/Sets the end offset.
		/// </summary>
		public int EndOffset { get; set; }
		
		/// <summary>
		/// Gets/Sets the name displayed for the folding.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Gets/Sets whether the folding is closed by default.
		/// </summary>
		public bool DefaultClosed { get; set; }
		
		/// <summary>
		/// Creates a new NewFolding instance.
		/// </summary>
		public NewFolding()
		{
		}
		
		/// <summary>
		/// Creates a new NewFolding instance.
		/// </summary>
		public NewFolding(int start, int end)
		{
			if (!(start <= end))
				throw new ArgumentException("'start' must be less than 'end'");
			this.StartOffset = start;
			this.EndOffset = end;
			this.Name = null;
			this.DefaultClosed = false;
		}
		
		int ISegment.Offset {
			get { return this.StartOffset; }
		}
		
		int ISegment.Length {
			get { return this.EndOffset - this.StartOffset; }
		}
	}
}
