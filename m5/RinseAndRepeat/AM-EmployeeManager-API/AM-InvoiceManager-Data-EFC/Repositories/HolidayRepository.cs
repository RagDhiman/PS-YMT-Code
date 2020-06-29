using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class HolidayRepository : DapperBaseRepository, IHolidayRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.EmployeeId,
                B.StartDateTime,
                B.EndDateTime,
                B.OnCall,
                B.OnCallRateMultiplier,
                B.Paid
            FROM vwEMHolidays B";

        public HolidayRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteHoliday(Holiday Holiday)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMHolidayDelete", new { Holiday.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Holiday[]> GetAllHolidayesAsync(int EmployeeId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Holidays = await con.QueryAsync<Holiday>(sql: $"{SQL} where B.EmployeeId = @EmployeeId", new { EmployeeId }, commandType: CommandType.Text);

                return Holidays.AsList().ToArray();
            }
        }

        public async Task<Holiday> GetHolidayAsync(int? EmployeeId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Holiday = await con.QueryAsync<Holiday>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Holiday.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewHolidayAsync(int EmployeeId, Holiday Holiday)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Holiday>(sql: "spEMHolidayInsert",
                    new
                    {
                        Holiday.EmployeeId,
                        Holiday.StartDateTime,
                        Holiday.EndDateTime,
                        Holiday.OnCall,
                        Holiday.OnCallRateMultiplier,
                        Holiday.Paid
                    },
                    commandType: CommandType.StoredProcedure);

                Holiday.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateHolidayAsync(int EmployeeId, Holiday Holiday)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMHolidayUpdate",
                    new
                    {
                        Holiday.Id,
                        Holiday.EmployeeId,
                        Holiday.StartDateTime,
                        Holiday.EndDateTime,
                        Holiday.OnCall,
                        Holiday.OnCallRateMultiplier,
                        Holiday.Paid
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
