using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Infrastructure;
using BussinessLogic.Models;

namespace WindowsFormsApp2.Services
{
    public static class QLKhachHang
    {
        public static List<KhachHang> Find(string keyword = null, int? id = null)
        {
            var list = new List<KhachHang>();
            string sql = @"SELECT id, TenKH, CMND_CCCD, DiaChi, SDT, LoaiKH, GhiChu FROM dbo.KhachHang
                            WHERE (@kw IS NULL OR TenKH LIKE '%' + @kw + '%')
                              AND (@id IS NULL OR id = @id)
                            ORDER BY TenKH";
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", (object)keyword ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", (object)id ?? DBNull.Value);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new KhachHang
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            TenKH = Convert.ToString(rd["TenKH"]),
                            CMND_CCCD = Convert.ToString(rd["CMND_CCCD"]),
                            DiaChi = rd["DiaChi"] == DBNull.Value ? null : Convert.ToString(rd["DiaChi"]),
                            SDT = rd["SDT"] == DBNull.Value ? null : Convert.ToString(rd["SDT"]),
                            LoaiKH = Convert.ToString(rd["LoaiKH"]),
                            GhiChu = rd["GhiChu"] == DBNull.Value ? null : Convert.ToString(rd["GhiChu"])
                        });
                    }
                }
            }
            return list;
        }

        public static bool ExistsByCMND(string cmnd, int? excludeId = null)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.KhachHang WHERE CMND_CCCD=@cm AND (@ex IS NULL OR id<>@ex)", conn))
            {
                cmd.Parameters.AddWithValue("@cm", cmnd);
                cmd.Parameters.AddWithValue("@ex", (object)excludeId ?? DBNull.Value);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public static int Insert(KhachHang kh)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"INSERT dbo.KhachHang(TenKH, CMND_CCCD, DiaChi, SDT, LoaiKH, GhiChu)
                                              OUTPUT INSERTED.id
                                              VALUES(@Ten,@CM,@DiaChi,@SDT,@Loai,@GhiChu)", conn))
            {
                cmd.Parameters.AddWithValue("@Ten", kh.TenKH);
                cmd.Parameters.AddWithValue("@CM", kh.CMND_CCCD);
                cmd.Parameters.AddWithValue("@DiaChi", (object)kh.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrWhiteSpace(kh.SDT) ? (object)DBNull.Value : (object)kh.SDT);
                cmd.Parameters.AddWithValue("@Loai", string.IsNullOrWhiteSpace(kh.LoaiKH) ? (object)DBNull.Value : (object)kh.LoaiKH);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(kh.GhiChu) ? (object)DBNull.Value : (object)kh.GhiChu);
                var newId = cmd.ExecuteScalar();
                return Convert.ToInt32(newId);
            }
        }

        public static int Update(KhachHang kh)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"UPDATE dbo.KhachHang
                                              SET TenKH=@Ten, CMND_CCCD=@CM, DiaChi=@DiaChi, SDT=@SDT, LoaiKH=@Loai, GhiChu=@GhiChu
                                              WHERE id=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", kh.Id);
                cmd.Parameters.AddWithValue("@Ten", kh.TenKH);
                cmd.Parameters.AddWithValue("@CM", kh.CMND_CCCD);
                cmd.Parameters.AddWithValue("@DiaChi", (object)kh.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrWhiteSpace(kh.SDT) ? (object)DBNull.Value : (object)kh.SDT);
                cmd.Parameters.AddWithValue("@Loai", string.IsNullOrWhiteSpace(kh.LoaiKH) ? (object)DBNull.Value : (object)kh.LoaiKH);
                cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(kh.GhiChu) ? (object)DBNull.Value : (object)kh.GhiChu);
                return cmd.ExecuteNonQuery();
            }
        }

        public static bool HasInvoices(int khId)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"SELECT COUNT(1) FROM dbo.HoaDon hd
                                              JOIN dbo.Phong p ON p.id = hd.MaPhong
                                              WHERE p.MaKH = @kh", conn))
            {
                cmd.Parameters.AddWithValue("@kh", khId);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public static int Delete(IEnumerable<int> ids)
        {
            int count = 0;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("DELETE dbo.KhachHang WHERE id=@Id", conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                foreach (var id in ids)
                {
                    cmd.Parameters["@Id"].Value = id;
                    count += cmd.ExecuteNonQuery();
                }
            }
            return count;
        }
    }
}
