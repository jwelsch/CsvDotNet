using System;

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
            if (count < 0)
            {
                throw new ArgumentException(nameof(count));
            }

            var ordinals = new int[count];

            for (var i = 0; i < ordinals.Length; i++)
            {
                ordinals[i] = i;
            }

            return new OrdinalValidator(ordinals);
        }
    }
}
