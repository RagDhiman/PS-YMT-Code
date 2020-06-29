using AccountsManager_Data;
using AccountsManager_Data.Repositories;
using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_API.Workers
{
    public class ExpenseMessageWorker : RabMQServiceBase
    {
        private readonly ILogger<ExpenseMessageWorker> _logger;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseMessageWorker(ILogger<ExpenseMessageWorker> logger, 
            IConfiguration configuration) : base(logger, configuration)
        {
            try
            {
                base.QueueName = "AccountManager.ExpensesIn";
                _logger = logger;

                var optionsBuilder = new DbContextOptionsBuilder<AccountManagerContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AccountManagerConnection"));

                _expenseRepository = new ExpenseRepository(new AccountManagerContext(optionsBuilder.Options));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public override bool Process(string message)
        {
            _logger.LogInformation(message);

            try
            {
                Expense expense = JsonConvert.DeserializeObject<Expense>(message);
                _expenseRepository.StoreNewExpenseAsync(expense);
            }
            catch (Exception e) {
                _logger.LogError(e,e.Message);
            }

            return true;
        }
    }
}
