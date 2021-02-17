using System;

namespace CsvDotNet.Tests.Samples
{
    [NamedCsvRow]
    public class NamedSampleRow
    {
        [NamedCsvColumn]
        public int Id { get; set; }

        [NamedCsvColumn]
        public string Name { get; set; }

        [NamedCsvColumn]
        public DateTime Timestamp { get; set; }

        public int Count { get; set; }
    }
}
