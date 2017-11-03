using System;
using FluentAssertions;
using Xunit;

namespace CSVExtensions.Test
{
    public class CsvExtensionsTests
    {
        private static readonly Person[] Items =
        {
            new Person
            {
                Name = "Craigles",
                Date = DateTime.UtcNow
            },
            new Person
            {
                Name = "Dee Dee",
                Location = "Somewhere",
                Age = 31,
                Date = DateTime.UtcNow,
                Sibling = new Person
                {
                    Name = "Limmy",
                    Location = "Don't know"
                }
            }
        };
        
        [Fact]
        public void CsvString()
        {
            var csv = Items.AsCsvString();

            csv.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void CsvStringWithCustomFields()
        {
            var customCsv = Items.AsCsvString(new []{"Name", "Number", "Summary"},
                i => i.Name,
                i => i.Age,
                i => $"Name: {i.Name}, Date: {i.Date}");

            customCsv.Should().NotBeNullOrEmpty();
        }
    }
}