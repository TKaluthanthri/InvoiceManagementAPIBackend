using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Repositories
{
    public interface IEmailRepository
    {
        bool SendInvoiceEmail(InvoiceModel invoice, CustomerModel customer);
    }
}
