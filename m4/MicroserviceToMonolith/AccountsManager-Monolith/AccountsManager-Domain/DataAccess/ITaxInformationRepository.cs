using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ITaxInformationRepository
    {
        Task<TaxInformation[]> GetAllTaxInformationsAsync(int AccountId);
        Task<TaxInformation> GetTaxInformationAsync(int AccountId, int id);
        Task<bool> StoreNewTaxInformationAsync(int AccountId, TaxInformation TaxInformation);
        Task<bool> UpdateTaxInformationAsync(int AccountId, TaxInformation TaxInformation);
        Task<bool> DeleteTaxInformation(TaxInformation TaxInformation);
    }
}
