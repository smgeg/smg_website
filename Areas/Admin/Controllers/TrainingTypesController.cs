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
    public class TrainingTypesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/TrainingTypes
        public ActionResult Index()
        {
            return View(db.TrainingTypes.ToList());
        }

        // GET: Admin/TrainingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingTypes.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        // GET: Admin/TrainingTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TrainingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                trainingType.CreatedBy = User.Identity.Name;
                trainingType.CreatedOn = DateTime.Now;

                db.TrainingTypes.Add(trainingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainingType);
        }

        // GET: Admin/TrainingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingTypes.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        // POST: Admin/TrainingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                trainingType.UpdatedBy = User.Identity.Name;
                trainingType.UpdatedOn = DateTime.Now;

                db.Entry(trainingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingType);
        }

        // GET: Admin/TrainingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingType trainingType = db.TrainingTypes.Find(id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        // POST: Admin/TrainingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingType trainingType = db.TrainingTypes.Find(id);
            db.TrainingTypes.Remove(trainingType);
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
