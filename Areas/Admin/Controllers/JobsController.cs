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
    public class JobsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Jobs
        public ActionResult Index()
        {
            return View(db.Jobs.Where(w=> w.IsActive).OrderByDescending(r => r.Id).ToList());
        }

        // GET: Admin/Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job Job = db.Jobs.Find(id);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        // GET: Admin/Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn,ShortDescriptionAr,ShortDescriptionEn,_ExpirationDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(job._ExpirationDate))
                {
                    var date = DateTime.ParseExact(job._ExpirationDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    job.ExpirationDate = date;
                }

                job.CreatedBy = User.Identity.Name;
                job.CreatedOn = DateTime.Now;
                job.IsActive = true;
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Admin/Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job Job = db.Jobs.Find(id);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        // POST: Admin/Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn,ShortDescriptionAr,ShortDescriptionEn,_ExpirationDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(job._ExpirationDate))
                {
                    var date = DateTime.ParseExact(job._ExpirationDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    job.ExpirationDate = date;
                }

                job.UpdatedBy = User.Identity.Name;
                job.UpdatedOn = DateTime.Now;
                job.IsActive = true;
                db.Entry(job).State = EntityState.Modified;

                db.Entry(job).Property(p => p.CreatedOn).IsModified = false;
                db.Entry(job).Property(p => p.CreatedBy).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Admin/Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job Job = db.Jobs.Find(id);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        // POST: Admin/Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            job.IsActive = false;
            //db.Jobs.Remove(Job);
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
