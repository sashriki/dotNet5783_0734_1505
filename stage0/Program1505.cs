using System;

namespace Targil0
{
    partial class program
    {
        static void Main(string[]args)
        {
            welcome1505();
            welcome0734();
            Console.ReadKey();

        }
        static partial void welcome0734();
        private static void welcome1505()
        {
            Console.WriteLine("rgfds");
            Console.Write("enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first cosole application", name);
        }
    }
}