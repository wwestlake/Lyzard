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
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Document
{
	/// <summary>
	/// Contains weak event managers for the TextDocument events.
	/// </summary>
	public static class TextDocumentWeakEventManager
	{
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.UpdateStarted"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class UpdateStarted : WeakEventManagerBase<UpdateStarted, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.UpdateStarted += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.UpdateStarted -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.UpdateFinished"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class UpdateFinished : WeakEventManagerBase<UpdateFinished, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.UpdateFinished += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.UpdateFinished -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.Changing"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class Changing : WeakEventManagerBase<Changing, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.Changing += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.Changing -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.Changed"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class Changed : WeakEventManagerBase<Changed, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.Changed += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.Changed -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.LineCountChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		[Obsolete("The TextDocument.LineCountChanged event will be removed in a future version. Use PropertyChangedEventManager instead.")]
		public sealed class LineCountChanged : WeakEventManagerBase<LineCountChanged, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.LineCountChanged += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.LineCountChanged -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.TextLengthChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		[Obsolete("The TextDocument.TextLengthChanged event will be removed in a future version. Use PropertyChangedEventManager instead.")]
		public sealed class TextLengthChanged : WeakEventManagerBase<TextLengthChanged, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.TextLengthChanged += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.TextLengthChanged -= DeliverEvent;
			}
		}
		
		/// <summary>
		/// Weak event manager for the <see cref="TextDocument.TextChanged"/> event.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public sealed class TextChanged : WeakEventManagerBase<TextChanged, TextDocument>
		{
			/// <inheritdoc/>
			protected override void StartListening(TextDocument source)
			{
				source.TextChanged += DeliverEvent;
			}
			
			/// <inheritdoc/>
			protected override void StopListening(TextDocument source)
			{
				source.TextChanged -= DeliverEvent;
			}
		}
	}
}
