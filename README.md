# CSVExtensions
Extensions for IEnumerable that return back a CSV formatted string. Groundbreaking stuff.

## Usage
Given an example list of items:
```
People =
{
    new Person
    {
        Name = "Craigles",
        Date = DateTime.UtcNow,
        Gender = Gender.Male
    },
    new Person
    {
        Name = "Dee Dee",
        Location = "Somewhere",
        Age = 31,
        Gender = Gender.Male,
        Date = DateTime.UtcNow
    }
};
```
Using the default options:
```
var csv = People.AsCsvString();
```
```
Name,Location,Age,Date,Gender
"Craigles","","0","11/04/2017 00:00:01","Male",""
"Dee Dee","Somewhere","31","11/04/2017 00:00:01","Male"
```
Providing custom header values and value functons:
```
var customCsv = People.AsCsvString(new []{"Name", "Number", "Summary"},
    i => i.Name,
    i => i.Age,
    i => $"Name: {i.Name}, Date: {i.Date}");
```
```
Name,Number,Summary
"Craigles","0","Name: Craigles, Date: 11/04/2017 00:03:09"
"Dee Dee","31","Name: Dee Dee, Date: 11/04/2017 00:03:09"
```

### Nested Objects
All properties of an object are output using their ToString() implementation. Nested objects without a custom ToString() method will simply just use the default ToString() implementation:
```
Items =
{
    new Person
    {
        Name = "Craigles",
        Date = DateTime.UtcNow,
        Gender = Gender.Male,
        Sibling = new Person
        {
            Name = "Dee Dee"
        }
    }
};
```
```
var csv = People.AsCsvString();
```
```
Name,Location,Age,Date,Gender,Sibling
"Craigles","","0","11/04/2017 00:10:52","Male","CSVExtensions.Test.Person"
```