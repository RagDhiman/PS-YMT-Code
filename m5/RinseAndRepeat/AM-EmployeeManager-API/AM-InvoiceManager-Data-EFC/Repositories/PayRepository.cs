using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class PayRepository : DapperBaseRepository, IPayRepository
    {
        internal const string SQL =
            @"SELECT
                        B.Id,
                        B.HourlyRate,
                        B.EmployeeId,
                        B.DefaultRate,
                        B.StartTime,
                        B.EndTime
                FROM vwEMPays B";

        public PayRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeletePay(Pay Pay)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMPayDelete", new { Pay.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Pay[]> GetAllPaysAsync(int EmployeeId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Pays = await con.QueryAsync<Pay>(sql: $"{SQL} where B.EmployeeId = @EmployeeId", new { EmployeeId }, commandType: CommandType.Text);

                return Pays.AsList().ToArray();
            }
        }

        public async Task<Pay> GetPayAsync(int? EmployeeId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Pay = await con.QueryAsync<Pay>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Pay.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewPayAsync(int EmployeeId, Pay Pay)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Pay>(sql: "spEMPayInsert",
                    new
                    {
                        Pay.HourlyRate,
                        Pay.EmployeeId,
                        Pay.DefaultRate,
                        Pay.StartTime,
                        Pay.EndTime
                    },
                    commandType: CommandType.StoredProcedure);

                Pay.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdatePayAsync(int EmployeeId, Pay Pay)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMPayUpdate",
                    new
                    {
                        Pay.Id,
                        Pay.HourlyRate,
                        Pay.EmployeeId,
                        Pay.DefaultRate,
                        Pay.StartTime,
                        Pay.EndTime
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
