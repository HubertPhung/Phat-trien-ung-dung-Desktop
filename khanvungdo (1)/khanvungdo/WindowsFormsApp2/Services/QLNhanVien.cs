using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Infrastructure;
using BussinessLogic.Models;

namespace WindowsFormsApp2.Services
{
    public static class QLNhanVien
    {
        // Ðãng nh?p: tr? v? NhanVien n?u h?p l?, null n?u sai
        public static NhanVien Authenticate(string tenTaiKhoan, string password)
        {
            if (string.IsNullOrWhiteSpace(tenTaiKhoan) || string.IsNullOrWhiteSpace(password))
                return null;

            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_Login", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                cmd.Parameters.AddWithValue("@password", password);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    var nv = new NhanVien
                    {
                        MaTaiKhoan = Convert.ToString(rd["MaTaiKhoan"]),
                        TenTaiKhoan = Convert.ToString(rd["TenTaiKhoan"]),
                        MatKhau = password, // demo only; real app should not store plaintext
                        Loai = Convert.ToByte(Convert.ToInt32(rd["Type"]))
                    };
                    return nv;
                }
            }
        }
    }
}
