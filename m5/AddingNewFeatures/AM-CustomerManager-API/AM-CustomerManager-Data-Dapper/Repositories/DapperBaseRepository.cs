using System;
using System.Data;
using System.Data.SqlClient;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class DapperBaseRepository
    {

        public DapperBaseRepository(String connectionString)
        {
            _connectionString = connectionString;
        }


        private String _connectionString;



        protected IDbConnection GetConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }

    }
}
