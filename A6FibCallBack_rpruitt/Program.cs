using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace A6FibCallBack_rpruitt
{
    internal class Program
    {
        public delegate int CalculateFibonacci(int num);

        public static int number = 0;

        private static void Main(string[] args)
        {
            Console.WriteLine("Please enter a number");
            number = Convert.ToInt32(Console.ReadLine());
            CalculateFibonacci cf = new CalculateFibonacci(CalculateFib);
            IAsyncResult asyncResult = cf.BeginInvoke(number, new AsyncCallback(WorkCompleted), null);

            while (!asyncResult.IsCompleted)
            {
                Console.WriteLine($"Continuing to do work on thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static int CalculateFib(int n)
        {
            if ((n == 0) || (n == 1))
            {
                return n;
            }
            else
            {
                return (CalculateFib(n - 1) + CalculateFib(n - 2));
            }
        }

        public static void WorkCompleted(IAsyncResult res)
        {
            Console.WriteLine("Calculating Fibonacci completed");
            AsyncResult ar = (AsyncResult)res;
            CalculateFibonacci cf = (CalculateFibonacci)ar.AsyncDelegate;
            long result = cf.EndInvoke(res);
            Console.WriteLine($"Result is: {result}");
        }
    }
}