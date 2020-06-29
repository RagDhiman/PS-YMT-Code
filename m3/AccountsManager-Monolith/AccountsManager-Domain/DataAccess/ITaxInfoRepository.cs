using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface ITaxInfoRepository
    {
        Task<TaxInfo[]> GetAllTaxInfoesAsync(int CustomerId);
        Task<TaxInfo> GetTaxInfoAsync(int CustomerId, int id);
        Task<bool> StoreNewTaxInfoAsync(int CustomerId, TaxInfo TaxInfo);
        Task<bool> UpdateTaxInfoAsync(int CustomerId, TaxInfo TaxInfo);
        Task<bool> DeleteTaxInfo(TaxInfo TaxInfo);
    }
}
