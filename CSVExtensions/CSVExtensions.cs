using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVExtensions
{
    public static class EnumerableExtensions
    {
        private const string Seperator = ",";

        public static string AsCsvString<T>(this IEnumerable<T> items)
        {
            var properties = typeof(T).GetProperties().Select(property => property).ToArray();
            var propertyNames = properties.Select(property => property.Name);
            var headersLine = string.Join(Seperator, propertyNames);
            var valueLines = new List<string>();

            foreach (var item in items)
            {
                var values = properties.Select(p =>
                {
                    var propertyValue = p.GetValue(item)?.ToString();

                    if (string.IsNullOrEmpty(propertyValue))
                    {
                        return "\"\"";
                    }

                    return $"\"{propertyValue.Escape('\"')}\"";
                });

                valueLines.Add(string.Join(Seperator, values));
            }

            return new StringBuilder()
                .AppendLine(headersLine)
                .AppendLines(valueLines.ToArray())
                .ToString();
        }

        public static string AsCsvString<T>(this IEnumerable<T> items, string[] headers, params Func<T, object>[] valueFuncs)
        {
            var headersLine = string.Join(Seperator, headers);
            var valueLines = new List<string>();

            foreach (var item in items)
            {
                var values = valueFuncs.Select(valueFunc =>
                {
                    var value = valueFunc(item).ToString().Escape('\"');
                    return $"\"{value}\"";
                });

                valueLines.Add(string.Join(Seperator, values));
            }

            return new StringBuilder()
                .AppendLine(headersLine)
                .AppendLines(valueLines.ToArray())
                .ToString();
        }

        private static string Escape(this string source, char charToEscape)
        {
            return source.Replace(charToEscape.ToString(), $"{charToEscape}{charToEscape}");
        }

        private static StringBuilder AppendLines(this StringBuilder sb, params string[] lines)
        {
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            return sb;
        }
    }
}