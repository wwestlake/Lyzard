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

namespace ICSharpCode.AvalonEdit.Utils
{
	/// <summary>
	/// A collection where adding and removing items causes a callback.
	/// It is valid for the onAdd callback to throw an exception - this will prevent the new item from
	/// being added to the collection.
	/// </summary>
	sealed class ObserveAddRemoveCollection<T> : Collection<T>
	{
		readonly Action<T> onAdd, onRemove;
		
		public ObserveAddRemoveCollection(Action<T> onAdd, Action<T> onRemove)
		{
			this.onAdd = onAdd;
			this.onRemove = onRemove;
		}
		
		protected override void ClearItems()
		{
			if (onRemove != null) {
				foreach (T val in this)
					onRemove(val);
			}
			base.ClearItems();
		}
		
		protected override void InsertItem(int index, T item)
		{
			if (onAdd != null)
				onAdd(item);
			base.InsertItem(index, item);
		}
		
		protected override void RemoveItem(int index)
		{
			if (onRemove != null)
				onRemove(this[index]);
			base.RemoveItem(index);
		}
		
		protected override void SetItem(int index, T item)
		{
			if (onRemove != null)
				onRemove(this[index]);
			try {
				if (onAdd != null)
					onAdd(item);
			} catch {
				// When adding the new item fails, just remove the old one
				// (we cannot keep the old item since we already successfully called onRemove for it)
				base.RemoveAt(index);
				throw;
			}
			base.SetItem(index, item);
		}
	}
}
