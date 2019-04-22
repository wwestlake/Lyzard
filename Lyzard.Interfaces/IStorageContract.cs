using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyzard.Interfaces
{
    public interface IStorageContract<T> where T : class
    {

        /// <summary>
        /// Purges a container of all existing data
        /// </summary>
        void Purge();

        /// <summary>
        /// The settings for the DataStore
        /// </summary>
        IStorageSettings Settings { get; set;  }

        /// <summary>
        /// Returns an enumeration of items that match the predicate
        /// </summary>
        /// <param name="predicate">The predicate to match items</param>
        /// <returns>And enumeration of items</returns>
        IQueryable<T> Query(Predicate<T> predicate);

        /// <summary>
        /// Finds the first occurance, latest revision of item that matches T
        /// or null if no matches
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>The item or null</returns>
        T Find(Predicate<T> predicate);

        /// <summary>
        /// Returns a specified revision that matches the predicate
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <param name="revision">The desired revision</param>
        /// <returns>The item</returns>
        T Find(Predicate<T> predicate, int revision);


        /// <summary>
        /// Stores or updates the item 
        /// </summary>
        /// <param name="item">The item to store</param>
        T Store(T item);

        /// <summary>
        /// Deletes an item and all revisions from the database
        /// </summary>
        /// <param name="item">The item to delete</param>
        void Delete(T item);

        /// <summary>
        /// Removes all revisions except the latest revision reducing database size
        /// </summary>
        /// <param name="item">The item to prune</param>
        void Prune(T item);

        /// <summary>
        /// Prunes the database from the specified revision back (includes the specified revision)
        /// </summary>
        /// <param name="item">The item to prune</param>
        /// <param name="revision">The revision to prune</param>
        void Prune(T item, int revision);


        /// <summary>
        /// Gets the identifier of an item.  
        /// </summary>
        /// <param name="item">The item to identify</param>
        /// <returns>The Guid or Null if not found</returns>
        Guid? Identify(T item);

    }
}