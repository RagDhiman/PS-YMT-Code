using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class EquipmentRepository : DapperBaseRepository, IEquipmentRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.EmployeeId,
                B.LoanStartDateTime,
                B.LoanEndDateTime,
                B.Reference,
                B.Name,
                B.ExpectedReturnDate
            FROM vwEMEquipment B";

        public EquipmentRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteEquipment(Equipment Equipment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMEquipmentDelete", new { Equipment.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Equipment[]> GetAllEquipmentesAsync(int EmployeeId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Equipments = await con.QueryAsync<Equipment>(sql: $"{SQL} where B.EmployeeId = @EmployeeId", new { EmployeeId }, commandType: CommandType.Text);

                return Equipments.AsList().ToArray();
            }
        }

        public async Task<Equipment> GetEquipmentAsync(int? EmployeeId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Equipment = await con.QueryAsync<Equipment>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Equipment.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewEquipmentAsync(int EmployeeId, Equipment Equipment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Equipment>(sql: "spEMEquipmentInsert",
                    new
                    {
                        Equipment.EmployeeId,
                        Equipment.LoanStartDateTime,
                        Equipment.LoanEndDateTime,
                        Equipment.Reference,
                        Equipment.Name,
                        Equipment.ExpectedReturnDate
                    },
                    commandType: CommandType.StoredProcedure);

                Equipment.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateEquipmentAsync(int EmployeeId, Equipment Equipment)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMEquipmentUpdate",
                    new
                    {
                        Equipment.Id,
                        Equipment.EmployeeId,
                        Equipment.LoanStartDateTime,
                        Equipment.LoanEndDateTime,
                        Equipment.Reference,
                        Equipment.Name,
                        Equipment.ExpectedReturnDate
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
