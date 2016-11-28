using System;

namespace FactorialTest
{
    class FactorialTest
    {
        static decimal Factorial(int num)
        {
            if (num == 0)
            {
                return 1;
            }
            else
            {
                return num * Factorial(num - 1);
            }
        }
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine(Factorial(num));
        }
    }
}
