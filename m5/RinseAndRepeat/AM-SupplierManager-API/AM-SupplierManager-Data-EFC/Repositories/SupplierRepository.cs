using AM_SupplierManager_Core;
using AM_SupplierManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class SupplierRepository : DapperBaseRepository, ISupplierRepository
    {
        internal const string SQL =
            @"SELECT
                    B.Id,
                    B.AccountId,
                    B.ContactName,
                    B.Company,
                    B.Email,
                    B.Phone,
                    B.Mobile,
                    B.Fax,
                    B.Website
                FROM vwSMSuppliers B";

        public SupplierRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteSupplier(Supplier Supplier)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMSuppliersDelete", new { Supplier.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Supplier[]> GetAllSuppliersAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Supplier = await con.QueryAsync<Supplier>(sql: $"{SQL}", commandType: CommandType.Text);

                return Supplier.AsList().ToArray();
            }
        }

        public async Task<Supplier> GetSupplierAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Supplier = await con.QueryAsync<Supplier>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Supplier.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewSupplierAsync(Supplier Supplier)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Supplier>(sql: "spSMSuppliersInsert",
                    new
                    {
                        Supplier.AccountId,
                        Supplier.ContactName,
                        Supplier.Company,
                        Supplier.Email,
                        Supplier.Phone,
                        Supplier.Mobile,
                        Supplier.Fax,
                        Supplier.Website
                    },
                    commandType: CommandType.StoredProcedure);

                Supplier.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier Supplier)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spSMSuppliersUpdate",
                    new
                    {
                        Supplier.Id,
                        Supplier.AccountId,
                        Supplier.ContactName,
                        Supplier.Company,
                        Supplier.Email,
                        Supplier.Phone,
                        Supplier.Mobile,
                        Supplier.Fax,
                        Supplier.Website
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
