using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class PaymentBillingRepository : DapperBaseRepository, IPaymentBillingRepository
    {
        internal const string SQL =
            @"SELECT
                 Id, CustomerId, PrefferedMethod, Terms, OpeningBalance
                FROM vwCMPaymentBilling B";

        public PaymentBillingRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeletePaymentBilling(PaymentBilling PaymentBilling)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMPaymentBillingDelete", new { PaymentBilling.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<PaymentBilling[]> GetAllPaymentBillingesAsync(int CustomerId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var PaymentBillings = await con.QueryAsync<PaymentBilling>(sql: $"{SQL} where B.CustomerId = @CustomerId", new { CustomerId }, commandType: CommandType.Text);

                return PaymentBillings.AsList().ToArray();
            }
        }

        public async Task<PaymentBilling> GetPaymentBillingAsync(int? CustomerId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var PaymentBilling = await con.QueryAsync<PaymentBilling>(sql: $"{SQL} where B.Id = @Id and B.CustomerId = @CustomerId", new { Id, CustomerId }, commandType: CommandType.Text);

                return PaymentBilling.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewPaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<PaymentBilling>(sql: "spCMPaymentBillingInsert",
                    new
                    {
                        PaymentBilling.CustomerId,
                        PaymentBilling.PrefferedMethod,
                        PaymentBilling.Terms,
                        PaymentBilling.OpeningBalance
                    },
                    commandType: CommandType.StoredProcedure);

                PaymentBilling.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdatePaymentBillingAsync(int CustomerId, PaymentBilling PaymentBilling)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMPaymentBillingUpdate",
                    new
                    {
                        PaymentBilling.Id,
                        PaymentBilling.CustomerId,
                        PaymentBilling.PrefferedMethod,
                        PaymentBilling.Terms,
                        PaymentBilling.OpeningBalance
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
