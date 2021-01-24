using InvoiceManagementAPI.EmailSendingService;
using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InvoiceManagementAPI.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public EmailRepository() 
        {
            
        }
        public bool SendInvoiceEmail(InvoiceModel invoice , CustomerModel customer) {

            bool status = false;
            EmailBodyContent emailbody = new EmailBodyContent();
            EmailModel emailSettings = new EmailModel();
            

            emailbody.FirstName = customer.FirstName.Trim();
            emailbody.LastName = customer.LastName.Trim();
            emailbody.Date = DateTime.Now.Date.ToString();
            emailbody.Amount = invoice.Amount.ToString();
            emailbody.InvoiceNumber = invoice.InvoiceNumber.Trim().ToString();

            emailSettings.EmailSender = ConfigurationManager.AppSettings["smtpEmail"].ToString();
            emailSettings.EmailReceiver = invoice.CustomerEmail.Trim().ToString();
            emailSettings.EmailSubject = "Invoice Statement";
            emailSettings.EmailBody = "<link href =\"//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css\" rel=\"stylesheet\" id=\"bootstrap-css\"><script src=\"//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js\"></script><script src=\"//code.jquery.com/jquery-1.11.1.min.js\"></script><!------ Include the above in your HEAD tag ----------><!DOCTYPE html><html lang=\"en\"><head>  <title>Bootstrap Example</title>  <meta charset=\"utf-8\">  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">  <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\">  <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js\"></script></head><body><div style=\"margin:5% 40% 0% 33%;float:left; width:500px; box-shadow:0 0 3px #aaa; height:auto;color:#666;\">   <div style=\"width:100%; padding:10px; float:left; background:#1ca8dd; color:#fff; font-size:30px; text-align:center;\">	Invoice Details   </div>    <div style=\"width:100%; padding:0px 0px;border-bottom:1px solid rgba(0,0,0,0.2);float:left;\">        <div style=\"width:30%; float:left;padding:10px;\">                       <span style=\"font-size:14px;float:left; width:100%;\">                {{FirstName}} {{LastName}}            </span>			 <span style=\"font-size:14px;float:left;width:100%;\">                 {{Address}}            </span>			        </div>		        <div style=\"width:40%; float:right;padding:\">            <span style=\"font-size:14px;float:right;  padding:10px; text-align:right;\">                <b>Date : </b>{{Date}}            </span>			<span style=\"font-size:14px;float:right;  padding:10px; text-align:right;\">               <b>Invoice# : </b>{{Invoice Number}}            </span>        </div>    </div>                <div style=\"width:100%; padding:0px; float:left;\">               <div style=\"width:100%;float:left;background:#efefef;\">            <span style=\"float:left; text-align:left;padding:10px;width:50%;color:#888;font-weight:600;\">            Decription           </span>         <span style=\"font-weight:600;float:left;padding:10px ;width:40%;color:#888;text-align:right;\">			Amount        </span>      </div>	       	  	  	  	   <div style=\"width:100%;float:left; background:#fff;\">                    <span style=\"font-weight:600;float:right;padding:10px 0px;width:40%;color:#666;text-align:center;\">           Amount To Pay : {{Amount}}        </span>      </div>    </div></body></html>";

            status = EmailService.SendInvoiceAsSMTPEmail(emailSettings, emailbody);

            return status;
        }
    }
}