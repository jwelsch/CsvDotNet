using DotNetReflector;

namespace CsvDotNet.Mapping
{
    public interface ICsvColumnMap
    {
        ITypeReflector Type { get; }

        bool CanMapIndex(int columnIndex);

        void Map(object obj, object value);
    }

    public class CsvColumnMap : ICsvColumnMap
    {
        private readonly int _columnIndex;
        private readonly IPropertyReflector _reflector;

        public ITypeReflector Type => _reflector.PropertyType;

        public CsvColumnMap(int columnIndex, IPropertyReflector reflector)
        {
            _columnIndex = columnIndex;
            _reflector = reflector;
        }

        public bool CanMapIndex(int columnIndex)
        {
            return _columnIndex == columnIndex;
        }

        public void Map(object obj, object value)
        {
            _reflector.SetValue(obj, value);
        }
    }
}
