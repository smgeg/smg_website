using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using Website.Utility;
using Website.ViewModels;

namespace Website.Controllers
{
    public class _AccountController : Controller
    {
        //[AllowAnonymous]
        // GET: Account
        Context.MyContext db = new Context.MyContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (db.AgricultureNetworkUsers.Any(a => a.IsActive && a.UserName == loginViewModel.UserName && a.Password == loginViewModel.Password))
                {
                    var userData = db.AgricultureNetworkUsers.FirstOrDefault(a => a.UserName == loginViewModel.UserName && a.Password == loginViewModel.Password);
                    var userId = userData.Id;
                    var userName = userData.UserName;
                    Session["IsActiveUser"] = userId;
                    Session["UserName"] = userName;
                    return RedirectToAction("Profile");
                }
                ModelState.AddModelError("", Resources.Resource.InvalidUsernameOrPassword);
            }
            
            return View(loginViewModel);
        }


        public new ActionResult Profile()
        {
            if (Utilities.IsUserAuthenticatedOnWebsite)
            {
                var userId = Convert.ToInt32(Session["IsActiveUser"].ToString());
                var userData = db.AgricultureNetworkUsers.Find(userId);
                if (userData != null)
                {
                    if (db.UserCourseCollections.Any(a => a.UserId == userId))
                    {
                        var courses = db.UserCourseCollections.Where(a => a.UserId == userId).Select(s => s.CourseId).ToList();
                        foreach (var item in courses)
                        {
                            var course = db.Courses.Find(item);
                            userData.userCourses.Add(Utilities.IsArabic ? course.NameAr : course.NameEn);
                        }
                    }

                    return View(userData);
                }
            }
            return RedirectToAction("Login", "_Account");
        }

        public ActionResult Signout()
        {
            if (Utilities.IsUserAuthenticatedOnWebsite)
            {
                Session["IsActiveUser"] = null;
                Session["UserName"] = null;
            }
            return RedirectToAction("Index" , "Home");
        }


    }
}