using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace ChuDe4
{
    internal static class DbInitializer
    {
        private static bool _initialized;

        public static void EnsureDatabase()
        {
            if (_initialized)
                return;

            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var pathProject = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Database\\RestaurantManagement.sql"));
                var pathOutput = Path.Combine(baseDir, @"Database\\RestaurantManagement.sql");
                var scriptPath = File.Exists(pathProject) ? pathProject : (File.Exists(pathOutput) ? pathOutput : null);

                if (string.IsNullOrEmpty(scriptPath))
                {
                    _initialized = true;
                    return;
                }

                var script = File.ReadAllText(scriptPath);

                using (var conn = new SqlConnection("server=.; Integrated Security=true;"))
                {
                    conn.Open();

                    var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (var batch in batches)
                    {
                        var sql = batch.Trim();
                        if (sql.Length == 0)
                            continue;

                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
               
            }
            finally
            {
                _initialized = true;
            }
        }
    }
}
