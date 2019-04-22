using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Interfaces
{

    public enum Format {
        None,
        Indented
    }

    /// <summary>
    /// Represents the contract for serializing and deserializing information.
    /// This contract is meant for "non-binary" serialization
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Writes the serialized data to text writer
        /// </summary>
        /// <param name="writer">The text writer to write to</param>
        /// <param name="item">The item to serialize</param>
        void Serialize<T>(TextWriter writer, Format format, T item);

        /// <summary>
        /// Reads from the text reader and returns the item T
        /// </summary>
        /// <param name="reader">The reader to read from</param>
        /// <returns>The item deserialized</returns>
        T Deserialize<T>(TextReader reader);
    }
}
