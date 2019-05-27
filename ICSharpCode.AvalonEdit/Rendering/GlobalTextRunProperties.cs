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
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace ICSharpCode.AvalonEdit.Rendering
{
	sealed class GlobalTextRunProperties : TextRunProperties
	{
		internal Typeface typeface;
		internal double fontRenderingEmSize;
		internal Brush foregroundBrush;
		internal Brush backgroundBrush;
		internal System.Globalization.CultureInfo cultureInfo;
		
		public override Typeface Typeface { get { return typeface; } }
		public override double FontRenderingEmSize { get { return fontRenderingEmSize; } }
		public override double FontHintingEmSize { get { return fontRenderingEmSize; } }
		public override TextDecorationCollection TextDecorations { get { return null; } }
		public override Brush ForegroundBrush { get { return foregroundBrush; } }
		public override Brush BackgroundBrush { get { return backgroundBrush; } }
		public override System.Globalization.CultureInfo CultureInfo { get { return cultureInfo; } }
		public override TextEffectCollection TextEffects { get { return null; } }
	}
}
