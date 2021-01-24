using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Tests
{
    public class InvoiceModelTest
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime invoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string CustomerEmail { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsMailSent { get; set; }
        public int CustomerId { get; set; }
    }
}
