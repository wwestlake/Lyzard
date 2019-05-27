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

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// An enum that specifies the possible Y positions that can be returned by VisualLine.GetVisualPosition.
	/// </summary>
	public enum VisualYPosition
	{
		/// <summary>
		/// Returns the top of the TextLine.
		/// </summary>
		LineTop,
		/// <summary>
		/// Returns the top of the text.
		/// If the line contains inline UI elements larger than the text, TextTop may be below LineTop.
		/// For a line containing regular text (all in the editor's main font), this will be equal to LineTop.
		/// </summary>
		TextTop,
		/// <summary>
		/// Returns the bottom of the TextLine.
		/// </summary>
		LineBottom,
		/// <summary>
		/// The middle between LineTop and LineBottom.
		/// </summary>
		LineMiddle,
		/// <summary>
		/// Returns the bottom of the text. 
		/// If the line contains inline UI elements larger than the text, TextBottom might be above LineBottom.
		/// For a line containing regular text (all in the editor's main font), this will be equal to LineBottom.
		/// </summary>
		TextBottom,
		/// <summary>
		/// The middle between TextTop and TextBottom.
		/// </summary>
		TextMiddle,
		/// <summary>
		/// Returns the baseline of the text.
		/// </summary>
		Baseline
	}
}
