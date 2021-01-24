using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Repositories
{
    public interface ICustomerRepository
    {
        List<CustomerModel> GetAllCustomers();
        bool SaveCustomerDetails(Customer customer);
        CustomerModel GetCustomerByName(string searchText);
        List<string> GetCustomerEmailList();
        CustomerModel GetCustomerByEmail(string searchText);
    }
}
