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
    public class AddressRepository: IAddressRepository
    {
        private readonly AccountManagerContext _context;

        public AddressRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Address[]> GetAllAddressesAsync(int CustomerId)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query
                .Where(a => a.CustomerId == CustomerId)    
                .OrderByDescending(a => a.Street)
                    .ThenByDescending(a => a.PostCode);

            return await query.ToArrayAsync();
        }

        public async Task<Address> GetAddressAsync(int CustomerId, int id)
        {
            IQueryable<Address> query = _context.Addresses;

            return await query.Where(a => a.CustomerId == CustomerId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewAddressAsync(int CustomerId, Address Address)
        {
            Address.CustomerId = CustomerId;

            _context.Addresses.Add(Address);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAddressAsync(int CustomerId, Address Address)
        {
            Address.CustomerId = CustomerId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAddress(Address Address)
        {
            _context.Remove(Address);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
