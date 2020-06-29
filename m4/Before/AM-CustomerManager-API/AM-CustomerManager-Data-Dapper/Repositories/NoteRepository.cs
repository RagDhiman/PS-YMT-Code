using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class NoteRepository : DapperBaseRepository, INoteRepository
    {
        internal const string SQL =
            @"SELECT
                 Id, NoteText, CustomerId
                FROM vwCMNote B";

        public NoteRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteNote(Note Note)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMNoteDelete", new { Note.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Note[]> GetAllNotesAsync(int CustomerId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Notes = await con.QueryAsync<Note>(sql: $"{SQL} where B.CustomerId = @CustomerId", new { CustomerId }, commandType: CommandType.Text);

                return Notes.AsList().ToArray();
            }
        }

        public async Task<Note> GetNoteAsync(int? CustomerId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Note = await con.QueryAsync<Note>(sql: $"{SQL} where B.Id = @Id and B.CustomerId = @CustomerId", new { Id, CustomerId }, commandType: CommandType.Text);

                return Note.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewNoteAsync(int CustomerId, Note Note)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Note>(sql: "spCMNoteInsert",
                    new
                    {
                        Note.NoteText,
                        Note.CustomerId
                    },
                    commandType: CommandType.StoredProcedure);

                Note.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateNoteAsync(int CustomerId, Note Note)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMNoteUpdate",
                    new
                    {
                        Note.Id,
                        Note.NoteText,
                        Note.CustomerId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}

