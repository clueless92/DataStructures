namespace Pr01ReverseNumbersWithStack
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            Stack<int> stack = new Stack<int>();
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    stack.Push(int.Parse(input[i]));
                }
                while (stack.Count != 0)
                {
                    Console.Write("{0} ", stack.Pop());
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(input[0]);
            }
        }
    }
}
