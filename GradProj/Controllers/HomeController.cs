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


namespace GradProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssignment(CreateAssignmentModel model)
        {
            System.Diagnostics.Debug.WriteLine(model.AssignmentName);
            System.Diagnostics.Debug.WriteLine(model.AssignmentInfo);
            if (ModelState.IsValid)
            {
                DBBridge.CreateAssignment(model.AssignmentName, model.AssignmentInfo);
            }
            
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.Count() > 20)
                {
                    ViewBag.Message = "The file name length is too long.";
                    return View();
                }
                
                var path = Path.Combine("C:\\Users\\deniz\\source\\repos\\GradProj\\Metrics\\", "Assignment.cs");
                file.SaveAs(path);

                /*CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                ICodeCompiler icc = codeProvider.CreateCompiler();
                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateInMemory = true;
                //parameters.OutputAssembly = "assignmentoutput.exe";
                CompilerResults compileResult = icc.CompileAssemblyFromFile(parameters, "C:\\Users\\deniz\\source\\repos\\GradProj\\Metrics\\Assignment.cs");

                var text = "";
                if (compileResult.Errors.Count > 0)
                {
                    foreach (CompilerError CompErr in compileResult.Errors)
                    {
                        text = text +
                            "Line number " + CompErr.Line +
                            ", Error Number: " + CompErr.ErrorNumber +
                            ", '" + CompErr.ErrorText + ";" +
                            Environment.NewLine + Environment.NewLine;
                    }
                }

                System.Diagnostics.Debug.WriteLine(text);

                if (compileResult.Errors.Count == 0)
                {
                    return RedirectToAction("UploadResult");
                }
                else
                {
                    ViewBag.Message = "The uploaded file cannot be compiled!";
                    return View();
                }*/
            }

            return RedirectToAction("UploadResult");
            //return View();
        }

        public ActionResult UploadResult()
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
                    UserId = row.UserId,
                    InsId = row.InstitutionId,
                    UserName = row.UserFullName,
                    MailAddress = row.Email
                });
            }

            return View(Users);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUserInfo()
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
                DBBridge.EditUser(UserId, model.InsId, model.UserName, model.MailAddress);
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
                DBBridge.EditRole(UserId, model.Role);
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
                DBBridge.CreateRole(UserId, model.Role);
            }
            
            return View();
        }
    }
}