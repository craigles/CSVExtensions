using System;
using System.Globalization;
using CSVExtensions.Test.Extensions;
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
            
            var lines = csv.ToCsvLines();
            var headers = lines[0].ToCsvHeaders();
            
            
            headers[0].Should().Be("Name");
            headers[1].Should().Be("Location");
            headers[2].Should().Be("Age");
            headers[3].Should().Be("Date");
            headers[4].Should().Be("Gender");
            headers[5].Should().Be("Sibling");
            
            var firstRow = lines[1].ToCsvValues();
            var firstItem = Items[0];
            
            firstRow[0].Should().Be(firstItem.Name);
            firstRow[1].Should().Be(string.Empty);
            firstRow[2].Should().Be(firstItem.Age.ToString());
            firstRow[3].Should().Be(firstItem.Date.ToString(CultureInfo.CurrentCulture));
            firstRow[4].Should().Be(firstItem.Gender.ToString());
            firstRow[5].Should().Be(string.Empty);
            
            var secondRow = lines[2].ToCsvValues();
            var secondItem = Items[1];
            
            secondRow[0].Should().Be(secondItem.Name);
            secondRow[1].Should().Be(secondItem.Location);
            secondRow[2].Should().Be(secondItem.Age.ToString());
            secondRow[3].Should().Be(secondItem.Date.ToString(CultureInfo.CurrentCulture));
            secondRow[4].Should().Be(secondItem.Gender.ToString());
            secondRow[5].Should().Be(secondItem.Sibling.ToString());
        }

        [Fact]
        public void CsvStringWithCustomFields()
        {
            var customCsv = Items.AsCsvString(new []{"Name", "Number", "Summary"},
                i => i.Name,
                i => i.Age,
                i => $"Name: {i.Name}, Date: {i.Date}");

            customCsv.Should().NotBeNullOrEmpty();

            var lines = customCsv.ToCsvLines();
            var headers = lines[0].ToCsvHeaders();

            headers[0].Should().Be("Name");
            headers[1].Should().Be("Number");
            headers[2].Should().Be("Summary");

            var firstRow = lines[1].ToCsvValues();
            var firstItem = Items[0];
            
            firstRow[0].Should().Be(firstItem.Name);
            firstRow[1].Should().Be(firstItem.Age.ToString());
            firstRow[2].Should().Be($"Name: {firstItem.Name}, Date: {firstItem.Date}");

            var secondRow = lines[2].ToCsvValues();
            var secondItem = Items[1];
            
            secondRow[0].Should().Be(secondItem.Name);
            secondRow[1].Should().Be(secondItem.Age.ToString());
            secondRow[2].Should().Be($"Name: {secondItem.Name}, Date: {secondItem.Date}");
        }
    }
}