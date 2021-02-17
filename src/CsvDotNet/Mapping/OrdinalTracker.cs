namespace CsvDotNet.Mapping
{
    public interface IOrdinalTracker
    {
        int Ordinal { get; }

        bool Exists { get; set; }
    }

    public class OrdinalTracker : IOrdinalTracker
    {
        public int Ordinal { get; }

        public bool Exists { get; set; }

        public OrdinalTracker(int ordinal)
        {
            Ordinal = ordinal;
        }
    }
}