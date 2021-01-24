using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagementAPI.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
    }
}