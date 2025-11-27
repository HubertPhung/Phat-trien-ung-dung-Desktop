using System;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp2.Security
{
 // Simple salted SHA256 hasher (upgradeable). Format: SHA256:<saltBase64>:<hashBase64>
 public static class PasswordHasher
 {
 private const int SaltSize =16;

 public static string Hash(string password)
 {
 if (password == null) throw new ArgumentNullException(nameof(password));
 var salt = new byte[SaltSize];
 using (var rng = RandomNumberGenerator.Create())
 {
 rng.GetBytes(salt);
 }
 var hash = ComputeHash(password, salt);
 return $"SHA256:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
 }

 public static bool IsHashed(string stored)
 {
 return stored != null && stored.StartsWith("SHA256:");
 }

 public static bool Verify(string password, string stored)
 {
 if (string.IsNullOrEmpty(stored) || password == null) return false;
 if (!IsHashed(stored))
 {
 // Legacy plaintext fallback
 return password == stored;
 }
 var parts = stored.Split(':');
 if (parts.Length !=3) return false;
 try
 {
 var salt = Convert.FromBase64String(parts[1]);
 var expected = parts[2];
 var hash = ComputeHash(password, salt);
 var actual = Convert.ToBase64String(hash);
 return SlowEquals(expected, actual);
 }
 catch { return false; }
 }

 private static byte[] ComputeHash(string password, byte[] salt)
 {
 using (var sha = SHA256.Create())
 {
 var pwdBytes = Encoding.UTF8.GetBytes(password);
 var all = new byte[salt.Length + pwdBytes.Length];
 Buffer.BlockCopy(salt,0, all,0, salt.Length);
 Buffer.BlockCopy(pwdBytes,0, all, salt.Length, pwdBytes.Length);
 return sha.ComputeHash(all);
 }
 }

 // Constant time compare
 private static bool SlowEquals(string a, string b)
 {
 if (a == null || b == null || a.Length != b.Length) return false;
 int diff =0;
 for (int i =0; i < a.Length; i++) diff |= a[i] ^ b[i];
 return diff ==0;
 }
 }
}
