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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;

namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// Search commands for AvalonEdit.
	/// </summary>
	public static class SearchCommands
	{
		/// <summary>
		/// Finds the next occurrence in the file.
		/// </summary>
		public static readonly RoutedCommand FindNext = new RoutedCommand(
			"FindNext", typeof(SearchPanel),
			new InputGestureCollection { new KeyGesture(Key.F3) }
		);
		
		/// <summary>
		/// Finds the previous occurrence in the file.
		/// </summary>
		public static readonly RoutedCommand FindPrevious = new RoutedCommand(
			"FindPrevious", typeof(SearchPanel),
			new InputGestureCollection { new KeyGesture(Key.F3, ModifierKeys.Shift) }
		);
		
		/// <summary>
		/// Closes the SearchPanel.
		/// </summary>
		public static readonly RoutedCommand CloseSearchPanel = new RoutedCommand(
			"CloseSearchPanel", typeof(SearchPanel),
			new InputGestureCollection { new KeyGesture(Key.Escape) }
		);
	}
	
	/// <summary>
	/// TextAreaInputHandler that registers all search-related commands.
	/// </summary>
	public class SearchInputHandler : TextAreaInputHandler
	{
		/// <summary>
		/// Creates a new SearchInputHandler and registers the search-related commands.
		/// </summary>
		public SearchInputHandler(TextArea textArea)
			: base(textArea)
		{
			RegisterCommands(this.CommandBindings);
			panel = new SearchPanel();
			panel.Attach(TextArea);
		}
		
		void RegisterCommands(ICollection<CommandBinding> commandBindings)
		{
			commandBindings.Add(new CommandBinding(ApplicationCommands.Find, ExecuteFind));
			commandBindings.Add(new CommandBinding(SearchCommands.FindNext, ExecuteFindNext));
			commandBindings.Add(new CommandBinding(SearchCommands.FindPrevious, ExecuteFindPrevious));
			commandBindings.Add(new CommandBinding(SearchCommands.CloseSearchPanel, ExecuteCloseSearchPanel));
		}
		
		SearchPanel panel;
		
		void ExecuteFind(object sender, ExecutedRoutedEventArgs e)
		{
			panel.Open();
			if (!(TextArea.Selection.IsEmpty || TextArea.Selection.IsMultiline))
				panel.SearchPattern = TextArea.Selection.GetText();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Input, (Action)delegate { panel.Reactivate(); });
		}
		
		void ExecuteFindNext(object sender, ExecutedRoutedEventArgs e)
		{
			panel.FindNext();
		}
		
		void ExecuteFindPrevious(object sender, ExecutedRoutedEventArgs e)
		{
			panel.FindPrevious();
		}
		
		void ExecuteCloseSearchPanel(object sender, ExecutedRoutedEventArgs e)
		{
			panel.Close();
		}
		
		/// <summary>
		/// Fired when SearchOptions are modified inside the SearchPanel.
		/// </summary>
		public event EventHandler<SearchOptionsChangedEventArgs> SearchOptionsChanged {
			add { panel.SearchOptionsChanged += value; }
			remove { panel.SearchOptionsChanged -= value; }
		}
	}
}
