using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Data;

namespace WindowsFormsApp2.Infrastructure
{
    internal static class Db
    {
        private const string DefaultConnectionString = "Data Source=.;Initial Catalog=QuanLyDVKS;Integrated Security=True;MultipleActiveResultSets=True";
        private static string GetConnectionString()
        {
            try
            {
                var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                if (File.Exists(configPath))
                {
                    var doc = XDocument.Load(configPath);
                    var cs = doc.Root?
                        .Element("connectionStrings")?
                        .Elements("add")
                        .FirstOrDefault(e => (string)e.Attribute("name") == "QuanLyDVKS")?
                        .Attribute("connectionString")?.Value;

                    if (!string.IsNullOrWhiteSpace(cs))
                    {
                        return cs;
                    }
                }
            }
            catch
            {
                // ignored - fall back to default
            }

            return DefaultConnectionString;
        }

        public static SqlConnection Open()
        {
            var conn = new SqlConnection(GetConnectionString());
            conn.Open(); // surface exact error
            return conn;
        }

        public static void ExecuteStoredProcedure(SqlConnection conn, string procedureName)
        {
            using (var cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
