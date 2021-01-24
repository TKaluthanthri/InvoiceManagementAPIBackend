using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagementAPI.EmailSendingService
{
    public class EmailBodyContent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public string InvoiceNumber { get; set; }
        public string Amount { get; set; }
        public string Address { get; set; }


    }

}