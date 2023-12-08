using System;

namespace ZEUS
{
    public class Testing
    {
        public static void SayHello()
        {
            Console.WriteLine("Hello from ZEUS!");
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static string Greet(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
