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
    public class RegisterUsersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Trainings
        public ActionResult Index()
        {
            var regiseredUsers = db.RegisteredUsers.OrderByDescending(o => o.Id).ToList();

            var courses = db.Courses.ToList();
            var trainings = db.Trainings.ToList();
            regiseredUsers.ForEach(f =>
            {
                f.Type = f.CourseId.HasValue ? "دبلومة تدريبية" : "كورس تدريبى";
                f.CourseName = f.CourseId.HasValue ? (courses.Any(d => d.Id == f.CourseId) ? courses.FirstOrDefault(d => d.Id == f.CourseId).NameAr : "") : (trainings.Any(d => d.Id == f.TrainingId) ? trainings.FirstOrDefault(d => d.Id == f.TrainingId).NameAr : "");
            });
            return View(regiseredUsers);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            return View(registeredUser);
        }

        // POST: Admin/AgricultureNetworkUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            db.RegisteredUsers.Remove(registeredUser);
            db.SaveChanges();
            return RedirectToAction("Index");
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
