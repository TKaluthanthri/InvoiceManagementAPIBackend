using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagementAPI.DBOperations
{
    public class DBContext : IdentityDbContext<IdentityUser>
    {
        public DBContext() : base("InvoiceManagementContex") {

            //Configuration.ProxyCreationEnabled = false; // this is line to be added

        }
    }
}