﻿using Common;
using System;
using System.Data.SqlClient;

namespace Services
{
    public class TestService
    {
        public static void TestConnection()
        {

            try
            {
                using (var context = new SqlConnection(Parameters.ConnectionString))
                {
                    context.Open();
                    Console.WriteLine("Sql Connection successful");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sql Server: {ex.Message}");
            }

        }
    }
}
