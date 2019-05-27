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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ICSharpCode.AvalonEdit.Xml
{
	/// <summary>
	/// Collection that is publicly read-only and has support 
	/// for adding/removing multiple items at a time.
	/// </summary>
	public class AXmlObjectCollection<T>: Collection<T>, INotifyCollectionChanged
	{
		/// <summary> Occurs when the collection is changed </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		
		/// <summary> Raises <see cref="CollectionChanged"/> event </summary>
		// Do not inherit - it is not called if event is null
		void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (CollectionChanged != null) {
				CollectionChanged(this, e);
			}
		}
		
		/// <inheritdoc/>
		protected override void ClearItems()
		{
			throw new NotSupportedException();
		}
		
		/// <inheritdoc/>
		protected override void InsertItem(int index, T item)
		{
			throw new NotSupportedException();
		}
		
		/// <inheritdoc/>
		protected override void RemoveItem(int index)
		{
			throw new NotSupportedException();
		}
		
		/// <inheritdoc/>
		protected override void SetItem(int index, T item)
		{
			throw new NotSupportedException();
		}
		
		internal void InsertItemAt(int index, T item)
		{
			base.InsertItem(index, item);
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new T[] { item }.ToList(), index));
		}
		
		internal void RemoveItemAt(int index)
		{
			T removed = this[index];
			base.RemoveItem(index);
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new T[] { removed }.ToList(), index));
		}
		
		internal void InsertItemsAt(int index, IList<T> items)
		{
			for(int i = 0; i < items.Count; i++) {
				base.InsertItem(index + i, items[i]);
			}
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)items, index));
		}
		
		internal void RemoveItemsAt(int index, int count)
		{
			List<T> removed = new List<T>();
			for(int i = 0; i < count; i++) {
				removed.Add(this[index]);
				base.RemoveItem(index);
			}
			if (CollectionChanged != null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList)removed, index));
		}
	}
}
