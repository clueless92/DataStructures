namespace Pr02CalculateSequenceWithQueue
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Queue<int> sequence = new Queue<int>();
            sequence.Enqueue(n);
            for (int i = 0; i < 50; i++)
            {
                int curr = sequence.Dequeue();
                if (i == 49)
                {
                    Console.WriteLine(curr);
                }
                else
                {
                    Console.Write("{0}, ", curr);
                }
                sequence.Enqueue(curr + 1);
                sequence.Enqueue(curr * 2 + 1);
                sequence.Enqueue(curr + 2);
            }
        }
    }
}
