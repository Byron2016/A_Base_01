using Services;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestService.TestConnection();
            Console.Read();
        }
    }
}
