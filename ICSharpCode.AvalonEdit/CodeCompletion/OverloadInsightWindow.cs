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
using System.Windows;
using System.Windows.Input;

using ICSharpCode.AvalonEdit.Editing;

namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// Insight window that shows an OverloadViewer.
	/// </summary>
	public class OverloadInsightWindow : InsightWindow
	{
		OverloadViewer overloadViewer = new OverloadViewer();
		
		/// <summary>
		/// Creates a new OverloadInsightWindow.
		/// </summary>
		public OverloadInsightWindow(TextArea textArea) : base(textArea)
		{
			overloadViewer.Margin = new Thickness(2,0,0,0);
			this.Content = overloadViewer;
		}
		
		/// <summary>
		/// Gets/Sets the item provider.
		/// </summary>
		public IOverloadProvider Provider {
			get { return overloadViewer.Provider; }
			set { overloadViewer.Provider = value; }
		}
		
		/// <inheritdoc/>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (!e.Handled && this.Provider.Count > 1) {
				switch (e.Key) {
					case Key.Up:
						e.Handled = true;
						overloadViewer.ChangeIndex(-1);
						break;
					case Key.Down:
						e.Handled = true;
						overloadViewer.ChangeIndex(+1);
						break;
				}
				if (e.Handled) {
					UpdateLayout();
					UpdatePosition();
				}
			}
		}
	}
}
