using System;
using System.Reflection;

namespace DotNetReflector
{
    public interface IParameterReflector
    {
        string Name { get; }

        ITypeReflector ParameterType { get; }
    }

    public class ParameterReflector : IParameterReflector
    {
        public ParameterInfo ParameterInfo { get; }

        private ITypeReflector _parameterType;

        public string Name => ParameterInfo.Name;

        public ITypeReflector ParameterType
        {
            get
            {
                if (_parameterType == null)
                {
                    _parameterType = new TypeReflector(ParameterInfo.ParameterType);
                }

                return _parameterType;
            }
        }

        public ParameterReflector(ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
        }
    }
}
