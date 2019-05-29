using System.Collections.Generic;

namespace Lyzard.GraphicsLib
{
    /// <summary>
    /// Delegate for transfering data to the graphics system
    /// </summary>
    /// <typeparam name="T">The return type of the data</typeparam>
    /// <returns>The data</returns>
    public delegate T DataSource<T>();

    /// <summary>
    /// Generic data set that represnts the data to be plotted in a given chart
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataSet<T>
    {
        private DataSource<IEnumerable<T>> dataSource;

        /// <summary>
        /// Constructs a data set expecting an enumeration of the data type as the data source
        /// </summary>
        /// <param name="data"></param>
        public DataSet(DataSource<IEnumerable<T>> data)
        {
            SetDataSource(data);
        }

        /// <summary>
        /// Gets the enumerated data
        /// </summary>
        public IEnumerable<T> Data { get { return GetDataSource()(); } }


        private DataSource<IEnumerable<T>> GetDataSource()
        {
            return dataSource;
        }

        private void SetDataSource(DataSource<IEnumerable<T>> value)
        {
            dataSource = value;
        }
    }


}
