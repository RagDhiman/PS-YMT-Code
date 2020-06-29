using AccountsManager_Domain.DataAccess;
using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public interface ICustomerCreditService
    {
        Task<Credit> ProcessCreditWithBankAsync(Credit credit);
        Task<bool> SaveAndProcessCredit(ICreditRepository creditRepositery, int invoiceId, Credit credit);
    }
}