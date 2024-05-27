using System;
using System.Threading;

namespace ServerCore
{
    internal class Program
    {
        static void MainThread(object state)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Hello Thread");
            }
            
        }

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(MainThread);


            /*
            Thread t = new Thread(MainThread);
            t.Name = "test Thread";
            t.IsBackground = true;
            t.Start();
            Console.WriteLine("Waiting for Thread");
            t.Join();
            Console.WriteLine("Hello, World!");
            */
        }
    }
}
