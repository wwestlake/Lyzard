﻿/* 
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
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// A popup-like window that is attached to a text segment.
	/// </summary>
	public class InsightWindow : CompletionWindowBase
	{
		static InsightWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(InsightWindow),
			                                         new FrameworkPropertyMetadata(typeof(InsightWindow)));
			AllowsTransparencyProperty.OverrideMetadata(typeof(InsightWindow),
			                                            new FrameworkPropertyMetadata(Boxes.True));
		}
		
		/// <summary>
		/// Creates a new InsightWindow.
		/// </summary>
		public InsightWindow(TextArea textArea) : base(textArea)
		{
			this.CloseAutomatically = true;
			AttachEvents();
		}
		
		/// <inheritdoc/>
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			
			Rect caret = this.TextArea.Caret.CalculateCaretRectangle();
			Point pointOnScreen = this.TextArea.TextView.PointToScreen(caret.Location - this.TextArea.TextView.ScrollOffset);
			Rect workingArea = System.Windows.Forms.Screen.FromPoint(pointOnScreen.ToSystemDrawing()).WorkingArea.ToWpf().TransformFromDevice(this);
			
			MaxHeight = workingArea.Height;
			MaxWidth = Math.Min(workingArea.Width, Math.Max(1000, workingArea.Width * 0.6));
		}
		
		/// <summary>
		/// Gets/Sets whether the insight window should close automatically.
		/// The default value is true.
		/// </summary>
		public bool CloseAutomatically { get; set; }
		
		/// <inheritdoc/>
		protected override bool CloseOnFocusLost {
			get { return this.CloseAutomatically; }
		}
		
		void AttachEvents()
		{
			this.TextArea.Caret.PositionChanged += CaretPositionChanged;
		}
		
		/// <inheritdoc/>
		protected override void DetachEvents()
		{
			this.TextArea.Caret.PositionChanged -= CaretPositionChanged;
			base.DetachEvents();
		}
		
		void CaretPositionChanged(object sender, EventArgs e)
		{
			if (this.CloseAutomatically) {
				int offset = this.TextArea.Caret.Offset;
				if (offset < this.StartOffset || offset > this.EndOffset) {
					Close();
				}
			}
		}
	}
	
	/// <summary>
	/// TemplateSelector for InsightWindow to replace plain string content by a TextBlock with TextWrapping.
	/// </summary>
	internal sealed class InsightWindowTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item is string)
				return (DataTemplate)((FrameworkElement)container).FindResource("TextBlockTemplate");
			
			return null;
		}
	}
}
