using System;
using System.Linq;
using System.Threading;

namespace P01.EvenNumbersThread
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            int start = input[0];
            int end = input[1];

            Thread evens = new Thread(() =>
            {
                PrintEvenNumbers(start, end);
                
            });

            evens.Start();
            evens.Join();
            Console.WriteLine("Thread finished work");

        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
