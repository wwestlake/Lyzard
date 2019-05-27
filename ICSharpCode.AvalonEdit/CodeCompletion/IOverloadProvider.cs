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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// Provides the items for the OverloadViewer.
	/// </summary>
	public interface IOverloadProvider : INotifyPropertyChanged
	{
		/// <summary>
		/// Gets/Sets the selected index.
		/// </summary>
		int SelectedIndex { get; set; }
		
		/// <summary>
		/// Gets the number of overloads.
		/// </summary>
		int Count { get; }
		
		/// <summary>
		/// Gets the text 'SelectedIndex of Count'.
		/// </summary>
		string CurrentIndexText { get; }
		
		/// <summary>
		/// Gets the current header.
		/// </summary>
		object CurrentHeader { get; }
		
		/// <summary>
		/// Gets the current content.
		/// </summary>
		object CurrentContent { get; }
	}
}
