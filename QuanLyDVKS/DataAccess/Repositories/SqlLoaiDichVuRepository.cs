using System.Collections.Generic;
using System.Data;
using DataAccess.Db;

namespace DataAccess.Repositories
{
    public class SqlLoaiDichVuRepository
    {
        public IEnumerable<(int Id, string Name)> GetAll()
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "USP_XuatLoaiDichVu";
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return (System.Convert.ToInt32(reader["id"]), reader["name"].ToString());
                    }
                }
            }
        }
    }
}
