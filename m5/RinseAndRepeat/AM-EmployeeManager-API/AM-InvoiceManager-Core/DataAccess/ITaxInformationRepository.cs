using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface ITaxInformationRepository
    {
        Task<TaxInformation[]> GetAllTaxInformationesAsync(int EmployeeId);
        Task<TaxInformation> GetTaxInformationAsync(int? EmployeeId, int? id);
        Task<bool> StoreNewTaxInformationAsync(int EmployeeId, TaxInformation TaxInformation);
        Task<bool> UpdateTaxInformationAsync(int EmployeeId, TaxInformation TaxInformation);
        Task<bool> DeleteTaxInformation(TaxInformation TaxInformation);
    }
}
