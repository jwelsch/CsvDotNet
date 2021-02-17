using System.Reflection;

namespace DotNetReflector
{
    public interface IFieldReflector : IMemberReflector
    {
        ITypeReflector FieldType { get; }

        object GetValue(object obj);

        void SetValue(object obj, object value);
    }

    public class FieldReflector : MemberReflector<FieldInfo>, IFieldReflector
    {
        private ITypeReflector _fieldType;

        public FieldReflector(FieldInfo fieldInfo)
            : base(fieldInfo)
        {
        }

        public ITypeReflector FieldType
        {
            get
            {
                if (_fieldType == null)
                {
                    _fieldType = new TypeReflector(MemberInfo.FieldType);
                }

                return _fieldType;
            }
        }

        public object GetValue(object obj)
        {
            return MemberInfo.GetValue(obj);
        }

        public void SetValue(object obj, object value)
        {
            MemberInfo.SetValue(obj, value);
        }
    }
}
