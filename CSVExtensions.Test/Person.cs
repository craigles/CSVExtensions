using System;

namespace CSVExtensions.Test
{
    class Person
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public Gender Gender { get; set; }
        public Person Sibling { get; set; }
    }

    internal enum Gender
    {
        Male,
        Female
    }
}