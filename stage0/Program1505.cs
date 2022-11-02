using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[]args)
        {
            welcome1505();
            Welcome0734();
            Console.ReadKey();
            Console.WriteLine("hgfds");
            Console.WriteLine("htgfd");

        }
        static partial void Welcome0734();
        private static void welcome1505()
        {
            Console.Write("enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first cosole application", name);
        }
    }
}