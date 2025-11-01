// DataAccess.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace ChuDe4
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess()
        {
            // Fixed connection string for this lab project
            _connectionString = "server=.; database=RestaurantManagement; Integrated Security=true;";
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length >0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                var table = new DataTable();
                conn.Open();
                adapter.Fill(table);
                return table;
            }
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length >0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null && parameters.Length >0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}