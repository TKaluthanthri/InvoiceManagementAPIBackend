using InvoiceManagementAPI.Controllers;
using InvoiceManagementAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController customerController;
        public CustomerControllerTest()
        {
            ICustomerRepository repo = new CustomerRepository();
            customerController = new CustomerController(repo);
        }

        [TestMethod]
        public void SaveCustomer()
        {
            Customer customerObject = new Customer();
            customerObject.FirstName = "Merry";
            customerObject.LastName = "Brandybuck";
            customerObject.Address = "Shire";
            customerObject.Email = "brandybuck@mailinator.com";
            customerObject.Createddate = DateTime.Now;
            var result = customerController.SaveCustomer(customerObject);

            Assert.IsNotNull("200 OK", result.StatusCode.ToString());
        }

        [TestMethod]
        public void GetAllCustomers()
        {
            var result = customerController.GetAllCustomers().Content.ReadAsStringAsync().Result;

            List<CustomerModelTest> customers = JsonConvert.DeserializeObject<List<CustomerModelTest>>(result);

            Assert.IsNotNull(result);
            Assert.AreEqual("Frodo", customers[2].FirstName.ToString());
            Assert.AreEqual("Samwise", customers[3].FirstName.ToString());

        }


        [TestMethod]
        public void SearchCustomerByName()
        {
            var result = customerController.GetCustomersByName("Frodo").Content.ReadAsStringAsync().Result;
            CustomerModelTest customer = JsonConvert.DeserializeObject<CustomerModelTest>(result);

            Assert.IsNotNull(result);
            Assert.AreEqual("Frodo", customer.FirstName.ToString());
            Assert.AreEqual("baggins", customer.LastName.ToString());
        }

    }

}
