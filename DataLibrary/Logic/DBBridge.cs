using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataLibrary.Logic
{
    public static class DBBridge
    {
        public static List<UserModel> LoadUsers()
        {
            string sql = @"SELECT InstitutionId, UserName, Email FROM dbo.Users;";

            return SqlAccess.LoadData<UserModel>(sql);
        }

            public static List<ResultModel> LoadResults()
        {
            string sql = @"SELECT us.InstitutionId, res.ResId, assign.AsName, res.Score, res.TotAsNum FROM dbo.ResultData res INNER JOIN dbo.AssignmentData assign ON res.AsId = assign.AsId INNER JOIN dbo.Users us ON res.UserId = us.UserId;";

            return SqlAccess.LoadData<ResultModel>(sql);
        }
    }
}
