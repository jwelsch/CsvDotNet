using System.Linq;
using System.Reflection;

namespace DotNetReflector
{
    public interface IMethodReflector : IMemberReflector
    {
        ITypeReflector ReturnType { get; }

        IParameterReflector[] GetParameters();

        object Invoke(object obj, params object[] parameters);
    }

    public class MethodReflector : MemberReflector<MethodInfo>, IMethodReflector
    {
        private ITypeReflector _returnType;
        private IParameterReflector[] _parameterTypes;

        public MethodReflector(MethodInfo methodInfo)
            : base(methodInfo)
        {
        }

        public ITypeReflector ReturnType
        {
            get
            {
                if (_returnType == null)
                {
                    _returnType = new TypeReflector(MemberInfo.ReturnType);
                }

                return _returnType;
            }
        }

        public IParameterReflector[] GetParameters()
        {
            if (_parameterTypes == null)
            {
                _parameterTypes = MemberInfo.GetParameters().Select(i => new ParameterReflector(i)).ToArray();
            }

            return _parameterTypes;
        }

        public object Invoke(object obj, params object[] parameters)
        {
            return MemberInfo.Invoke(obj, parameters);
        }
    }
}
