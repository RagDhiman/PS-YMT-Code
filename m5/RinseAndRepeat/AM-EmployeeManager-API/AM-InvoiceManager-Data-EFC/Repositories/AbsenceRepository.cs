using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class AbsenceRepository : DapperBaseRepository, IAbsenceRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.EmployeeId,
                B.StartDateTime,
                B.EndDateTime,
                B.Description,
                B.Notes,
                B.Paid
                FROM vwEMAbsences B";

        public AbsenceRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteAbsence(Absence Absence)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMAbsenceDelete", new { Absence.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Absence[]> GetAllAbsenceesAsync(int EmployeeId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Absences = await con.QueryAsync<Absence>(sql: $"{SQL} where B.EmployeeId = @EmployeeId", new { EmployeeId }, commandType: CommandType.Text);

                return Absences.AsList().ToArray();
            }
        }

        public async Task<Absence> GetAbsenceAsync(int? EmployeeId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Absence = await con.QueryAsync<Absence>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Absence.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewAbsenceAsync(int EmployeeId, Absence Absence)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Absence>(sql: "spEMAbsenceInsert",
                    new
                    {
                        Absence.EmployeeId,
                        Absence.StartDateTime,
                        Absence.EndDateTime,
                        Absence.Description,
                        Absence.Notes,
                        Absence.Paid
                    },
                    commandType: CommandType.StoredProcedure);

                Absence.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateAbsenceAsync(int EmployeeId, Absence Absence)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMAbsenceUpdate",
                    new
                    {
                        Absence.Id,
                        Absence.EmployeeId,
                        Absence.StartDateTime,
                        Absence.EndDateTime,
                        Absence.Description,
                        Absence.Notes,
                        Absence.Paid
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
