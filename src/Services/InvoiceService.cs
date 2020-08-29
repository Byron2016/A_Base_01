using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Services
{
    public class InvoiceService
    {
        public List<Invoice> GetAll()
        {

            var result = new List<Invoice>();

            using(var context = new SqlConnection(Parameters.ConnectionString))
            {
                context.Open();

                var command = new SqlCommand("SELECT * FROM Invoices WHERE Habilitado = @habilitado", context);

                command.Parameters.AddWithValue("@habilitado", 1);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Invoice = new Invoice
                        {
                            Id = Convert.ToInt32(reader["Id"]),

                            Iva = Convert.ToDecimal(reader["Iva"]),

                            SubTotal = Convert.ToDecimal(reader["SubTotal"]),

                            Total = Convert.ToDecimal(reader["Total"]),

                            ClientId = Convert.ToInt32(reader["ClientId"]),

                            Habilitado = Convert.ToBoolean(reader["Habilitado"])
                        };

                        result.Add(Invoice);

                    }
                }

                // Set aditional properties

                foreach(var invoice in result)
                {
                    // Client
                    SetClient(invoice, context);

                    // Detail
                    SetDetail(invoice, context);
                }

            }

            return result;

        }

        private void SetClient(Invoice invoice, SqlConnection context)
        {
            var command = new SqlCommand("SELECT * FROM clients WHERE id = @clientId and Habilitado = @habilitado", context);

            command.Parameters.AddWithValue("@clientId", invoice.ClientId);

            command.Parameters.AddWithValue("@habilitado", 1);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                invoice.Client = new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),

                    Name = reader["Name"].ToString(),

                    Habilitado = Convert.ToBoolean(reader["Habilitado"])
                };
            }
        }

        private void SetDetail(Invoice invoice, SqlConnection context)
        {
            var command = new SqlCommand("SELECT * FROM InvoiceDetail WHERE InvoiceId = @InvoiceId and Habilitado = @habilitado", context);

            command.Parameters.AddWithValue("@InvoiceId", invoice.Id);

            command.Parameters.AddWithValue("@habilitado", 1);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    invoice.Detail.Add(new InvoiceDetail
                    {
                        Id = Convert.ToInt32(reader["Id"]),

                        ProductId = Convert.ToInt32(reader["productId"]),

                        Quantity = Convert.ToInt32(reader["Quantity"]),

                        Price = Convert.ToDecimal(reader["Price"]),

                        Iva = Convert.ToDecimal(reader["Iva"]),

                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),

                        Total = Convert.ToDecimal(reader["Total"]),

                        Habilitado = Convert.ToBoolean(reader["Habilitado"]),

                        Invoice = invoice
                    });
                }
            };

            foreach (var detail in invoice.Detail)
            {
                // Product
                SetProduct(detail, context);

            }
        }

        private void SetProduct(InvoiceDetail detail, SqlConnection context)
        {
            var command = new SqlCommand("SELECT * FROM products WHERE id = @productId and Habilitado = @habilitado", context);

            command.Parameters.AddWithValue("@productId", detail.ProductId);
            command.Parameters.AddWithValue("@habilitado", 1);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                detail.Product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),

                    Price = Convert.ToDecimal(reader["Price"]),

                    Name = reader["Name"].ToString(),

                    Habilitado = Convert.ToBoolean(reader["Habilitado"])
                };
            }
        }
    }
}
