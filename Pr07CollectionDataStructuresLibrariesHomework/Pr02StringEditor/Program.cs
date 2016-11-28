namespace Pr02StringEditor
{
    using System;
    using Wintellect.PowerCollections;

    class Program
    {
        static void Main(string[] args)
        {
            BigList<char> rope = new BigList<char>();
            while (true)
            {
                string[] input = Console.ReadLine().Split(new[] {' '}, 2);
                string command = input[0];
                switch (command)
                {
                    case "END":
                        return;
                    case "APPEND":
                        Append(input, rope);
                        break;
                    case "INSERT":
                        Insert(input, rope);
                        break;
                    case "DELETE":
                        Delete(input, rope);
                        break;
                    case "REPLACE":
                        Replace(input, rope);
                        break;
                    case "PRINT":
                        Console.WriteLine(string.Join("", rope));
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
        }

        private static void Replace(string[] input, BigList<char> rope)
        {
            string[] parts = input[1].Split(new[] {' '}, 3);
            int index = int.Parse(parts[0]);
            int count = int.Parse(parts[1]);
            string value = parts[2];

            if (index < 0 || count < 0 || index + count >= rope.Count)
            {
                Console.WriteLine("ERROR");
            }
            else
            {
                rope.RemoveRange(index, count);
                rope.InsertRange(index, value);
                Console.WriteLine("OK");
            }
        }

        private static void Delete(string[] input, BigList<char> rope)
        {
            string[] parts = input[1].Split();
            int index = int.Parse(parts[0]);
            int count = int.Parse(parts[1]);

            if (index < 0 || count < 0 || index + count >= rope.Count)
            {
                Console.WriteLine("ERROR");
            }
            else
            {
                rope.RemoveRange(index, count);
                Console.WriteLine("OK");
            }
        }

        private static void Insert(string[] input, BigList<char> rope)
        {
            string[] parts = input[1].Split(new[] {' '}, 2);
            int index = int.Parse(parts[0]);
            string value = parts[1];

            if (index >= rope.Count || index < 0)
            {
                Console.WriteLine("ERROR");
            }
            else
            {
                rope.InsertRange(index, value);
                Console.WriteLine("OK");
            }
        }

        private static void Append(string[] input, BigList<char> rope)
        {
            string value = input[1];
            rope.AddRange(value);
            Console.WriteLine("OK");
        }
    }
}
