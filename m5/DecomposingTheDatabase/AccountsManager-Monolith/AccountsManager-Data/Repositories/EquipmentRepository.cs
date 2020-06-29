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
    public class EquipmentRepository: IEquipmentRepository
    {
        private readonly AccountManagerContext _context;

        public EquipmentRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Equipment[]> GetAllEquipmentsAsync(int EmployeeId)
        {
            IQueryable<Equipment> query = _context.Equipments;

            query = query
                .Where(a => a.EmployeeId == EmployeeId);

            return await query.ToArrayAsync();
        }

        public async Task<Equipment> GetEquipmentAsync(int EmployeeId, int id)
        {
            IQueryable<Equipment> query = _context.Equipments;

            return await query.Where(a => a.EmployeeId == EmployeeId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewEquipmentAsync(int EmployeeId, Equipment Equipment)
        {
            Equipment.EmployeeId = EmployeeId;

            _context.Equipments.Add(Equipment);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateEquipmentAsync(int EmployeeId, Equipment Equipment)
        {
            Equipment.EmployeeId = EmployeeId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteEquipment(Equipment Equipment)
        {
            _context.Remove(Equipment);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
