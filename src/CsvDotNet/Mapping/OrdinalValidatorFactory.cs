namespace CsvDotNet.Mapping
{
    public interface IOrdinalValidatorFactory
    {
        IOrdinalValidator Create(int count);
    }

    public class OrdinalValidatorFactory : IOrdinalValidatorFactory
    {
        public IOrdinalValidator Create(int count)
        {
            var trackers = new OrdinalTracker[count];

            for (var i = 0; i < trackers.Length; i++)
            {
                trackers[i] = new OrdinalTracker(i);
            }

            return new OrdinalValidator(trackers);
        }
    }
}
