using System;
using System.Linq;

namespace CSVExtensions.Test.Extensions
{
    public static class StringExtensions
    {
        public static string[] ToCsvValues(this string @string)
        {
            var values = @string.Split(new[] {"\",\""}, StringSplitOptions.None);
            return values.Select(v => v.Trim('"')).ToArray();
        }

        public static string[] ToCsvLines(this string @string)
        {
            return @string.Split('\n');
        }

        public static string[] ToCsvHeaders(this string @string)
        {
            return @string.Split(',');
        }
    }
}