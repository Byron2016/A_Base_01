using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
