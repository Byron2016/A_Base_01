using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<InvoiceDetail> Detail { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public bool Habilitado { get; set; }

        public Invoice()
        {
            Detail = new List<InvoiceDetail>();
        }
    }
}
