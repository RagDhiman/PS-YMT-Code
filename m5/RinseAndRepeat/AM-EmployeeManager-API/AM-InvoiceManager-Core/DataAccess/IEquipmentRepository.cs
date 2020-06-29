using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface IEquipmentRepository
    {
        Task<Equipment[]> GetAllEquipmentesAsync(int CustomerId);
        Task<Equipment> GetEquipmentAsync(int? CustomerId, int? id);
        Task<bool> StoreNewEquipmentAsync(int CustomerId, Equipment Equipment);
        Task<bool> UpdateEquipmentAsync(int CustomerId, Equipment Equipment);
        Task<bool> DeleteEquipment(Equipment Equipment);
    }
}
