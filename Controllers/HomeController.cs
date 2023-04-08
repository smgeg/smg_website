using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using Website.Context;
using Website.Models;
using Website.ViewModels;
//using Telerik.Web.PDF;
using Kendo.Mvc.Extensions;
using System.Web.Routing;
using Website.Utility;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        MyContext db = new MyContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ComingSoon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMailToAdmin()
        {
            return Json("MF000", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CourseTypeDetails(int? id) 
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var courseDetails = db.CourseTypes.Find(id);
            if (courseDetails == null)
            {
                return HttpNotFound();
            }

            ViewBag.header = Utility.Utilities.IsArabic ? courseDetails.NameAr : courseDetails.NameEn;
            ViewBag.details = Utility.Utilities.IsArabic ? courseDetails.DescriptionAr : courseDetails.DescriptionEn;

            var typeCourses = db.Courses.Where(w => w.CourseTypeId == id);
            return View(typeCourses.ToList());
        }
        public ActionResult TrainingTypeDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var traingTypeDetails = db.TrainingTypes.Find(id);
            if (traingTypeDetails == null)
            {
                return HttpNotFound();
            }

            ViewBag.header = Utility.Utilities.IsArabic ? traingTypeDetails.NameAr : traingTypeDetails.NameEn;
            ViewBag.details = Utility.Utilities.IsArabic ? traingTypeDetails.DescriptionAr : traingTypeDetails.DescriptionEn;

            var typeCourses = db.Trainings.Where(w => w.TrainingTypeId == id);
            return View(typeCourses.ToList());
        }

        public ActionResult CourseDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var courseDetails = db.Courses.Find(id);
            if (courseDetails == null)
            {
                return HttpNotFound();
            }

            //ViewBag.header = Utility.Utilities.IsArabic ? courseDetails.NameAr : courseDetails.NameEn;
            //ViewBag.details = Utility.Utilities.IsArabic ? courseDetails.DescriptionAr : courseDetails.DescriptionEn;

            //var typeCourses = db.Courses.Where(w => w.CourseTypeId == id);

            if (db.CourseApprovalCollections.Any(a => a.CourseId == id))
            {
                var approvals = db.CourseApprovalCollections.Where(a => a.CourseId == id).Select(s => s.CourseApprovalId).ToList();
                foreach (var item in approvals)
                {
                    var approval = db.CourseApprovals.Find(item);
                    courseDetails.courseApprovalsWithImage.Add(approval);
                }
            }

            return View(courseDetails);
        }
        public ActionResult TrainingDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var trainingDetails = db.Trainings.Find(id);
            if (trainingDetails == null)
            {
                return HttpNotFound();
            }

            //ViewBag.header = Utility.Utilities.IsArabic ? courseDetails.NameAr : courseDetails.NameEn;
            //ViewBag.details = Utility.Utilities.IsArabic ? courseDetails.DescriptionAr : courseDetails.DescriptionEn;

            //var typeCourses = db.Courses.Where(w => w.CourseTypeId == id);
            return View(trainingDetails);
        }

        public ActionResult RegisterUser(string type, int? id)
        {
            if (string.IsNullOrEmpty(type) || !id.HasValue)
            {
                return HttpNotFound();
            }

            ViewBag.type = type;
            ViewBag.id = id.Value;
            string name = "";
            if (type == "c") //course
            {
                var courseDetails = db.Courses.Find(id);
                if (courseDetails == null)
                {
                    return HttpNotFound();
                }
                name = Utility.Utilities.IsArabic ? courseDetails.NameAr : courseDetails.NameEn;
            }
            else if (type == "t") //trainng
            {
                var trainingDetails = db.Trainings.Find(id);
                if (trainingDetails == null)
                {
                    return HttpNotFound();
                }
                name = Utility.Utilities.IsArabic ? trainingDetails.NameAr : trainingDetails.NameEn;
            }
            else
                return HttpNotFound();

            ViewBag.Name = name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisteredUser registeredUser)
        {
            if (ModelState.IsValid)
            {
                if (registeredUser.Type == "c") //course
                {
                    registeredUser.CourseId = registeredUser.Id;
                    registeredUser.Id = 0;
                }
                else if (registeredUser.Type == "t") //training
                {
                    registeredUser.TrainingId = registeredUser.Id;
                    registeredUser.Id = 0;
                }
                db.RegisteredUsers.Add(registeredUser);
                db.SaveChanges();
                ViewBag.success = true;
                ViewBag.name = registeredUser.Name;

                //send Mail
                DirectoryInfo directoryInfo = new DirectoryInfo(Request.PhysicalApplicationPath);
                string body = System.IO.File.ReadAllText(directoryInfo.FullName + @"\MailTemplates\RegisterUserForCourse.html");
                string courseNameEn = "", courseNameAr = "";
                if (registeredUser.Type == "c")
                {
                    var obj = db.Courses.Find(registeredUser.CourseId);
                    if (obj != null)
                    {
                        courseNameEn = obj.NameEn;
                        courseNameAr = obj.NameAr;
                    }
                }
                else
                {
                    var obj = db.Trainings.Find(registeredUser.TrainingId);
                    if (obj != null)
                    {
                        courseNameEn = obj.NameEn;
                        courseNameAr = obj.NameAr;
                    }
                }
                body = string.Format(body , registeredUser.Name , registeredUser.Type == "c" ? "Course" :"Training Section" , courseNameEn , registeredUser.Type == "c" ? "الدبلومة التدريبية" : "الكورس التدريبى", courseNameAr);
                Utility.Utilities.SendMail("Course registration request", registeredUser.Email, body);
                return View();
            }
            ViewBag.type = registeredUser.Type;
            ViewBag.id = registeredUser.Id;
            return View();
        }

        public ActionResult GalleryCourses()
        {
            var coursesGallery = db.Galleries.Where(w=> w.Type == "c");
            if (coursesGallery.Any())
            {
                coursesGallery.ToList().ForEach(f=> 
                {
                    var images = db.GalleryImages.Where(w => w.GalleryId == f.Id);
                    if (images.Any())
                    {
                        f.FirstImage = images.FirstOrDefault().Image;
                    }
                });
            }
            return View(coursesGallery.OrderByDescending(o => o.Id).ToList());
        }
        public ActionResult GallerySeminars()
        {
            var seminarsGallery = db.Galleries.Where(w => w.Type == "n");
            if (seminarsGallery.Any())
            {
                seminarsGallery.ToList().ForEach(f =>
                {
                    var images = db.GalleryImages.Where(w => w.GalleryId == f.Id);
                    if (images.Any())
                    {
                        f.FirstImage = images.FirstOrDefault().Image;
                    }
                });
            }
            return View(seminarsGallery.OrderByDescending(o => o.Id).ToList());
        }
        public ActionResult GalleryEvents()
        {
            var eventsGallery = db.Galleries.Where(w => w.Type == "e");
            if (eventsGallery.Any())
            {
                eventsGallery.ToList().ForEach(f =>
                {
                    var images = db.GalleryImages.Where(w => w.GalleryId == f.Id);
                    if (images.Any())
                    {
                        f.FirstImage = images.FirstOrDefault().Image;
                    }
                });
            }
            return View(eventsGallery.OrderByDescending(o => o.Id).ToList());
        }

        public ActionResult GalleryDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }

            var galleryImages = db.GalleryImages.Where(w=> w.GalleryId == gallery.Id);

            if (galleryImages.Any())
            {
                gallery.Images = galleryImages.Select(s=> s.Image).ToList();
            }

            return View(gallery);
        }

        public ActionResult BlogDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            

            return View(blog);
        }

        public ActionResult Careers()
        {
            var jobs = db.Jobs.Where(w => w.IsActive).OrderByDescending(o=> o.Id);
            return View(jobs.ToList());
        }
        public ActionResult CareerDetails(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var jobDetais = db.Jobs.Find(id);
            if (jobDetais == null)
            {
                return HttpNotFound();
            }
            return View(jobDetais);
        }

        public ActionResult ApplyToCareer(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var jobDetais = db.Jobs.Find(id);
            if (jobDetais == null)
            {
                return HttpNotFound();
            }
            return View(new JobUsers { Job = jobDetais });
        }

        [HttpPost]
        public ActionResult ApplyToCareer(JobUsers registeredUserForJob)
        {
            if (ModelState.IsValid)
            {
                //JobUsers jobUser = new JobUsers();
                var guid = Guid.NewGuid().ToString();
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/cv"); //Path  
                    guid = guid + Path.GetExtension(Request.Files[0].FileName);

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    Request.Files[0].SaveAs(imgPath);
                    registeredUserForJob.CV = guid;
                }

                registeredUserForJob.CreatedOn = DateTime.Now;
                //jobUser.Email = registeredUserForJob.AppliedEmail;
                //jobUser.Mobile = registeredUserForJob.AppliedMobile;
                //jobUser.Name = registeredUserForJob.AppliedName;
                registeredUserForJob.JobId = registeredUserForJob.Id;
                registeredUserForJob.Id = 0;

                db.JobUserss.Add(registeredUserForJob);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

                ViewBag.success = true;
                ViewBag.name = registeredUserForJob.Name;

                //send Mail
                DirectoryInfo directoryInfo = new DirectoryInfo(Request.PhysicalApplicationPath);
                string body = System.IO.File.ReadAllText(directoryInfo.FullName + @"\MailTemplates\RegisterUserForJob.html");

                var jobDetais1 = db.Jobs.Find(registeredUserForJob.JobId);
                string jobNameEn = jobDetais1.NameEn, jobNameAr = jobDetais1.NameEn;

                body = string.Format(body, registeredUserForJob.Name, jobNameEn, jobNameAr);
                Utility.Utilities.SendMail("Apply for a job ", registeredUserForJob.Email, body);
                return View(new JobUsers { Job = jobDetais1 });
            }
            


            var jobDetais = db.Jobs.Find(registeredUserForJob.Id);
            if (jobDetais == null)
            {
                return HttpNotFound();
            }
            registeredUserForJob.Job = jobDetais;
            return View(registeredUserForJob);
        }


        //public ActionResult LoginToAgricultureNetworkUsers()
        //{
        //    return View();
        //}
        

        public ActionResult AgricultureNetworkSheet1(int? id)
        {
            if (Session["AgricultureNetworkSheet1"] != null)
            {
                if (id.HasValue)
                {
                    var userObj = db.AgricultureNetworkUsers.Find(id);
                    return View(userObj);
                }
                return View(new AgricultureNetworkUser());
            }
            return RedirectToAction("LoginToAgricultureNetworkUsers");
        }

        public ActionResult ChangeUI(string lang, string co, string ac, string navigateId)
        {
            Session["DefaultLang"] = lang.ToLower();
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            var routeValuesDictionary = new RouteValueDictionary();
            var qRequest = (HttpUtility.ParseQueryString(new Uri(Request.UrlReferrer.ToString()).Query));
            if (qRequest.AllKeys.Any())
                qRequest.AllKeys.ToList().ForEach(key => routeValuesDictionary.Add(key, qRequest.GetValues(key).FirstOrDefault()));
            if (!string.IsNullOrEmpty(navigateId))
                routeValuesDictionary.Add("id", navigateId);

            return RedirectToAction(ac, routeValuesDictionary);
        }

    }
}