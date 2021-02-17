using System;
using System.Linq;
using System.Reflection;

namespace DotNetReflector
{
    public interface ITypeReflector
    {
        Type Type { get; }

        string Name { get; }

        string FullName { get; }

        IMethodReflector GetMethod(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IPropertyReflector GetProperty(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IFieldReflector GetField(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IMethodReflector[] GetMethods(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IPropertyReflector[] GetProperties(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IFieldReflector[] GetFields(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance);

        IAttributeReflector[] GetCustomAttributes();

        bool Equals(ITypeReflector comparison);
    }

    public class TypeReflector : ITypeReflector
    {
        public Type Type { get; }

        public string Name => Type.Name;

        public string FullName => Type.FullName;

        public TypeReflector(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public IMethodReflector GetMethod(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMember<IMethodReflector>(() => Type.GetMethod(name, bindingFlags) ?? throw new NotImplementedException($"The type '{Type.FullName}' does not implement a method with name '{name}'."),
                                                i => new MethodReflector(i as MethodInfo));
        }

        public IPropertyReflector GetProperty(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMember<IPropertyReflector>(() => Type.GetProperty(name, bindingFlags) ?? throw new NotImplementedException($"The type '{Type.FullName}' does not implement a property with name '{name}'."),
                                                  i => new PropertyReflector(i as PropertyInfo));
        }

        public IFieldReflector GetField(string name, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMember<IFieldReflector>(() => Type.GetField(name, bindingFlags) ?? throw new NotImplementedException($"The type '{Type.FullName}' does not implement a field with name '{name}'."),
                                               i => new FieldReflector(i as FieldInfo));
        }

        public IMethodReflector[] GetMethods(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMembers<IMethodReflector>(() => Type.GetMethods(bindingFlags),
                                                 i => i.Select(j => new MethodReflector(j as MethodInfo)).ToArray());
        }

        public IPropertyReflector[] GetProperties(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMembers<IPropertyReflector>(() => Type.GetProperties(bindingFlags),
                                                   i => i.Select(j => new PropertyReflector(j as PropertyInfo)).ToArray());
        }

        public IFieldReflector[] GetFields(BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            return GetMembers<IFieldReflector>(() => Type.GetFields(bindingFlags),
                                                i => i.Select(j => new FieldReflector(j as FieldInfo)).ToArray());
        }

        public IAttributeReflector[] GetCustomAttributes()
        {
            var attributes = Type.GetCustomAttributes();

            return attributes.Select(i => new AttributeReflector(i)).ToArray();
        }

        public bool Equals(ITypeReflector comparison)
        {
            return comparison.FullName == FullName;
        }

        public override bool Equals(object comparison)
        {
            if (comparison == null || comparison.GetType() != GetType())
            {
                return false;
            }

            return Equals((ITypeReflector)comparison);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        private static T GetMember<T>(Func<MemberInfo> getter, Func<MemberInfo, T> wrapper)
            where T : IMemberReflector
        {
            var info = getter();

            return info == null ? default : wrapper(info);
        }

        private static T[] GetMembers<T>(Func<MemberInfo[]> getter, Func<MemberInfo[], T[]> wrapper)
            where T : IMemberReflector
        {
            var info = getter();

            return info == null ? default : wrapper(info);
        }
    }
}
