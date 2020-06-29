using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class InvoiceLineRepository : DapperBaseRepository, IInvoiceLineRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.InvoiceId,
            B.Service,
            B.Quantity,
            B.Rate,
            B.VAT
                FROM vwIMInvoiceLines B";

        public InvoiceLineRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteInvoiceLine(InvoiceLine InvoiceLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMInvoiceLineDelete", new { InvoiceLine.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<InvoiceLine[]> GetAllInvoiceLineesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var InvoiceLines = await con.QueryAsync<InvoiceLine>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return InvoiceLines.AsList().ToArray();
            }
        }

        public async Task<InvoiceLine> GetInvoiceLineAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var InvoiceLine = await con.QueryAsync<InvoiceLine>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return InvoiceLine.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<InvoiceLine>(sql: "spIMInvoiceLineInsert",
                    new
                    {
                        InvoiceLine.InvoiceId,
                        InvoiceLine.Service,
                        InvoiceLine.Quantity,
                        InvoiceLine.Rate,
                        InvoiceLine.VAT
                    },
                    commandType: CommandType.StoredProcedure);

                InvoiceLine.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateInvoiceLineAsync(int InvoiceId, InvoiceLine InvoiceLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMInvoiceLineUpdate",
                    new
                    {
                        InvoiceLine.Id,
                        InvoiceLine.InvoiceId,
                        InvoiceLine.Service,
                        InvoiceLine.Quantity,
                        InvoiceLine.Rate,
                        InvoiceLine.VAT
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
