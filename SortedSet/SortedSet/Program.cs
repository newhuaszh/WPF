using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSet
{
    public class Person
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() { }
        public Person(string firstName, string lastName, int age)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}, Age: {2}", FirstName, LastName, Age);
        }
    }

    class SortPeopleByAge : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            if (x.Age > y.Age)
                return 1;
            if (x.Age < y.Age)
                return -1;
            else
                return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<Person> setOfPeople = new SortedSet<Person>(new SortPeopleByAge())
            {
                new Person {FirstName = "Homer", LastName = "Simpson", Age = 47 },
                new Person {FirstName = "Marge", LastName = "Simpson", Age = 45 },
                new Person {FirstName = "Lisa", LastName = "Simpson", Age = 9 },
                new Person {FirstName = "Bart", LastName = "Simpson", Age = 8 }
            };
        }
    }
}
