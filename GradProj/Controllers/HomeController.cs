using DataLibrary.Logic;
using GradProj.Models;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles ="Student")]
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload your files.";

            return View();
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

        [Authorize(Roles ="Admin")]
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
                    UserName = row.UserName,
                    MailAddress = row.UserMail
                });
            }

            return View(Users);
        }
    }
}