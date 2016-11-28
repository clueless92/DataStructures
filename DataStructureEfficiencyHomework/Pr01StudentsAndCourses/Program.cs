namespace Pr01StudentsAndCourses
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var courseDictionary = new SortedDictionary<string, SortedSet<Person>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                string[] inputParts = input.Split(new[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries);
                string courseName = inputParts[2];
                string firstName = inputParts[0];
                string lastName = inputParts[1];

                if (!courseDictionary.ContainsKey(courseName))
                {
                    courseDictionary.Add(courseName, new SortedSet<Person>());
                }

                courseDictionary[courseName].Add(new Person(firstName, lastName));
            }

            foreach (var pair in courseDictionary)
            {
                Console.Write("{0}: ", pair.Key);
                Console.WriteLine(string.Join(", ", pair.Value));
            }
        }
    }
}
