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
	/// Allows <see cref="VisualLineElementGenerator"/>s, <see cref="IVisualLineTransformer"/>s and
	/// <see cref="IBackgroundRenderer"/>s to be notified when they are added or removed from a text view.
	/// </summary>
	public interface ITextViewConnect
	{
		/// <summary>
		/// Called when added to a text view.
		/// </summary>
		void AddToTextView(TextView textView);
		
		/// <summary>
		/// Called when removed from a text view.
		/// </summary>
		void RemoveFromTextView(TextView textView);
	}
}
