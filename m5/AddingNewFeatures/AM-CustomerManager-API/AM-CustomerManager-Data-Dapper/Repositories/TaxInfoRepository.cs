using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class TaxInfoRepository : DapperBaseRepository, ITaxInfoRepository
    {
        internal const string SQL =
            @"SELECT
                 Id, TaxRegNo, UTRNo, CustomerId
                FROM vwCMTaxInfo B";

        public TaxInfoRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteTaxInfo(TaxInfo TaxInfo)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMTaxInfoDelete", new { TaxInfo.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<TaxInfo[]> GetAllTaxInfoesAsync(int CustomerId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var TaxInfos = await con.QueryAsync<TaxInfo>(sql: $"{SQL} where B.CustomerId = @CustomerId", new { CustomerId }, commandType: CommandType.Text);

                return TaxInfos.AsList().ToArray();
            }
        }

        public async Task<TaxInfo> GetTaxInfoAsync(int? CustomerId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var TaxInfo = await con.QueryAsync<TaxInfo>(sql: $"{SQL} where B.Id = @Id and B.CustomerId = @CustomerId", new { Id, CustomerId }, commandType: CommandType.Text);

                return TaxInfo.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewTaxInfoAsync(int CustomerId, TaxInfo TaxInfo)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<TaxInfo>(sql: "spCMTaxInfoInsert",
                    new
                    {
                        TaxInfo.TaxRegNo,
                        TaxInfo.UTRNo,
                        TaxInfo.CustomerId
                    },
                    commandType: CommandType.StoredProcedure);

                TaxInfo.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateTaxInfoAsync(int CustomerId, TaxInfo TaxInfo)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMTaxInfoUpdate",
                    new
                    {
                        TaxInfo.Id,
                        TaxInfo.TaxRegNo,
                        TaxInfo.UTRNo,
                        TaxInfo.CustomerId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
