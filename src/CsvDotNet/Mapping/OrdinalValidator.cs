using System;
using System.Collections.Generic;
using System.Linq;

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

        int[] GetUnvalidatedOrdinals();
    }

    public class OrdinalValidator : IOrdinalValidator
    {
        private readonly IEnumerable<IOrdinalTracker> _trackers;

        public OrdinalValidator(int[] ordinals)
        {
            if (ordinals == null)
            {
                throw new ArgumentNullException(nameof(ordinals));
            }

            EnsureUniqueOrdinals(ordinals);

            _trackers = ordinals.Select(i => new OrdinalTracker(i)).ToArray();
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

        public int[] GetUnvalidatedOrdinals()
        {
            return _trackers.Where(i => !i.Exists).Select(i => i.Ordinal).ToArray();
        }

        private static void EnsureUniqueOrdinals(int[] ordinals)
        {
            for (var i = 0; i < ordinals.Length; i++)
            {
                for (var j = i + 1; j < ordinals.Length; j++)
                {
                    if (ordinals[i] == ordinals[j])
                    {
                        throw new ArgumentException($"The ordinal '{ordinals[i]}' appears more than once.");
                    }
                }
            }
        }
    }
}
