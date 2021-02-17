using System;

namespace CsvDotNet.Tests.Samples
{
    [OrderedCsvRow]
    public class OrderedSampleRow
    {
        [OrderedCsvColumn(0)]
        public int Id { get; set; }

        [OrderedCsvColumn(2)]
        public DateTime Timestamp { get; set; }

        [OrderedCsvColumn(1)]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
