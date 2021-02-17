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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is OrderedSampleRow compare))
            {
                return false;
            }

            return Id == compare.Id && Name == compare.Name && Timestamp == compare.Timestamp;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Timestamp.GetHashCode();
        }
    }
}
