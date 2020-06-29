using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_NotificationsService_Data_EFC.Repositories
{
    public class WebhookPostRepository : DapperBaseRepository, IWebhookPostRepository
    {
        internal const string SQL =
            @"SELECT
                    B.Id,
                    B.URL,
                    B.Sender,
                    B.Body,
                    B.PostDateTime
                FROM vwNSWebhookPosts B";

        public WebhookPostRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteWebhookPost(WebhookPost WebhookPost)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSWebhookPostDelete", new { WebhookPost.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<WebhookPost[]> GetAllWebhookPostsAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var WebhookPost = await con.QueryAsync<WebhookPost>(sql: $"{SQL}", commandType: CommandType.Text);

                return WebhookPost.AsList().ToArray();
            }
        }

        public async Task<WebhookPost> GetWebhookPostAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var WebhookPost = await con.QueryAsync<WebhookPost>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return WebhookPost.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewWebhookPostAsync(WebhookPost WebhookPost)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<WebhookPost>(sql: "spNSWebhookPostInsert",
                    new
                    {
                        WebhookPost.URL,
                        WebhookPost.Sender,
                        WebhookPost.Body,
                        WebhookPost.PostDateTime
                    },
                    commandType: CommandType.StoredProcedure);

                WebhookPost.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateWebhookPostAsync(WebhookPost WebhookPost)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spNSWebhookPostUpdate",
                    new
                    {
                        WebhookPost.Id,
                        WebhookPost.URL,
                        WebhookPost.Sender,
                        WebhookPost.Body,
                        WebhookPost.PostDateTime
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
