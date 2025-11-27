
using DataAccess.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    public class CategoryDA
    {
        // Lấy tất cả Category (Category_GetAll)
        public List<Category> GetAll()
        {
            var list = new List<Category>();

            using (var sqlConn = new SqlConnection(Ultilities.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilities.Category_GetAll;

                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // detect whether the result contains a "Type" column
                        bool hasType = false;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i).Equals("Type", StringComparison.OrdinalIgnoreCase))
                            {
                                hasType = true;
                                break;
                            }
                        }

                        var category = new Category
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Name"].ToString(),
                            Type = (hasType && reader["Type"] != DBNull.Value) ? Convert.ToInt32(reader["Type"]) : 0
                        };
                        list.Add(category);
                    }
                }
            }

            return list;
        }

        public int Insert_Update_Delete(Category category, int action)
        {
            using (var sqlConn = new SqlConnection(Ultilities.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilities.Category_InsertUpdateDelete;

                // @ID là InputOutput để khi insert có thể lấy ID mới
                var pId = new SqlParameter("@ID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.InputOutput,
                    Value = category.ID
                };
                command.Parameters.Add(pId);

                command.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = (object)category.Name ?? DBNull.Value;

                // NOTE: removed @Type parameter because your stored procedure doesn't accept it.
                // If you later add @Type to the SP, add the parameter here:
                // command.Parameters.Add("@Type", SqlDbType.Int).Value = category.Type;

                command.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                sqlConn.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0 && command.Parameters["@ID"].Value != DBNull.Value)
                    return Convert.ToInt32(command.Parameters["@ID"].Value);

                return 0;
            }
        }
    }
}