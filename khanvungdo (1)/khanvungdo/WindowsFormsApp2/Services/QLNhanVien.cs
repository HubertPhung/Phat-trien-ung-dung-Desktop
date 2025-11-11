using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Infrastructure;
using BussinessLogic.Models;
using WindowsFormsApp2.Security;

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
            using (var cmd = new SqlCommand("USP_DangNhap", conn))
            {
                // L?y theo username r?i ki?m tra password ? ?ng d?ng ð? h? tr? hash l?n plaintext (legacy)
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    var storedPass = Convert.ToString(rd["PassWord"]);
                    // Verify hashed (SHA256:<salt>:<hash>) ho?c fallback so sánh plaintext
                    if (!PasswordHasher.Verify(password, storedPass))
                        return null;

                    var nv = new NhanVien
                    {
                        MaTaiKhoan = Convert.ToString(rd["MaTaiKhoan"]),
                        TenTaiKhoan = Convert.ToString(rd["TenTaiKhoan"]),
                        MatKhau = null, // không lýu plaintext trong b? nh?
                        Loai = Convert.ToByte(Convert.ToInt32(rd["Type"]))
                    };
                    return nv;
                }
            }
        }
    }
}
