using System;
using System.Reflection;

namespace DotNetReflector
{
    public interface IMemberReflector
    {
        string Name { get; }
    }

    public class MemberReflector<T> : IMemberReflector
        where T : MemberInfo
    {
        public T MemberInfo { get; }

        public string Name => MemberInfo.Name;

        public MemberReflector(T memberInfo)
        {
            MemberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
        }
    }
}
