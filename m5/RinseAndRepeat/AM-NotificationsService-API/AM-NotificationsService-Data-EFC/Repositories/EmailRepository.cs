using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_NotificationsService_Data_EFC.Repositories
{
    public class EmailRepository : DapperBaseRepository, IEmailRepository
    {
        internal const string SQL =
            @"SELECT
                    B.Id,
                    B.SendTo,
                    B.Sender,
                    B.Subject,
                    B.Message,
                    B.SentDateTime,
                    B.DeliveredDateTime
                FROM vwNSEmails B";

        public EmailRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteEmail(Email Email)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSEmailDelete", new { Email.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Email[]> GetAllEmailsAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Email = await con.QueryAsync<Email>(sql: $"{SQL}", commandType: CommandType.Text);

                return Email.AsList().ToArray();
            }
        }

        public async Task<Email> GetEmailAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Email = await con.QueryAsync<Email>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Email.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewEmailAsync(Email Email)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Email>(sql: "spNSEmailInsert",
                    new
                    {
                        Email.SendTo,
                        Email.Sender,
                        Email.Subject,
                        Email.Message,
                        Email.SentDateTime,
                        Email.DeliveredDateTime
                    },
                    commandType: CommandType.StoredProcedure);

                Email.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateEmailAsync(Email Email)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSEmailUpdate",
                    new
                    {
                        Email.Id,
                        Email.SendTo,
                        Email.Sender,
                        Email.Subject,
                        Email.Message,
                        Email.SentDateTime,
                        Email.DeliveredDateTime
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
