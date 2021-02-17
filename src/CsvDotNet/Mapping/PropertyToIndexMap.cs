using DotNetReflector;

namespace CsvDotNet.Mapping
{
    public interface IPropertyToIndexMap
    {
        IPropertyReflector Reflector { get; }

        int Index { get; }
    }

    public class PropertyToIndexMap : IPropertyToIndexMap
    {
        public IPropertyReflector Reflector { get; }

        public int Index { get; }

        public PropertyToIndexMap(IPropertyReflector reflector, int index)
        {
            Reflector = reflector;
            Index = index;
        }
    }
}
