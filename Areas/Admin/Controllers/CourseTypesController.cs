using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Context;
using Website.Models;

namespace Website.Areas.Admin.Controllers
{
    [Authorize]
    public class CourseTypesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/CourseTypes
        public ActionResult Index()
        {
            return View(db.CourseTypes.OrderByDescending(o=>o.IsEnable).ToList());
        }

        // GET: Admin/CourseTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // GET: Admin/CourseTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn,IsEnable")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                courseType.CreatedBy = User.Identity.Name;
                courseType.CreatedOn = DateTime.Now;
                db.CourseTypes.Add(courseType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseType);
        }

        // GET: Admin/CourseTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: Admin/CourseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn,IsEnable")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                courseType.UpdatedBy = User.Identity.Name;
                courseType.UpdatedOn = DateTime.Now;

                db.Entry(courseType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseType);
        }

        // GET: Admin/CourseTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = db.CourseTypes.Find(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: Admin/CourseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseType courseType = db.CourseTypes.Find(id);
            db.CourseTypes.Remove(courseType);
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
