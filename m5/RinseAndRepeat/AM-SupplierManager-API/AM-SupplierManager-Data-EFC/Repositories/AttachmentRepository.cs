using AM_SupplierManager_Core;
using AM_SupplierManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class AttachmentRepository : DapperBaseRepository, IAttachmentRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.FilePath,
                B.SupplierId
            FROM vwSMAttachments B";

        public AttachmentRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteAttachment(Attachment Attachment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMAttachmentsDelete", new { Attachment.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Attachment[]> GetAllAttachmentsAsync(int SupplierId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Attachments = await con.QueryAsync<Attachment>(sql: $"{SQL} where B.SupplierId = @SupplierId", new { SupplierId }, commandType: CommandType.Text);

                return Attachments.AsList().ToArray();
            }
        }

        public async Task<Attachment> GetAttachmentAsync(int? SupplierId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Attachment = await con.QueryAsync<Attachment>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Attachment.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewAttachmentAsync(int SupplierId, Attachment Attachment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Attachment>(sql: "spSMAttachmentsInsert",
                    new
                    {
                        Attachment.FilePath,
                        Attachment.SupplierId
                    },
                    commandType: CommandType.StoredProcedure);

                Attachment.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateAttachmentAsync(int SupplierId, Attachment Attachment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMAttachmentsUpdate",
                    new
                    {
                        Attachment.Id,
                        Attachment.FilePath,
                        Attachment.SupplierId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
