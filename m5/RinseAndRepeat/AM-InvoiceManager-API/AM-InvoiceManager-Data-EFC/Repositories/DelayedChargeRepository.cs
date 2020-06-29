using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class DelayedChargeRepository : DapperBaseRepository, IDelayedChargeRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.InvoiceId,
            B.CustomerId,
            B.DelayedChargeDate,
            B.Message
            FROM vwIMDelayedCharges B";

        public DelayedChargeRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteDelayedCharge(DelayedCharge DelayedCharge)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMDelayedChargeDelete", new { DelayedCharge.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<DelayedCharge[]> GetAllDelayedChargeesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var DelayedCharges = await con.QueryAsync<DelayedCharge>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return DelayedCharges.AsList().ToArray();
            }
        }

        public async Task<DelayedCharge> GetDelayedChargeAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var DelayedCharge = await con.QueryAsync<DelayedCharge>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return DelayedCharge.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<DelayedCharge>(sql: "spIMDelayedChargeInsert",
                    new
                    {
                        DelayedCharge.InvoiceId,
                        DelayedCharge.CustomerId,
                        DelayedCharge.DelayedChargeDate,
                        DelayedCharge.Message
                    },
                    commandType: CommandType.StoredProcedure);

                DelayedCharge.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateDelayedChargeAsync(int InvoiceId, DelayedCharge DelayedCharge)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMDelayedChargeUpdate",
                    new
                    {
                        DelayedCharge.Id,
                        DelayedCharge.InvoiceId,
                        DelayedCharge.CustomerId,
                        DelayedCharge.DelayedChargeDate,
                        DelayedCharge.Message
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
