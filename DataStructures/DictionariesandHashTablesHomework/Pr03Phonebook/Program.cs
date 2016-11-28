namespace Pr03Phonebook
{
    using System;
    using Pr01DictionaryImplementation;

    class Program
    {
        static void Main(string[] args)
        {
            LinkedHashMap<string, string> phonebook = new LinkedHashMap<string,string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "search")
                {
                    break;
                }

                string[] inputParts = input.Split('-');
                string name = inputParts[0];
                string phoneNumber = inputParts[1];

                if (!phonebook.ContainsKey(name))
                {
                    phonebook.Add(name, phoneNumber);
                }
            }

            while (true)
            {
                string searchName = Console.ReadLine();
                if (searchName == "exit")
                {
                    return;
                }

                if (phonebook.ContainsKey(searchName))
                {
                    Console.WriteLine("{0} -> {1}", searchName, phonebook[searchName]);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", searchName);
                }
            }
        }
    }
}
