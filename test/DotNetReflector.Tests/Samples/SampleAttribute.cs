using System;

namespace DotNetReflector.Tests.Samples
{
    public class SampleAttribute : Attribute
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
