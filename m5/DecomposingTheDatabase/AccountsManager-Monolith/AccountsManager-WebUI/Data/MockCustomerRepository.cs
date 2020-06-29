using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsManager_WebUI.Models;

namespace AccountsManager_WebUI.Data
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            return Task.FromResult(new CustomerModel[] {
                new CustomerModel
                {
                    CustomerId = 1,
                    FirstName = "Rag",
                    LastName = "Dhiman",
                    MiddleName = "Kumar",
                    Company = "Pluralsight",
                    Title = "Mr",
                    Email = "rag.dhiman@gmail.com",
                    Mobile = "07123456789",
                    Website = "www.test.com",
                    Fax = "NA",
                    Phone = "0123456789",
                    DisplayNameAs = "Rag Dhiman",
                    Suffix = "MCP"
                },
                new CustomerModel
                {
                    CustomerId = 2,
                    FirstName = "Ayan",
                    LastName = "Singh",
                    MiddleName = "Kumar",
                    Company = "AS Ltd",
                    Title = "Mr",
                    Email = "ayan.ss@test.com",
                    Mobile = "07223456789",
                    Website = "www.ayanss.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = null,
                    Suffix = null
                },
                new CustomerModel
                {
                    CustomerId = 3,
                    FirstName = "Neha",
                    LastName = "Sagoo",
                    MiddleName = "Kaur",
                    Company = "Neha Ltd",
                    Title = "Mr",
                    Email = "neha@test.com",
                    Mobile = "07223456789",
                    Website = "www.nehaltd.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = null,
                    Suffix = null
                },
                new CustomerModel
                {
                    CustomerId = 4,
                    FirstName = "Ung",
                    LastName = "Notta D",
                    MiddleName = "Kaur",
                    Company = "Neha Ltd",
                    Title = "Mr",
                    Email = "ung@test.com",
                    Mobile = "07223456789",
                    Website = "www.nehaltd.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = null,
                    Suffix = null
                }
            }.AsEnumerable());
        }

        public Task<CustomerModel> GetCustomerAsync(int id)
        {
            return Task.FromResult(new CustomerModel
            {
                CustomerId = 1,
                FirstName = "Rag",
                LastName = "Dhiman",
                MiddleName = "Kumar",
                Company = "Pluralsight",
                Title = "Mr",
                Email = "rag.dhiman@gmail.com",
                Mobile = "07123456789",
                Website = "www.test.com",
                Fax = "NA",
                Phone = "0123456789",
                DisplayNameAs = "Rag Dhiman",
                Suffix = "MCP"
            });
        }
    }
}
