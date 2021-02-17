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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is NamedSampleRow compare))
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
