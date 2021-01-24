using InvoiceManagementAPI.DBOperations;
using InvoiceManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InvoiceManagementAPI.Repositories
{
    public class CustomerRepository : ApiController, ICustomerRepository
    {
            
        private InvoiceManagementEntities1 dbContext = new InvoiceManagementEntities1();
        //public CustomerRepository(InvoiceManagementEntities contex)
        //{
        //    this.dbContext = contex;
        //}

        public List<CustomerModel> GetAllCustomers()
        {
            try
            {
                List<Customer> customers =  dbContext.Customers.ToList();
                List<CustomerModel> customersList = new List<CustomerModel>();
                customersList = customers.Select(x => new CustomerModel()
                {
                    //do your variable mapping here 
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    Email =x.Email,
                    Createddate = x.Createddate
                    
                }).ToList();

                return customersList;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }
        }

        public bool SaveCustomerDetails(Customer customer)
        {
            try
            {
                if (dbContext.Customers.Any(x => x.Email == customer.Email))
                {
                    dbContext.Entry(customer).State = EntityState.Modified;
                }
                else
                {
                    customer.Createddate = DateTime.Now;
                    dbContext.Customers.Add(customer);
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CustomerModel GetCustomerByName(string searchText)
        {
            try
            {
                CustomerModel customer = new CustomerModel();
                var customer_ = dbContext.Customers.FirstOrDefault(cus => cus.FirstName.Contains(searchText.Trim()) || cus.LastName.Contains(searchText.Trim()));
               
                customer.Id = customer_.Id;
                customer.FirstName = customer_.FirstName;
                customer.LastName = customer_.LastName;
                customer.Address = customer_.Address;
                customer.Email = customer_.Email;
                customer.Createddate = customer_.Createddate;
                
                return customer;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }
        }


        public List<string> GetCustomerEmailList()
        {
            try
            {
                List<string> emailList = new List<string>();
                emailList = dbContext.Customers.Select(cus => cus.Email).Distinct().ToList<string>();
                return emailList;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }

        public CustomerModel GetCustomerByEmail(string searchText)
        {
            try
            {
                CustomerModel customer = new CustomerModel();
                var customer_ = dbContext.Customers.FirstOrDefault(cus => cus.Email.Contains(searchText.Trim()) || cus.LastName.Contains(searchText.Trim()));

                customer.Id = customer_.Id;
                customer.FirstName = customer_.FirstName;
                customer.LastName = customer_.LastName;
                customer.Address = customer_.Address;
                customer.Email = customer_.Email;
                customer.Createddate = customer_.Createddate;

                return customer;
            }
            catch (Exception ex)
            {
                // TO DO Add Nlog and log exceptions 
                ex.Message.ToString();
                throw ex;
            }
        }
    }

}