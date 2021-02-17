namespace DotNetReflector.Tests.Samples
{
    [Sample]
    public class SampleClass
    {
        public int field1;

        public string field2 = "foo";

        public int Property1 { get; set; }

        public string Property2 { get; set; } = "foo";

        public int Method1()
        {
            return 0;
        }

        public string Method2(string arg)
        {
            return "foo" + arg;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var comparison = (SampleClass)obj;

            return field1 == comparison.field1
                && field2 == comparison.field2
                && Property1 == comparison.Property1
                && Property2 == comparison.Property2;
        }

        public override int GetHashCode()
        {
            return field1.GetHashCode() ^ field2.GetHashCode() ^ Property1.GetHashCode() ^ Property2.GetHashCode();
        }
    }
}
