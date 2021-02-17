using System;

namespace DotNetReflector
{
    public interface IAttributeReflector
    {
        ITypeReflector Type { get; }

        bool Equals(IAttributeReflector comparison);

        T GetPropertyValue<T>(string name);
    }

    public class AttributeReflector : IAttributeReflector
    {
        private readonly Attribute _attribute;

        private ITypeReflector _type;
        public ITypeReflector Type
        {
            get
            {
                if (_type == null)
                {
                    _type = new TypeReflector(_attribute.GetType());
                }
                return _type;
            }
        }

        public AttributeReflector(Attribute attribute)
        {
            _attribute = attribute;
        }

        public bool Equals(IAttributeReflector comparison)
        {
            return Type.FullName == comparison.Type.FullName;
        }

        public T GetPropertyValue<T>(string name)
        {
            var reflector = Type.GetProperty(name);

            return (T)reflector.GetValue(_attribute);
        }
    }
}
