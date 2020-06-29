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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AccountManagerContext _context;

        public ExpenseRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Expense[]> GetAllExpensesAsync()
        {
            IQueryable<Expense> query = _context.Expenses;

            query = query.OrderByDescending(c => c.PayeeName).ThenByDescending(c => c.PaymentDate);

            return await query.ToArrayAsync();
        }

        public async Task<Expense> GetExpenseAsync(int id)
        {
            IQueryable<Expense> query = _context.Expenses;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewExpenseAsync(Expense Expense)
        {
            _context.Expenses.Add(Expense);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateExpenseAsync(Expense Expense)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteExpense(Expense Expense)
        {
            _context.Remove(Expense);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
