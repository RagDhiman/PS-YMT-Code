using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class EstimateRepository : DapperBaseRepository, IEstimateRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.InvoiceId,
            B.CustomerId,
            B.EstimateDate,
            B.ExpirationDate,
            B.Message
                FROM vwIMEstimates B";

        public EstimateRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteEstimate(Estimate Estimate)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMEstimateDelete", new { Estimate.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Estimate[]> GetAllEstimateesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Estimates = await con.QueryAsync<Estimate>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return Estimates.AsList().ToArray();
            }
        }

        public async Task<Estimate> GetEstimateAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Estimate = await con.QueryAsync<Estimate>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Estimate.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewEstimateAsync(int InvoiceId, Estimate Estimate)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Estimate>(sql: "spIMEstimateInsert",
                    new
                    {
                        Estimate.InvoiceId,
                        Estimate.CustomerId,
                        Estimate.EstimateDate,
                        Estimate.ExpirationDate,
                        Estimate.Message
                    },
                    commandType: CommandType.StoredProcedure);

                Estimate.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateEstimateAsync(int InvoiceId, Estimate Estimate)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spIMEstimateUpdate",
                    new
                    {
                        Estimate.Id,
                        Estimate.InvoiceId,
                        Estimate.CustomerId,
                        Estimate.EstimateDate,
                        Estimate.ExpirationDate,
                        Estimate.Message
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
