using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class CreditRepository : DapperBaseRepository, ICreditRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.InvoiceId,
                B.CreditDate,
                B.CreditAmount,
                B.ProductCredit,
                B.CustomerName,
                B.AccountNo,
                B.SortCode,
                B.HasCreditAgreement
                FROM vwIMCredits B";

        public CreditRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteCredit(Credit Credit)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMCreditDelete", new { Credit.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Credit[]> GetAllCreditesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Credits = await con.QueryAsync<Credit>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return Credits.AsList().ToArray();
            }
        }

        public async Task<Credit> GetCreditAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Credit = await con.QueryAsync<Credit>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Credit.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewCreditAsync(int InvoiceId, Credit Credit)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Credit>(sql: "spIMCreditInsert",
                    new
                    {
                        Credit.InvoiceId,
                        Credit.CreditDate,
                        Credit.CreditAmount,
                        Credit.ProductCredit,
                        Credit.CustomerName,
                        Credit.AccountNo,
                        Credit.SortCode,
                        Credit.HasCreditAgreement
                    },
                    commandType: CommandType.StoredProcedure);

                Credit.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateCreditAsync(int InvoiceId, Credit Credit)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMCreditUpdate",
                    new
                    {
                        Credit.Id,
                        Credit.InvoiceId,
                        Credit.CreditDate,
                        Credit.CreditAmount,
                        Credit.ProductCredit,
                        Credit.CustomerName,
                        Credit.AccountNo,
                        Credit.SortCode,
                        Credit.HasCreditAgreement
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
