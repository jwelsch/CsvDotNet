using System;

namespace CsvDotNet
{

    [Serializable]
    public class CsvException : Exception
    {
        public CsvException()
        {
        }

        public CsvException(string message)
            : base(message)
        {
        }

        public CsvException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CsvException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
