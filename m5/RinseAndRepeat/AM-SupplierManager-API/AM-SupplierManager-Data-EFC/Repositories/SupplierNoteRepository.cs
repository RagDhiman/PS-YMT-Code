using AM_SupplierManager_Core;
using AM_SupplierManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class SupplierNoteRepository : DapperBaseRepository, ISupplierNoteRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.NoteText,
                B.SupplierId
            FROM vwSMSupplierNotes B";

        public SupplierNoteRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteSupplierNote(SupplierNote SupplierNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMSupplierNotesDelete", new { SupplierNote.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<SupplierNote[]> GetAllSupplierNotesAsync(int SupplierId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SupplierNotes = await con.QueryAsync<SupplierNote>(sql: $"{SQL} where B.SupplierId = @SupplierId", new { SupplierId }, commandType: CommandType.Text);

                return SupplierNotes.AsList().ToArray();
            }
        }

        public async Task<SupplierNote> GetSupplierNoteAsync(int? SupplierId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SupplierNote = await con.QueryAsync<SupplierNote>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return SupplierNote.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<SupplierNote>(sql: "spSMSupplierNotesInsert",
                    new
                    {
                        SupplierNote.NoteText,
                        SupplierNote.SupplierId
                    },
                    commandType: CommandType.StoredProcedure);

                SupplierNote.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateSupplierNoteAsync(int SupplierId, SupplierNote SupplierNote)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMSupplierNotesUpdate",
                    new
                    {
                        SupplierNote.Id,
                        SupplierNote.NoteText,
                        SupplierNote.SupplierId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
