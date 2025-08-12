using System;

namespace Day9_onwards
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Show("Hello World");
        }

        public static void Show(string message, string name = "123")
        {
            Console.WriteLine(message + " - " + name);
        }
    }
}