using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccountsManager_Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AccountManagerContext _context;

        public CustomerRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Customer[]> GetAllCustomersAsync()
        {
            IQueryable<Customer> query = _context.Customers;

            query = query.OrderByDescending(c => c.LastName).ThenByDescending(c => c.FirstName);
            
            return await query.ToArrayAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            IQueryable<Customer> query = _context.Customers;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewCustomerAsync(Customer customer) {
            if (customer.AccountId <= 0) {
                customer.AccountId = 1987; //Would normally be taken of users account, but this is a demo this will do!
            }

            _context.Customers.Add(customer);
            return (await _context.SaveChangesAsync())>0;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (customer.AccountId <= 0)
            {
                customer.AccountId = 1987; //Would normally be taken of users account, but this is a demo this will do!
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCustomer(Customer customer) {
            _context.Remove(customer);
            return (await _context.SaveChangesAsync())>0;
        }

    }
}
