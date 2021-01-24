using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InvoiceManagementAPI.Repositories
{
    public class InvoiceRepository :  IInvoiceRepository
    {
        private InvoiceManagementEntities1 context = new InvoiceManagementEntities1();

        public InvoiceRepository()
        {
            // TO-DO DI needs to apply here
        }
        public List<InvoiceModel> GetAllInvoices()
        {
            try
            {
                List<Invoice> Invoices = context.Invoices.ToList();
                List<InvoiceModel> invoiceList = new List<InvoiceModel>();
                invoiceList = (List<InvoiceModel>)Invoices.Select(inv => new InvoiceModel()
                {
                    Id = inv.Id,
                    invoiceDate = inv.invoiceDate,
                    InvoiceNumber = inv.InvoiceNumber,
                    IsMailSent = inv.IsMailSent,
                    CustomerEmail = inv.Customer.Email,
                    CustomerId = inv.CustomerRef,
                    Amount = inv.Amount,
                    PaymentStatus = inv.PaymentStatus

                }).ToList();

                return invoiceList;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }
            
        }

        public List<InvoiceModel> FilterInvoicesByEmail(string Email)
        {
            try
            {
                List<Invoice> Invoices = context.Invoices.ToList();
                List<InvoiceModel> invoiceList = new List<InvoiceModel>();
                invoiceList = (List<InvoiceModel>)Invoices.Select(inv => new InvoiceModel()
                {
                    Id = inv.Id,
                    invoiceDate = inv.invoiceDate,
                    InvoiceNumber = inv.InvoiceNumber,
                    IsMailSent = inv.IsMailSent,
                    CustomerEmail = inv.Customer.Email,
                    CustomerId = inv.CustomerRef,
                    Amount = inv.Amount,
                    PaymentStatus = inv.PaymentStatus

                }).Where(invoice => invoice.CustomerEmail == Email).ToList();

                return invoiceList;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }

        }

        public List<InvoiceModel> FilterInvoicesInvoiceNumber(string invoiceNumber)
        {
            try
            {
                List<Invoice> Invoices = context.Invoices.ToList();
                List<InvoiceModel> invoiceList = new List<InvoiceModel>();
                invoiceList = (List<InvoiceModel>)Invoices.Select(inv => new InvoiceModel()
                {
                    Id = inv.Id,
                    invoiceDate = inv.invoiceDate,
                    InvoiceNumber = inv.InvoiceNumber,
                    IsMailSent = inv.IsMailSent,
                    CustomerEmail = inv.Customer.Email,
                    CustomerId = inv.CustomerRef,
                    Amount = inv.Amount,
                    PaymentStatus = inv.PaymentStatus

                }).Where(invoice => invoice.InvoiceNumber == invoiceNumber).ToList();

                return invoiceList;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }

        }



        public bool SaveInvoiceData(InvoiceModel invoice)
        {
            try
            {
                if (context.Invoices.Any(inv => inv.InvoiceNumber == invoice.InvoiceNumber || inv.Id == invoice.Id))
                {
                    var invoice_ = context.Invoices.First( inv => inv.InvoiceNumber == invoice.InvoiceNumber);
                    invoice_.InvoiceNumber = invoice.InvoiceNumber;
                    invoice_.invoiceDate = invoice.invoiceDate;
                    invoice_.IsMailSent = invoice.IsMailSent ? invoice.IsMailSent : false;
                    invoice_.PaymentStatus = invoice.PaymentStatus;
                    invoice_.CustomerRef = invoice.CustomerId;
                    invoice_.Amount = invoice.Amount;
                    //context.Entry(invoice).State = EntityState.Modified;
                }
                else
                {
                    Invoice inv = new Invoice();
                    inv.InvoiceNumber = invoice.InvoiceNumber;
                    inv.invoiceDate = invoice.invoiceDate ;
                    inv.IsMailSent = invoice.IsMailSent ? invoice.IsMailSent : false;
                    inv.PaymentStatus = invoice.PaymentStatus;
                    inv.CustomerRef = invoice.CustomerId;
                    inv.Amount = invoice.Amount;
                    context.Invoices.Add(inv);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveSelectedInvoice(string invoiceNumber)
        {
            try
            {
                var invoice = context.Invoices.Single(inv => inv.InvoiceNumber == invoiceNumber);
                context.Entry(invoice).State = EntityState.Deleted;

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        }
}