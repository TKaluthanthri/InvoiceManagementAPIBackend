using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Repositories
{
    public interface IInvoiceRepository
    {
        List<InvoiceModel> GetAllInvoices();
        bool SaveInvoiceData(InvoiceModel invoice);
        List<InvoiceModel> FilterInvoicesByEmail(string Email);
        bool RemoveSelectedInvoice(string invoiceNumber);
        List<InvoiceModel> FilterInvoicesInvoiceNumber(string invoiceNumber);
    }
}
