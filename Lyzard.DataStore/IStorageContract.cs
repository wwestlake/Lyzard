using System;
using System.Collections.Generic;

namespace Lyzard.DataStore
{
    public interface IStorageContract
    {
        IStorageSettings Settings { get; set; }

        T Find<T>(Predicate<T> predicate) where T : class;
        IEnumerable<T> Query<T>(Predicate<T> predicate) where T : class;
        int Remove<T>(Predicate<T> predicate) where T : class;
        void Store<T>(T item) where T : class;
    }
}