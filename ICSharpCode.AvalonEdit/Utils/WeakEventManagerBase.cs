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
using System.Diagnostics;
using System.Windows;

namespace ICSharpCode.AvalonEdit.Utils
{
	/// <summary>
	/// WeakEventManager with AddListener/RemoveListener and CurrentManager implementation.
	/// Helps implementing the WeakEventManager pattern with less code.
	/// </summary>
	public abstract class WeakEventManagerBase<TManager, TEventSource> : WeakEventManager
		where TManager : WeakEventManagerBase<TManager, TEventSource>, new()
		where TEventSource : class
	{
		/// <summary>
		/// Creates a new WeakEventManagerBase instance.
		/// </summary>
		protected WeakEventManagerBase()
		{
			Debug.Assert(GetType() == typeof(TManager));
		}
		
		/// <summary>
		/// Adds a weak event listener.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static void AddListener(TEventSource source, IWeakEventListener listener)
		{
			CurrentManager.ProtectedAddListener(source, listener);
		}
		
		/// <summary>
		/// Removes a weak event listener.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static void RemoveListener(TEventSource source, IWeakEventListener listener)
		{
			CurrentManager.ProtectedRemoveListener(source, listener);
		}
		
		/// <inheritdoc/>
		protected sealed override void StartListening(object source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			StartListening((TEventSource)source);
		}
		
		/// <inheritdoc/>
		protected sealed override void StopListening(object source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			StopListening((TEventSource)source);
		}
		
		/// <summary>
		/// Attaches the event handler.
		/// </summary>
		protected abstract void StartListening(TEventSource source);
		
		/// <summary>
		/// Detaches the event handler.
		/// </summary>
		protected abstract void StopListening(TEventSource source);
		
		/// <summary>
		/// Gets the current manager.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		protected static TManager CurrentManager {
			get {
				Type managerType = typeof(TManager);
				TManager manager = (TManager)GetCurrentManager(managerType);
				if (manager == null) {
					manager = new TManager();
					SetCurrentManager(managerType, manager);
				}
				return manager;
			}
		}
	}
}
