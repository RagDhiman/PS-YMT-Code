using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class TaxInformationRepository : DapperBaseRepository, ITaxInformationRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.EmployeeId,
                B.TaxCode,
                B.VAT,
                B.VATRef
                FROM vwEMTaxInformations B";

        public TaxInformationRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteTaxInformation(TaxInformation TaxInformation)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMTaxInformationDelete", new { TaxInformation.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<TaxInformation[]> GetAllTaxInformationesAsync(int InvoiceId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var TaxInformations = await con.QueryAsync<TaxInformation>(sql: $"{SQL} where B.InvoiceId = @InvoiceId", new { InvoiceId }, commandType: CommandType.Text);

                return TaxInformations.AsList().ToArray();
            }
        }

        public async Task<TaxInformation> GetTaxInformationAsync(int? InvoiceId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var TaxInformation = await con.QueryAsync<TaxInformation>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return TaxInformation.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewTaxInformationAsync(int InvoiceId, TaxInformation TaxInformation)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<TaxInformation>(sql: "spEMTaxInformationInsert",
                    new
                    {
                        TaxInformation.EmployeeId,
                        TaxInformation.TaxCode,
                        TaxInformation.VAT,
                        TaxInformation.VATRef
                    },
                    commandType: CommandType.StoredProcedure);

                TaxInformation.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateTaxInformationAsync(int InvoiceId, TaxInformation TaxInformation)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMTaxInformationUpdate",
                    new
                    {
                        TaxInformation.Id,
                        TaxInformation.EmployeeId,
                        TaxInformation.TaxCode,
                        TaxInformation.VAT,
                        TaxInformation.VATRef
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
