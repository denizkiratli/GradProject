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
            string sql = @"SELECT UserId, InstitutionId, UserFullName, Email FROM dbo.Users;";

            return SqlAccess.LoadData<UserModel>(sql);
        }

        public static List<ResultModel> LoadResults()
        {
            string sql = @"SELECT us.InstitutionId, res.ResId, assign.AsName, res.Score, res.TotAsNum FROM dbo.ResultData res INNER JOIN dbo.AssignmentData assign ON res.AsId = assign.AsId LEFT JOIN dbo.Users us ON res.UserId = us.UserId WHERE res.ResId IS NOT NULL;";

            return SqlAccess.LoadData<ResultModel>(sql);
        }

        public static List<ResultModel> CheckAttendanceNum(string UserId, int AsId)
        {
            ResultModel data = new ResultModel
            {
                UserId = UserId,
                AssignmentId = AsId
            };

            string sql = @"SELECT * FROM dbo.ResultData WHERE (UserId = @UserId AND AsId = @AssignmentId);";

            return SqlAccess.SearchData<ResultModel>(sql, data);
        }

        public static int CreateResult(string UserId, int AsId, double Score, int TotAsNum)
        {
            ResultModel data = new ResultModel
            {
                UserId = UserId,
                AssignmentId = AsId,
                Score = Score,
                TotAsNum = TotAsNum
            };

            string sql = @"INSERT INTO dbo.ResultData (UserId, AsId, Score, TotAsNum) VALUES (@UserId, @AssignmentId, @Score, @TotAsNum);";

            return SqlAccess.UseData(sql, data);
        }

        public static int EditUser(string UserId, int InsId, string UserFullName, string Email)
        {
            UserModel data = new UserModel
            {
                UserId = UserId,
                InstitutionId = InsId,
                UserFullName = UserFullName,
                Email = Email
            };

            string sql = @"UPDATE dbo.Users SET InstitutionId = @InstitutionId, UserFullName = @UserFullName, Email = @Email WHERE UserId = @UserId;";

            return SqlAccess.UseData(sql, data);
        }

        public static int EditRole(string UserId, string Role)
        {
            UserModel data = new UserModel
            {
                UserId = UserId,
                RoleId = Role
            };

            string sql = @"UPDATE dbo.UserRoles SET RoleId = @RoleId WHERE UserId = @UserId;";

            return SqlAccess.UseData(sql, data);
        }

        public static int CreateRole(string UserId, string Role)
        {
            UserModel data = new UserModel
            {
                UserId = UserId,
                RoleId = Role
            };

            string sql = @"INSERT INTO dbo.UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId);";

            return SqlAccess.UseData(sql, data);
        }

        public static int CreateAssignment(string AssignmentName, string AssignmentInfo)
        {
            AssignmentModel data = new AssignmentModel
            {
                AsName = AssignmentName,
                AsInfo = AssignmentInfo
            };

            string sql = @"INSERT INTO dbo.AssignmentData (AsName, AsInfo) VALUES (@AsName, @AsInfo);";

            return SqlAccess.UseData(sql, data);
        }

        public static List<AssignmentModel> LoadAssignments()
        {
            string sql = @"SELECT AsId, AsName, AsInfo, AsDate FROM dbo.AssignmentData;";

            return SqlAccess.LoadData<AssignmentModel>(sql);
        }

        public static List<ResultModel> CheckAssignmentNum(int AsId)
        {
            ResultModel data = new ResultModel
            {
                AssignmentId = AsId
            };

            string sql = @"SELECT * FROM dbo.AssignmentData WHERE (AsId = @AssignmentId);";

            return SqlAccess.SearchData<ResultModel>(sql, data);
        }
    }
}
