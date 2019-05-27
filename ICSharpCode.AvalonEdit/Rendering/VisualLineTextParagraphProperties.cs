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
using System.Windows.Media.TextFormatting;

namespace ICSharpCode.AvalonEdit.Rendering
{
	sealed class VisualLineTextParagraphProperties : TextParagraphProperties
	{
		internal TextRunProperties defaultTextRunProperties;
		internal TextWrapping textWrapping;
		internal double tabSize;
		internal double indent;
		internal bool firstLineInParagraph;
		
		public override double DefaultIncrementalTab {
			get { return tabSize; }
		}
		
		public override FlowDirection FlowDirection { get { return FlowDirection.LeftToRight; } }
		public override TextAlignment TextAlignment { get { return TextAlignment.Left; } }
		public override double LineHeight { get { return double.NaN; } }
		public override bool FirstLineInParagraph { get { return firstLineInParagraph; } }
		public override TextRunProperties DefaultTextRunProperties { get { return defaultTextRunProperties; } }
		public override TextWrapping TextWrapping { get { return textWrapping; } }
		public override TextMarkerProperties TextMarkerProperties { get { return null; } }
		public override double Indent { get { return indent; } }
	}
}
