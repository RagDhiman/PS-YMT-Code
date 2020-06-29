using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_NotificationsService_Data_EFC.Repositories
{
    public class SMSRepository : DapperBaseRepository, ISMSRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.SendTo, 
                B.Sender,
                B.Message,
                B.SentDateTime,
                B.DeliveredDateTime
                FROM vwNSSMSs B";

        public SMSRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteSMS(SMS SMS)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSSMSDelete", new { SMS.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<SMS[]> GetAllSMSsAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SMS = await con.QueryAsync<SMS>(sql: $"{SQL}", commandType: CommandType.Text);

                return SMS.AsList().ToArray();
            }
        }

        public async Task<SMS> GetSMSAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var SMS = await con.QueryAsync<SMS>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return SMS.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewSMSAsync(SMS SMS)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<SMS>(sql: "spNSSMSInsert",
                    new
                    {
                        SMS.SendTo,
                        SMS.Sender,
                        SMS.Message,
                        SMS.SentDateTime,
                        SMS.DeliveredDateTime
                    },
                    commandType: CommandType.StoredProcedure);

                SMS.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateSMSAsync(SMS SMS)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSSMSUpdate",
                    new
                    {
                        SMS.Id,
                        SMS.SendTo,
                        SMS.Sender,
                        SMS.Message,
                        SMS.SentDateTime,
                        SMS.DeliveredDateTime
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
