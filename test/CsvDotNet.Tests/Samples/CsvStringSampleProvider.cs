using System;
using System.Collections.Generic;
using System.Text;

namespace CsvDotNet.Tests.Samples
{
    public static class CsvStringSampleProvider
    {
        public static string[] OneColumnOneRow => new[] { "foo", "foo" };

        public static string TwoColumnsOneRow => "foo,bar";

        public static string ThreeColumnsOneRow => "foo,bar,baz";

        public static string TwoColumnsOneRowCrlf => "foo,bar\r\n";

        public static string TwoColumnsOneRowCr => "foo,bar\r";

        public static string TwoColumnsOneRowLf => "foo,bar\n";

        public static string TwoColumnsTwoRowsCrlf => "foo,bar\r\ntoo,baz";

        public static string TwoColumnsTwoRowsCr => "foo,bar\rtoo,baz";

        public static string TwoColumnsTwoRowsLf => "foo,bar\ntoo,baz";

        public static string ThreeColumnsOneRowFirstColumnEmpty => ",foo,bar";

        public static string ThreeColumnsOneRowSecondColumnEmpty => "foo,,bar";

        public static string ThreeColumnsOneRowLastColumnEmpty => "foo,bar,";

        public static string ThreeColumnsOneRowLastColumnEmptyCrLf => "foo,bar,\r\n";
    }
}
