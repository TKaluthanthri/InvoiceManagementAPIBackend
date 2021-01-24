using InvoiceManagementAPI.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace InvoiceManagementAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("api/customer/getAllCustomers")]
        public HttpResponseMessage GetAllCustomers()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                string payLoad = JsonConvert.SerializeObject(customerRepository.GetAllCustomers());
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(payLoad, Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred : "+ex.Message.ToString()+"", Encoding.UTF8, "application/json");
            }
            return response;
        }


        [HttpGet]
        [Route("api/customer/getCustomerByName")]
        public HttpResponseMessage GetCustomersByName(string searchText)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                string payLoad = JsonConvert.SerializeObject(customerRepository.GetCustomerByName(searchText));
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(payLoad, Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("Error Occurred : " + ex.Message.ToString() + "", Encoding.UTF8, "application/json");
            }
            return response;
        }


        [HttpPost]
        [Route("api/customer/savecustomer")]
        public HttpResponseMessage SaveCustomer(Customer customer)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (customer != null)
                {
                    bool isSavedCustomer = customerRepository.SaveCustomerDetails(customer);
                    response.StatusCode = isSavedCustomer ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                }
                else 
                {
                    response.Content = new StringContent("Error Occurred : Customer Details Not Provided", Encoding.UTF8, "application/json");
                }
               

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
