using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Website.Context;
using Website.Utility;
using Website.ViewModels;
//using Faker;

namespace Website.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly MyContext db;

        public AccountController()
        {
            db = new MyContext();
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //for (int i = 9; i < 14; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        db.Trainings.Add(new Models.Training 
            //        {
            //            TrainingTypeId = i,
            //            NameAr = Faker.Lorem.Sentence(5),
            //            NameEn = Faker.Lorem.Sentence(5),
            //            ContentDetailsAr = Faker.Lorem.Paragraph(6),
            //            ContentDetailsEn = Faker.Lorem.Paragraph(6),

            //            TargetAr = Faker.Lorem.Paragraph(6),
            //            TargetEn = Faker.Lorem.Paragraph(6),

            //            CoursePersonsAr= Faker.Lorem.Paragraph(6),
            //            CoursePersonsEn = Faker.Lorem.Paragraph(6),

            //            DetailsAr = Faker.Lorem.Paragraph(6),
            //            DescriptionEn = Faker.Lorem.Paragraph(6),

            //            DescriptionAr = Faker.Lorem.Paragraph(6),
            //            DetailsEn = Faker.Lorem.Paragraph(6),

            //            Location = Faker.Country.Name(),

            //            TrainingDate = DateTime.Now,
            //            HourlyDuration = Faker.RandomNumber.Next(),
            //            LecturesCount = Faker.RandomNumber.Next(),
            //            Price = Faker.RandomNumber.Next()

            //        });
            //    }
            //    db.SaveChanges();
            //}

            ////gallery
            //for (int i = 1; i < 20; i++)
            //{
            //        db.Galleries.Add(new Models.Gallery
            //        {
            //            Date = DateTime.Now,
            //            NameAr = Faker.Lorem.Sentence(5),
            //            NameEn = Faker.Lorem.Sentence(5),
            //            DescriptionAr = Faker.Lorem.Paragraph(6),
            //            DescriptionEn = Faker.Lorem.Paragraph(6),

            //            LocationAr = Faker.Country.Name(),
            //            LocationEn = Faker.Country.Name(),
            //        });

            //    db.SaveChanges();
            //}
            //gallery details
            //var galleries = db.Galleries;
            //foreach (var gallery in galleries)
            //{
            //    //string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/gallery/" + gallery.Id); //Path 
            //    //System.IO.Directory.CreateDirectory(path);
            //    for (int i = 0; i < 6; i++)
            //    {
            //        db.GalleryImages.Add(new GalleryImage
            //        {
            //            GalleryId = gallery.Id,
            //        });
            //    }
            
            //    db.SaveChanges();
            //}
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var encrptedPassword = Encrypt(model.Password);
                    var loggedUser = db.Users.Any(a => a.UserName.ToLower() == model.UserName && a.Password == model.Password);
                    if (loggedUser)
                    {
                        var user = db.Users.FirstOrDefault(a => a.UserName.ToLower() == model.UserName && a.Password == model.Password);

                        if (!user.IsActive)
                        {
                            ModelState.AddModelError("", "المستخدم غير مفعل");
                            return View(model);
                        }

                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        user.LastLoginDate = DateTime.Now;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Base");
                    }
                    else
                    {
                        ModelState.AddModelError("", "اسم المستخدم او كلمة المرور غير صحيح");
                        return View(model);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                string error = ex.ExceptionHandle();
                ModelState.AddModelError("", error);
                return View(model);
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}