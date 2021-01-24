using InvoiceManagementAPI.Controllers;
using InvoiceManagementAPI.EmailSendingService;
using InvoiceManagementAPI.Models;
using InvoiceManagementAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Tests.Controllers
{
    [TestClass]
    public class InvoiceControllerTest
    {
        private InvoiceController invoiceController;
        public InvoiceControllerTest()
        {
            ICustomerRepository customerRepo = new CustomerRepository();
            IInvoiceRepository invoiceRepo = new InvoiceRepository();
            IEmailRepository emailRepo = new EmailRepository();
            invoiceController = new InvoiceController(invoiceRepo,customerRepo, emailRepo);
        }

        [TestMethod]
        public void GetAllInvoices()
        {
            var result = invoiceController.GetAllInvoices().Content.ReadAsStringAsync().Result;
            List<InvoiceModelTest> invoices = JsonConvert.DeserializeObject<List<InvoiceModelTest>>(result);
            Assert.IsTrue(invoices.Any());
        }

        [TestMethod]
        public void SaveInvoice()
        {
            InvoiceModel invoice = new InvoiceModel();
            invoice.invoiceDate = DateTime.Now;
            invoice.Amount = 23000;
            invoice.CustomerEmail = "baggins@mailinator.com";
            invoice.InvoiceNumber = "INV0008";
            invoice.IsMailSent = false;
            invoice.PaymentStatus = "Paid";
            invoice.CustomerId = 0;
            var result = invoiceController.SaveInvoiceDetails(invoice);

            Assert.IsNotNull("200 OK", result.StatusCode.ToString());
        }


        [TestMethod]
        public void GetAllInvoiceByEmail()
        {
            var result = invoiceController.GetInvoicesByEmail("gamgee@mailinator.com").Content.ReadAsStringAsync().Result;
            List<InvoiceModel> invoices = JsonConvert.DeserializeObject<List<InvoiceModel>>(result);
            Assert.AreEqual("gamgee@mailinator.com", invoices.First().CustomerEmail.Trim());
        }

        [TestMethod]
        public void EmailSendingServiceTest()
        {
            EmailBodyContent emailbody = new EmailBodyContent();
            emailbody.FirstName = "Frodo";
            emailbody.LastName = "Baggins";
            emailbody.Date = DateTime.Now.Date.ToString();
            emailbody.Amount = "$23789.90";
            emailbody.InvoiceNumber = "INV001";
            emailbody.Address = "Test Addresss";

            EmailModel emailmodel = new EmailModel();
            emailmodel.EmailSender = "thilinik15@gmail.com";
            emailmodel.EmailReceiver = "baggines@mailinator.com";
            emailmodel.EmailSubject = "Invoice Statement";
            emailmodel.EmailBody = "<link href =\"//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css\" rel=\"stylesheet\" id=\"bootstrap-css\"><script src=\"//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js\"></script><script src=\"//code.jquery.com/jquery-1.11.1.min.js\"></script><!------ Include the above in your HEAD tag ----------><!DOCTYPE html><html lang=\"en\"><head>  <title>Bootstrap Example</title>  <meta charset=\"utf-8\">  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">  <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\">  <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js\"></script></head><body><div style=\"margin:5% 40% 0% 33%;float:left; width:500px; box-shadow:0 0 3px #aaa; height:auto;color:#666;\">   <div style=\"width:100%; padding:10px; float:left; background:#1ca8dd; color:#fff; font-size:30px; text-align:center;\">	Invoice Details   </div>    <div style=\"width:100%; padding:0px 0px;border-bottom:1px solid rgba(0,0,0,0.2);float:left;\">        <div style=\"width:30%; float:left;padding:10px;\">                       <span style=\"font-size:14px;float:left; width:100%;\">                {{FirstName}} {{LastName}}            </span>			 <span style=\"font-size:14px;float:left;width:100%;\">                 {{Address}}            </span>			        </div>		        <div style=\"width:40%; float:right;padding:\">            <span style=\"font-size:14px;float:right;  padding:10px; text-align:right;\">                <b>Date : </b>{{Date}}            </span>			<span style=\"font-size:14px;float:right;  padding:10px; text-align:right;\">               <b>Invoice# : </b>{{Invoice Number}}            </span>        </div>    </div>                <div style=\"width:100%; padding:0px; float:left;\">               <div style=\"width:100%;float:left;background:#efefef;\">            <span style=\"float:left; text-align:left;padding:10px;width:50%;color:#888;font-weight:600;\">            Decription           </span>         <span style=\"font-weight:600;float:left;padding:10px ;width:40%;color:#888;text-align:right;\">			Amount        </span>      </div>	       	  	  	  	   <div style=\"width:100%;float:left; background:#fff;\">                    <span style=\"font-weight:600;float:right;padding:10px 0px;width:40%;color:#666;text-align:center;\">           Amount To Pay : {{Amount}}        </span>      </div>    </div></body></html>";

           
            bool status = EmailService.SendInvoiceAsSMTPEmail(emailmodel, emailbody);
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void TestRemoveInvoice() {

            var result = invoiceController.DeleteInvoice("INV001").Content.ReadAsStringAsync().Result;
            List<InvoiceModel> invoices = JsonConvert.DeserializeObject<List<InvoiceModel>>(result);
            Assert.IsNull("INV001", invoices.Select(x => x.InvoiceNumber = "INV001").FirstOrDefault());
        }


    }
}
