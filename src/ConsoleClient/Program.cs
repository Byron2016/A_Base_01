using Services;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestService.TestConnection();

            var orderService = new OrderService();

            var result = orderService.GetAll();

            Console.Read();
        }
    }
}
