using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace Lyzard.Collections
{
    public class AveragingQueue : IEnumerable<double>, IEnumerable, ICollection, IReadOnlyCollection<double>
    {
        private Queue<double> _internalQueue = new Queue<double>();
        private int _maxQueueSize;
        private object syncRoot = new object();

        public AveragingQueue(int maxQueueSize)
        {
            _maxQueueSize = maxQueueSize;
        }

        public double Average => _internalQueue.Sum() / Count;

        //
        // Summary:
        //     Removes all objects from the System.Collections.Generic.Queue`1.
        public void Clear() { _internalQueue.Clear(); }
        //
        // Summary:
        //     Determines whether an element is in the System.Collections.Generic.Queue`1.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.Queue`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.Queue`1; otherwise, false.
        public bool Contains(double item) => _internalQueue.Contains(item);
        //
        // Summary:
        //     Removes and returns the object at the beginning of the System.Collections.Generic.Queue`1.
        //
        // Returns:
        //     The object that is removed from the beginning of the System.Collections.Generic.Queue`1.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.Queue`1 is empty.
        public double Dequeue() => _internalQueue.Dequeue();
        //
        // Summary:
        //     Adds an object to the end of the System.Collections.Generic.Queue`1.
        //
        // Parameters:
        //   item:
        //     The object to add to the System.Collections.Generic.Queue`1. The value can be
        //     null for reference types.
        public void Enqueue(double item)
        {
            while (Count >= _maxQueueSize)
            {
                _internalQueue.Dequeue();
            }
            _internalQueue.Enqueue(item);
        }
        //
        // Summary:
        //     Returns the object at the beginning of the System.Collections.Generic.Queue`1
        //     without removing it.
        //
        // Returns:
        //     The object at the beginning of the System.Collections.Generic.Queue`1.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.Queue`1 is empty.
        public double Peek() => _internalQueue.Peek();
        //
        // Summary:
        //     Copies the System.Collections.Generic.Queue`1 elements to a new array.
        //
        // Returns:
        //     A new array containing elements copied from the System.Collections.Generic.Queue`1.
        public double[] ToArray() => _internalQueue.ToArray();
        //
        // Summary:
        //     Sets the capacity to the actual number of elements in the System.Collections.Generic.Queue`1,
        //     if that number is less than 90 percent of current capacity.
        public void TrimExcess() => _internalQueue.TrimExcess();

        public int Count => _internalQueue.Count;

        public bool IsSynchronized => true;

        public object SyncRoot => _internalQueue;

        //
        // Summary:
        //     Copies the System.Collections.Generic.Queue`1 elements to an existing one-dimensional
        //     System.Array, starting at the specified array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.Queue`1. The System.Array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than zero.
        //
        //   T:System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.Queue`1 is greater
        //     than the available space from arrayIndex to the end of the destination array.
        public void CopyTo(Array array, int index) => _internalQueue.CopyTo(array as double[], index);


        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.Generic.Queue`1.
        //
        // Returns:
        //     An System.Collections.Generic.Queue`1.Enumerator for the System.Collections.Generic.Queue`1.
        public IEnumerator<double> GetEnumerator()
        {
            return _internalQueue.GetEnumerator();
        }

        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.Generic.Queue`1.
        //
        // Returns:
        //     An System.Collections.Generic.Queue`1.Enumerator for the System.Collections.Generic.Queue`1.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalQueue.GetEnumerator();
        }
    }
}
