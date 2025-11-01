using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Db
{
    public static class SqlConnectionFactory
    {
        private static string _connectionString;

        public static void Configure(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static IDbConnection Create()
        {
            var cs = _connectionString ?? "Data Source=.;Initial Catalog=QuanLyDVKS;Integrated Security=True;MultipleActiveResultSets=True";
            return new SqlConnection(cs);
        }
    }
}
