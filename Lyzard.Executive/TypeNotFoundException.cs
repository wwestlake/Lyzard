using System;
using System.Runtime.Serialization;

namespace Lyzard.Executive
{
    [Serializable]
    public class TypeNotFoundException : Exception
    {
        public TypeNotFoundException(string className, string message) : this ($"{className}: {message}")
        {
        }

        public TypeNotFoundException(string message) : base(message)
        {
        }

        public TypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}