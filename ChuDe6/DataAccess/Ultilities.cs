using System;
using System.Configuration;
using System.Linq;

namespace DataAccess
{
    public static class Ultilities
    {
        private const string StrName = "ConnectionString";
     
        private static string _connectionString;
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        public static string ConnectionString
        {
            get
            {
                if (!_initialized)
                {
                    lock (_lock)
                    {
                        if (!_initialized)
                        {
                            var setting = ConfigurationManager.ConnectionStrings[StrName];
                            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                            {
                                var availableKeys = string.Join(", ", 
                                    ConfigurationManager.ConnectionStrings
                                    .Cast<ConnectionStringSettings>()
                                    .Select(c => c.Name));
    
                                throw new ConfigurationErrorsException(
                                    $"Connection string '{StrName}' missing or empty. " +
                                    $"Available connection strings: [{availableKeys}]. " +
                                    $"Ensure <add name=\"{StrName}\" .../> exists in App.config.");
                            }
                            _connectionString = setting.ConnectionString;
                            _initialized = true;
                        }
                    }
                }
                return _connectionString;
            }
        }

        // Food
        public const string Food_GetAll = "Food_GetAll";
        public const string Food_InsertUpdateDelete = "Food_InsertUpdateDelete";

        // Category
        public const string Category_GetAll = "Category_GetAll";
        public const string Category_InsertUpdateDelete = "Category_InsertUpdateDelete";

        // Ban
        public const string Ban_InsertUpdateDelete = "Ban_InsertUpdateDelete";

        // Bills
        public const string Bills_InsertUpdateDelete = "Bills_InsertUpdateDelete2"; // dùng bản gộp

        // BillDetails
        public const string BillDetails_InsertUpdateDelete = "BillDetails_InsertUpdateDelete2";

        // Account
        public const string Account_InsertUpdateDelete = "Account_InsertUpdateDelete";

        // Rolee
        public const string Rolee_InsertUpdateDelete = "Rolee_InsertUpdateDelete2";

        // RoleAccount (khoá kép)
        public const string RoleAccount_InsertUpdateDelete = "RoleAccount_InsertUpdateDelete2";
    }
}
