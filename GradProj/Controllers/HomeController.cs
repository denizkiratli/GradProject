using DataLibrary.Logic;
using GradProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace GradProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = DBBridge.LoadAssignments();
            List<AssignmentModel> Assignments = new List<AssignmentModel>();

            foreach (var row in data)
            {
                Assignments.Add(new AssignmentModel
                {
                    AssignmentId = row.AsId,
                    AssignmentName = row.AsName,
                    AssignmentInfo = row.AsInfo,
                    AssignmentDate = row.AsDate
                });
            }
            
            return View(Assignments);
        }

        [Authorize(Roles = "Admin, Instructor")]
        public ActionResult CreateAssignment()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssignment(AssignmentModel model)
        {
            if (ModelState.IsValid)
            {
                var info = DBBridge.CreateAssignment(model.AssignmentName, model.AssignmentInfo);
                if (info == 1)
                    ViewBag.Message = "The assignment is created successfully!";
                else
                    ViewBag.Message = "The assignment cannot be created!";
            }
            
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, ResultModel model)
        {
            var AsNumCheck = DBBridge.CheckAssignmentNum(model.AssignmentId);
            if (AsNumCheck.Count == 0)
            {
                ViewBag.Message = "There is no Assignment Id which you typed.";
                return View();
            }

            if (file != null && file.ContentLength > 0)
            {
                if (!file.FileName.Contains(".cs"))
                {
                    ViewBag.Message = "The file extension is not .cs.";
                    return View();
                }              
                var path = Path.Combine("C:\\Users\\deniz\\source\\repos\\GradProj\\Metrics\\", "Assignment.cs");
                file.SaveAs(path);
            }
            else
            {
                ViewBag.Message = "The file is empty!";
                return View();
            }
            return RedirectToAction("UploadResult", new { AssignmentId = model.AssignmentId });
        }

        public ActionResult UploadResult(int AssignmentId)
        {
            Metrics.Program.Main();

            List<UploadResultModel> UploadResult = new List<UploadResultModel>();
            UploadResult.Add(new UploadResultModel
            {
                RWSMS = DataLibrary.Models.UploadResultModel.RWSMS,
                SDPMS = DataLibrary.Models.UploadResultModel.SDPMS,
                OOPMG = DataLibrary.Models.UploadResultModel.OOPMG,
                TUG = DataLibrary.Models.UploadResultModel.TUG,
                TG = DataLibrary.Models.UploadResultModel.TG,
                finalScore = DataLibrary.Models.UploadResultModel.finalScore
            });

            int TotAsNum = 1;
            var UserId = User.Identity.GetUserId();
            var res = DBBridge.CheckAttendanceNum(UserId, AssignmentId);
            if (res.Count >= 1)
                TotAsNum = res.Count + 1;
            var Score = DataLibrary.Models.UploadResultModel.finalScore;
            DBBridge.CreateResult(UserId, AssignmentId, Score, TotAsNum);

            return View(UploadResult);
        }

        public ActionResult Results()
        {
            var data = DBBridge.LoadResults();
            List<ResultModel> Results = new List<ResultModel>();

            foreach (var row in data)
            {
                Results.Add(new ResultModel
                {
                    ResultId = row.ResId,
                    InstitutionId = row.InstitutionId,
                    AssignmentName = row.AsName,
                    Score = row.Score,
                    NumberofAttendance = row.TotAsNum
                });
            }

            return View(Results);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult ViewUsers()
        {
            var data = DBBridge.LoadUsers();
            List<UserModel> Users = new List<UserModel>();

            foreach (var row in data)
            {
                Users.Add(new UserModel
                {
                    UserId = row.UserId,
                    InsId = row.InstitutionId,
                    UserName = row.UserFullName,
                    MailAddress = row.Email
                });
            }

            return View(Users);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUserInfo(int InsId, string UserName, string MailAddress)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(string UserId, UserModel model)
        {
            if (ModelState.IsValid)
            {
                var info = DBBridge.EditUser(UserId, model.InsId, model.UserName, model.MailAddress);
                
                if (info == 1)
                    ViewBag.Message = "The user information is changed successfully!";
                else
                    ViewBag.Message = "The user information cannot be changed!";
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(string UserId, UserModel model)
        {
            if (ModelState.IsValid)
            {
                var info = DBBridge.EditRole(UserId, model.Role);

                if (info == 1)
                    ViewBag.Message = "The user role is changed successfully!";
                else
                    ViewBag.Message = "The user role cannot be changed!";
            }
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(string UserId, UserModel model)
        {
            if (ModelState.IsValid)
            {
                var info = DBBridge.CreateRole(UserId, model.Role);

                if (info == 1)
                    ViewBag.Message = "The user role is created successfully!";
                else
                    ViewBag.Message = "The user role cannot be created!";
            }
            
            return View();
        }
    }
}