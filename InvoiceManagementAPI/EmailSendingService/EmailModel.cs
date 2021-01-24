using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagementAPI.EmailSendingService
{
    public class EmailModel
    {
        public string EmailSender { get; set; }
        public string EmailReceiver { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}