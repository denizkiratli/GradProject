using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataLibrary.DataAccess
{
    public static class SqlAccess
    {
        public static string GetConnecitonString(string connectionName="UserDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection connection = new SqlConnection(GetConnecitonString()))
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public static int UseData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnecitonString()))
            {
                return connection.Execute(sql, data);
            }
        }
    }
}
