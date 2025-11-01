using System;
using System.Data;
using DataAccess.Db;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class SqlHoaDonRepository
    {
        public int EnsureOpenInvoiceForRoom(int roomId)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "USP_ThemHoaDon";
                cmd.CommandType = CommandType.StoredProcedure;
                var p = cmd.CreateParameter();
                p.ParameterName = "@idPhong";
                p.Value = roomId;
                cmd.Parameters.Add(p);
                conn.Open();
                var dt = new System.Data.DataTable();
                using (var da = new SqlDataAdapter((SqlCommand)cmd))
                {
                    da.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["id"]);
                }
                return 0;
            }
        }

        public void AddOrUpdateChiTiet(int billId, int serviceId, int quantity, string note)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "USP_InsertChiTietHD";
                cmd.CommandType = CommandType.StoredProcedure;
                var p1 = cmd.CreateParameter(); p1.ParameterName = "@idHoaDon"; p1.Value = billId; cmd.Parameters.Add(p1);
                var p2 = cmd.CreateParameter(); p2.ParameterName = "@idDichVu"; p2.Value = serviceId; cmd.Parameters.Add(p2);
                var p3 = cmd.CreateParameter(); p3.ParameterName = "@soLuong"; p3.Value = quantity; cmd.Parameters.Add(p3);
                var p4 = cmd.CreateParameter(); p4.ParameterName = "@ghiChu"; p4.Value = (object)note ?? DBNull.Value; cmd.Parameters.Add(p4);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CloseInvoice(int billId, decimal discountPercent)
        {
            using (var conn = SqlConnectionFactory.Create())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "USP_DienHoaDon";
                cmd.CommandType = CommandType.StoredProcedure;
                var p1 = cmd.CreateParameter(); p1.ParameterName = "@billID"; p1.Value = billId; cmd.Parameters.Add(p1);
                var p2 = cmd.CreateParameter(); p2.ParameterName = "@giamgia"; p2.Value = discountPercent; cmd.Parameters.Add(p2);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
