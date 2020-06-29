using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class TrainingRepository : DapperBaseRepository, ITrainingRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.EmployeeId,
                B.StartDateTime,
                B.EndDateTime,
                B.Description,
                B.Name,
                B.Certification,
                B.CertificationName
                FROM vwEMTrainings B";

        public TrainingRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteTraining(Training Training)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMTrainingDelete", new { Training.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Training[]> GetAllTrainingesAsync(int EmployeeId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Trainings = await con.QueryAsync<Training>(sql: $"{SQL} where B.EmployeeId = @EmployeeId", new { EmployeeId }, commandType: CommandType.Text);

                return Trainings.AsList().ToArray();
            }
        }

        public async Task<Training> GetTrainingAsync(int? EmployeeId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Training = await con.QueryAsync<Training>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Training.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewTrainingAsync(int EmployeeId, Training Training)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Training>(sql: "spEMTrainingInsert",
                    new
                    {
                        Training.EmployeeId,
                        Training.StartDateTime,
                        Training.EndDateTime,
                        Training.Description,
                        Training.Name,
                        Training.Certification,
                        Training.CertificationName
                    },
                    commandType: CommandType.StoredProcedure);

                Training.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateTrainingAsync(int EmployeeId, Training Training)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMTrainingUpdate",
                    new
                    {
                        Training.Id,
                        Training.EmployeeId,
                        Training.StartDateTime,
                        Training.EndDateTime,
                        Training.Description,
                        Training.Name,
                        Training.Certification,
                        Training.CertificationName
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
