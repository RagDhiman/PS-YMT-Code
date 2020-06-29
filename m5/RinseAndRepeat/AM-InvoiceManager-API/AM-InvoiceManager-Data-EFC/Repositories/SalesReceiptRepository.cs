using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class SalesReceiptRepository : DapperBaseRepository, ISalesReceiptRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.InvoiceId,
                B.CustomerId,
                B.BankAccountId,
                B.SalesReceiptDate,
                B.PaymentMethod,
                B.ReferenceNo,
                B.Message
                FROM vwIMSalesReceipts B";

        public SalesReceiptRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteSalesReceipt(SalesReceipt SalesReceipt)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMSalesReceiptDelete", new { SalesReceipt.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<SalesReceipt[]> GetAllSalesReceiptesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SalesReceipts = await con.QueryAsync<SalesReceipt>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return SalesReceipts.AsList().ToArray();
            }
        }

        public async Task<SalesReceipt> GetSalesReceiptAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SalesReceipt = await con.QueryAsync<SalesReceipt>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return SalesReceipt.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<SalesReceipt>(sql: "spIMSalesReceiptInsert",
                    new
                    {
                        SalesReceipt.InvoiceId,
                        SalesReceipt.CustomerId,
                        SalesReceipt.BankAccountId,
                        SalesReceipt.SalesReceiptDate,
                        SalesReceipt.PaymentMethod,
                        SalesReceipt.ReferenceNo,
                        SalesReceipt.Message
                    },
                    commandType: CommandType.StoredProcedure);

                SalesReceipt.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateSalesReceiptAsync(int InvoiceId, SalesReceipt SalesReceipt)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMSalesReceiptUpdate",
                    new
                    {
                        SalesReceipt.Id,
                        SalesReceipt.InvoiceId,
                        SalesReceipt.CustomerId,
                        SalesReceipt.BankAccountId,
                        SalesReceipt.SalesReceiptDate,
                        SalesReceipt.PaymentMethod,
                        SalesReceipt.ReferenceNo,
                        SalesReceipt.Message
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
