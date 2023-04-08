using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Website.Context;
using Website.Models;

namespace Website.Areas.Admin.Controllers
{
    [Authorize]
    public class JobUsersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Trainings
        public ActionResult Index()
        {
            var regiseredUsers = db.JobUserss.OrderByDescending(o => o.Id).ToList();

            var jobs = db.Jobs.ToList();
            regiseredUsers.ForEach(f =>
            {
                if (jobs.Any(d => d.Id == f.JobId))
                    f.JobName = jobs.FirstOrDefault(d => d.Id == f.JobId).NameAr;
            });
            return View(regiseredUsers);
        }

        public ActionResult OpenCV(int? reqId)
        {
            if (!reqId.HasValue)
            {
                return HttpNotFound();
            }

            var jobUser = db.JobUserss.Find(reqId);
            if (jobUser == null)
            {
                return HttpNotFound();
            }

            string cvUrl = System.Web.HttpContext.Current.Server.MapPath("~/template/img/cv/" + jobUser.CV); //Path;
            //byte[] FileBytes = System.IO.File.ReadAllBytes(cvUrl);
            //return File(FileBytes, "application/pdf");

            return File(cvUrl, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(cvUrl));

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
