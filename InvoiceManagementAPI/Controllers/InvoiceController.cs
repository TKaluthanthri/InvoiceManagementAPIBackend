using InvoiceManagementAPI.EmailSendingService;
using InvoiceManagementAPI.Models;
using InvoiceManagementAPI.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace InvoiceManagementAPI.Controllers
{
    public class InvoiceController : ApiController
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IEmailRepository emailRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IEmailRepository emailRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.customerRepository = customerRepository;
            this.emailRepository = emailRepository;
        }

        [HttpGet]
        [Route("api/invoice/getAllInvoices")]
        public HttpResponseMessage GetAllInvoices()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                string payLoad = JsonConvert.SerializeObject(invoiceRepository.GetAllInvoices());
                response.Content = new StringContent(payLoad, Encoding.UTF8, "application/json");
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }

        }


        [HttpGet]
        [Route("api/invoice/getAllCustomerEmails")]
        public HttpResponseMessage GetAllCustomerEmail()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var emailList = customerRepository.GetCustomerEmailList();
                response.StatusCode = HttpStatusCode.OK;
                return Request.CreateResponse(response.StatusCode, emailList);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }

        }


        [HttpGet]
        [Route("api/invoice/filterInvoiceByInvoiceNumber")]
        public HttpResponseMessage FilterInvoiceByInvoiceNumber(string invoiceNumber)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    var invoices = invoiceRepository.FilterInvoicesInvoiceNumber(invoiceNumber);
                    response.StatusCode = HttpStatusCode.OK;
                    return Request.CreateResponse(response.StatusCode, invoices);
                }
                else {
                    response.Content = new StringContent("Invoice Number cannot be null", System.Text.Encoding.UTF8, "application/json");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }

        }



        [HttpPost]
        [Route("api/invoice/saveInvoiceDetails")]
        public HttpResponseMessage SaveInvoiceDetails(InvoiceModel invoice)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (invoice != null) {
                    if (invoice.CustomerId == 0 || !String.IsNullOrEmpty(invoice.CustomerEmail))
                    {
                        CustomerModel customer = customerRepository.GetCustomerByEmail(invoice.CustomerEmail);
                        invoice.CustomerId = customer.Id;
                        if (invoice.IsMailSent)
                        {
                            emailRepository.SendInvoiceEmail(invoice, customer);
                        }
                        var result =  invoiceRepository.SaveInvoiceData(invoice);
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else {
                        return  Request.CreateResponse(HttpStatusCode.NoContent, "Invoice Is Not Assigned to a Customer");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.NoContent, "Invoice Details Empty!!");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }
        }

        [HttpGet]
        [Route("api/invoice/getInvoicesByEmail")]
        public HttpResponseMessage GetInvoicesByEmail(string Email)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    string payLoad = JsonConvert.SerializeObject(invoiceRepository.FilterInvoicesByEmail(Email.Trim()));
                    response.Content = new StringContent(payLoad, Encoding.UTF8, "application/json");
                    response.StatusCode = HttpStatusCode.OK;
                }
                else {
                    response.Content = new StringContent(null, Encoding.UTF8, "application/json");
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }

        }


        [HttpDelete]
        [Route("api/invoice/deleteInvoice")]
        public HttpResponseMessage DeleteInvoice(string InvoiceNumber)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(InvoiceNumber))
                {
                    string payLoad = JsonConvert.SerializeObject(invoiceRepository.RemoveSelectedInvoice(InvoiceNumber));
                    response.Content = new StringContent(payLoad, Encoding.UTF8, "application/json");
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Content = new StringContent("No Invoice Number Provided", Encoding.UTF8, "application/json");
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred", System.Text.Encoding.UTF8, "application/json");
                return Request.CreateResponse(response.StatusCode, response.Content);
            }

        }

    }

}
