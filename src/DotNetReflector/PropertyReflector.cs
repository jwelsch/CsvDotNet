using System.Linq;
using System.Reflection;

namespace DotNetReflector
{
    public interface IPropertyReflector : IMemberReflector
    {
        ITypeReflector PropertyType { get; }

        object GetValue(object obj, params object[] index);

        void SetValue(object obj, object value);

        IAttributeReflector[] GetCustomAttributes();
    }


    public class PropertyReflector : MemberReflector<PropertyInfo>, IPropertyReflector
    {
        private ITypeReflector _propertyType;

        public PropertyReflector(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        public ITypeReflector PropertyType
        {
            get
            {
                if (_propertyType == null)
                {
                    _propertyType = new TypeReflector(MemberInfo.PropertyType);
                }

                return _propertyType;
            }
        }

        public object GetValue(object obj, params object[] index)
        {
            return MemberInfo.GetValue(obj, index);
        }

        public void SetValue(object obj, object value)
        {
            MemberInfo.SetValue(obj, value);
        }

        public IAttributeReflector[] GetCustomAttributes()
        {
            return MemberInfo.GetCustomAttributes().Select(i => new AttributeReflector(i)).ToArray();
        }
    }
}
