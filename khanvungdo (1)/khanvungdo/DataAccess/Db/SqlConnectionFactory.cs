using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataAccess.Db
{
    public static class SqlConnectionFactory
    {
        private static string _overrideConnectionString; 
        private const string DefaultConnectionString = "Data Source=.;Initial Catalog=QuanLyDVKS;Integrated Security=True;MultipleActiveResultSets=True";

        public static void Configure(string connectionString)
        {
            _overrideConnectionString = connectionString;
        }

        private static string ResolveConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(_overrideConnectionString))
                return _overrideConnectionString;

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
                // ignore, will fallback to default
            }

            return DefaultConnectionString;
        }

        public static IDbConnection Create()
        {
            var cs = ResolveConnectionString();
            return new SqlConnection(cs);
        }
    }
}
