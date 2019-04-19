using System.Collections.Generic;

namespace Lyzard.DataStore
{
    /// <summary>
    /// A more generalized MetaWrapper for wrapping data with additional meta data.
    /// The meta data type can hold any additional meta data desired
    /// </summary>
    /// <typeparam name="Tdata">The data type to wrap</typeparam>
    /// <typeparam name="Tmeta">The meta data type to include</typeparam>
    public sealed class MetaWrapper<Tdata, Tmeta>
        where Tdata: class
        where Tmeta: class, new()
    {
        private static Dictionary<Tdata, MetaWrapper<Tdata, Tmeta>> _dataToMeta = new Dictionary<Tdata, MetaWrapper<Tdata, Tmeta>>();
        private static Dictionary<MetaWrapper<Tdata, Tmeta>, Tdata> _metaToData = new Dictionary<MetaWrapper<Tdata, Tmeta>, Tdata>();

        /// <summary>
        /// The stored data
        /// </summary>
        public Tdata Data { get; private set; }

        /// <summary>
        /// The stored metadata
        /// </summary>
        public Tmeta Meta { get; private set; }

        /// <summary>
        /// Must use the create method
        /// </summary>
        private MetaWrapper()
        {
        }

        /// <summary>
        /// Cast or create MetaWrapper from data
        /// </summary>
        /// <param name="data"></param>
        public static implicit operator MetaWrapper<Tdata, Tmeta>(Tdata data)
        {
            if (_dataToMeta.ContainsKey(data))
            {
                return _dataToMeta[data];
            }
            return Create(data);
        }

        /// <summary>
        /// Cast MetaWrapper back to data.  Should not return null unless something went very wrong
        /// </summary>
        /// <param name="meta">The metawrapper object</param>
        public static implicit operator Tdata(MetaWrapper<Tdata, Tmeta> meta)
        {
            if (_metaToData.ContainsKey(meta))
            {
                return _metaToData[meta];
            }
            return null;
        }

        /// <summary>
        /// Creates a new MetaWrapper and registers it with the cache
        /// </summary>
        /// <param name="data">The data object to wrap</param>
        /// <returns>The wrapped object cast back to the data type</returns>
        public static Tdata Create(Tdata data, Tmeta metaData)
        {
            if (_dataToMeta.ContainsKey(data))
            {
                return _dataToMeta[data];
            } else
            {
                var wrapper = new MetaWrapper<Tdata, Tmeta>
                {
                    Data = data,
                    Meta = metaData
                };
                _dataToMeta.Add(data, wrapper);
                _metaToData.Add(wrapper, data);
                return wrapper;
            }
        }

        /// <summary>
        /// Creates a new MetaWrapper with default new Tmeta 
        /// </summary>
        /// <param name="data">The data object to wrap</param>
        /// <returns>The data object wrapped</returns>
        public static Tdata Create(Tdata data)
        {
            return Create(data, new Tmeta());
        }

        /// <summary>
        /// Gets the raw meta data associated with data
        /// data item must have been previously wrapped and returned
        /// or this will return null.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Tmeta GetMetaData(Tdata data)
        {
            if (_dataToMeta.ContainsKey(data))
            {
                return _dataToMeta[data].Meta;
            }
            return null;
        }

    }
}
