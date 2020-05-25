using DataLibrary.Logic;
using GradProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Message = "Upload your files.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine("C:\\Users\\deniz\\source\\repos\\GradProj\\Metrics\\", "Assignment.txt");
                file.SaveAs(path);
            }

            return RedirectToAction("UploadResult");
        }

        public ActionResult UploadResult()
        {
            Metrics.Program.Main();

            List<UploadResultModel> UploadResult = new List<UploadResultModel>();

            UploadResult.Add(new UploadResultModel
            {
                NumofClassData = DataLibrary.Models.UploadResultModel.NumofClassData,
                NumofClassMethod = DataLibrary.Models.UploadResultModel.NumofClassMethod,
                NumofCritClass = DataLibrary.Models.UploadResultModel.NumofCritClass
            });

            return View(UploadResult);
        }

        public ActionResult Results()
        {
            ViewBag.Message = "View the past results.";

            var data = DBBridge.LoadResults();
            List<ResultModel> Results = new List<ResultModel>();

            foreach (var row in data)
            {
                Results.Add(new ResultModel
                {
                    ResultId = row.ResultId,
                    AssignmentName = row.AssignmentName,
                    Score = row.Score,
                    NumberofAttendance = row.TotalAssignmentNumber
                });
            }

            return View(Results);
        }

        public ActionResult ViewUsers()
        {
            ViewBag.Message = "View the registered users.";

            var data = DBBridge.LoadUsers();
            List<UserModel> Users = new List<UserModel>();

            foreach (var row in data)
            {
                Users.Add(new UserModel
                {
                    UserId = row.InstitutionId,
                    UserName = row.UserFullName,
                    MailAddress = row.EMail
                });
            }

            return View(Users);
        }

        public ActionResult EditUserInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(UserModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}