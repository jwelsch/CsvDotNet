using System.Collections.Generic;

namespace CsvDotNet.Mapping
{
    public enum OrdinalStatus
    {
        Valid,
        InvalidAlreadyExists,
        InvalidUnexpected
    }

    public interface IOrdinalValidator
    {
        OrdinalStatus Validate(int ordinal);
    }

    public class OrdinalValidator : IOrdinalValidator
    {
        private readonly IEnumerable<IOrdinalTracker> _trackers;

        public OrdinalValidator(IEnumerable<IOrdinalTracker> trackers)
        {
            _trackers = trackers;
        }

        public OrdinalStatus Validate(int ordinal)
        {
            foreach (var tracker in _trackers)
            {
                if (ordinal == tracker.Ordinal)
                {
                    if (tracker.Exists)
                    {
                        return OrdinalStatus.InvalidAlreadyExists;
                    }
                    else
                    {
                        tracker.Exists = true;
                        return OrdinalStatus.Valid;
                    }
                }
            }

            return OrdinalStatus.InvalidUnexpected;
        }
    }
}
