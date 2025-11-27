using System.Collections.Generic;
using System.Data;
using DataAccess.Db;

namespace DataAccess.Repositories
{
    public class SqlDichVuRepository
    {
        public class DichVuDto
        {
            public int Id { get; set; }
            public string Ten { get; set; }
            public string DVT { get; set; }
            public decimal DonGia { get; set; }
        }

        public IEnumerable<DichVuDto> GetByLoaiId(int loaiId)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "USP_XuatDichVuTheoLoai";
                cmd.CommandType = CommandType.StoredProcedure;
                var p = cmd.CreateParameter();
                p.ParameterName = "@ID";
                p.Value = loaiId;
                cmd.Parameters.Add(p);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new DichVuDto
                        {
                            Id = System.Convert.ToInt32(reader["id"]),
                            Ten = reader["TenDV"].ToString(),
                            DVT = reader["DVT"].ToString(),
                            DonGia = (decimal)reader["DonGia"]
                        };
                    }
                }
            }
        }
    }
}
