using System.IO;

namespace CsvDotNet
{
    public class TextReaderCsvDataProvider : ICsvDataProvider
    {
        private readonly TextReader _reader;

        public TextReaderCsvDataProvider(TextReader reader)
        {
            _reader = reader;
        }

        public int Next()
        {
            return _reader.Read();
        }

        public int Peek()
        {
            return _reader.Peek();
        }
    }
}
