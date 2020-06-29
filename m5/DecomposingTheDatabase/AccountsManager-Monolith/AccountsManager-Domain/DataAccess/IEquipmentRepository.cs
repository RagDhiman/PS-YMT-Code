using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IEquipmentRepository
    {
        Task<Equipment[]> GetAllEquipmentsAsync(int AccountId);
        Task<Equipment> GetEquipmentAsync(int AccountId, int id);
        Task<bool> StoreNewEquipmentAsync(int AccountId, Equipment Equipment);
        Task<bool> UpdateEquipmentAsync(int AccountId, Equipment Equipment);
        Task<bool> DeleteEquipment(Equipment Equipment);
    }
}
