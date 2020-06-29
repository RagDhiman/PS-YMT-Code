using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class CreditNoteRepository : DapperBaseRepository, ICreditNoteRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.InvoiceId,
            B.CustomerId,
            B.CreditNoteDate,
            B.Message 
            FROM vwIMCreditNote B";

        public CreditNoteRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteCreditNote(CreditNote CreditNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMCreditNoteDelete", new { CreditNote.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<CreditNote[]> GetAllCreditNoteesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var CreditNotes = await con.QueryAsync<CreditNote>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return CreditNotes.AsList().ToArray();
            }
        }

        public async Task<CreditNote> GetCreditNoteAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var CreditNote = await con.QueryAsync<CreditNote>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return CreditNote.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewCreditNoteAsync(int InvoiceId, CreditNote CreditNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<CreditNote>(sql: "spIMCreditNoteInsert",
                    new
                    {
                        CreditNote.InvoiceId,
                        CreditNote.CustomerId,
                        CreditNote.CreditNoteDate,
                        CreditNote.Message
                    },
                    commandType: CommandType.StoredProcedure);

                CreditNote.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateCreditNoteAsync(int InvoiceId, CreditNote CreditNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMCreditNoteUpdate",
                    new
                    {
                        CreditNote.Id,
                        CreditNote.InvoiceId,
                        CreditNote.CustomerId,
                        CreditNote.CreditNoteDate,
                        CreditNote.Message
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
