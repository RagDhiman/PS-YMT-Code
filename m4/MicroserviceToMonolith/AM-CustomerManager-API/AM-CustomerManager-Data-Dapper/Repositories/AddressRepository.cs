using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class AddressRepository : DapperBaseRepository, IAddressRepository
    {
        internal const string SQL =
            @"SELECT
                Id, CustomerId, Street, CityTown, County, PostCode, Country
                FROM vwCMAddress B";

        public AddressRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteAddress(Address Address)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMAddressDelete", new { Address.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Address[]> GetAllAddressesAsync(int CustomerId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Addresss = await con.QueryAsync<Address>(sql: $"{SQL} where B.CustomerId = @CustomerId", new { CustomerId }, commandType: CommandType.Text);

                return Addresss.AsList().ToArray();
            }
        }

        public async Task<Address> GetAddressAsync(int? CustomerId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Address = await con.QueryAsync<Address>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Address.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewAddressAsync(int CustomerId, Address Address)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Address>(sql: "spCMAddressInsert",
                    new
                    {
                        Address.CustomerId,
                        Address.Street,
                        Address.CityTown,
                        Address.County,
                        Address.PostCode,
                        Address.Country
                    },
                    commandType: CommandType.StoredProcedure);

                Address.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateAddressAsync(int CustomerId, Address Address)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMAddressUpdate",
                    new
                    {
                        Address.Id,
                        Address.CustomerId,
                        Address.Street,
                        Address.CityTown,
                        Address.County,
                        Address.PostCode,
                        Address.Country
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
