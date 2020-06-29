using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class InvoiceRepository : DapperBaseRepository, IInvoiceRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.CustomerId,
            B.InvoiceDate,
            B.DueDate,
            B.Message
                FROM vwIMInvoices B";

        public InvoiceRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteInvoice(Invoice Invoice)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMInvoiceDelete", new { Invoice.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Invoice[]> GetAllInvoicesAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Invoices = await con.QueryAsync<Invoice>(sql: $"{SQL}", commandType: CommandType.Text);

                return Invoices.AsList().ToArray();
            }
        }

        public async Task<Invoice> GetInvoiceAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Invoice = await con.QueryAsync<Invoice>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Invoice.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewInvoiceAsync(Invoice Invoice)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Invoice>(sql: "spIMInvoiceInsert",
                    new
                    {
                        Invoice.CustomerId,
                        Invoice.InvoiceDate,
                        Invoice.DueDate,
                        Invoice.Message
                    },
                    commandType: CommandType.StoredProcedure);

                Invoice.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateInvoiceAsync(Invoice Invoice)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMInvoiceUpdate",
                    new
                    {
                        Invoice.Id,
                        Invoice.CustomerId,
                        Invoice.InvoiceDate,
                        Invoice.DueDate,
                        Invoice.Message
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
