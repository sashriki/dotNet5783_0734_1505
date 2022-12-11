namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome1505();
//            welcome0734();
            Console.ReadKey();
            Console.WriteLine("hgfds");

        }
        //static partial void welcome0734();
        private static void welcome1505()
        {
            Console.Write("enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first cosole application", name);
        }
    }
}