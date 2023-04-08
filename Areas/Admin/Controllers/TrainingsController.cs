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
    public class TrainingsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Trainings
        public ActionResult Index()
        {
            var TrainingTypes = db.TrainingTypes.OrderByDescending(r => r.Id).ToList();
            var data = db.Trainings.ToList();
            data.ForEach(f =>
            {
                if (TrainingTypes.Any(x => x.Id == f.TrainingTypeId))
                    f.TrainingTypeNameAr = TrainingTypes.FirstOrDefault(x => x.Id == f.TrainingTypeId).NameAr;
            });
            return View(data);
        }

        // GET: Admin/Trainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training Training = db.Trainings.Find(id);
            if (Training == null)
            {
                return HttpNotFound();
            }
            var TrainingTypes = db.TrainingTypes.ToList();

            if (TrainingTypes.Any(x => x.Id == Training.TrainingTypeId))
                Training.TrainingTypeNameAr = TrainingTypes.FirstOrDefault(x => x.Id == Training.TrainingTypeId).NameAr;

            return View(Training);
        }

        // GET: Admin/Trainings/Create
        public ActionResult Create()
        {
            ViewBag.trainingType = new SelectList(db.TrainingTypes, "Id", "NameAr");
            return View();
        }

        // POST: Admin/Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,TrainingTypeId,NameAr,NameEn,DescriptionAr,DescriptionEn,Price,HourlyDuration,LecturesCount,Location,DetailsAr,DetailsEn,TargetAr,TargetEn,ContentDetailsAr,ContentDetailsEn,CoursePersonsAr,CoursePersonsEn,_TrainingDate,Code")] Training Training , HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(Training._TrainingDate))
                {
                    var date = DateTime.ParseExact(Training._TrainingDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    Training.TrainingDate= date;
                }

                //image
                Training.Image = "default.png";
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Template/img/training"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    Training.Image = guid;
                }


                Training.CreatedOn = DateTime.Now;
                Training.CreatedBy = User.Identity.Name;

                db.Trainings.Add(Training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.trainingType = new SelectList(db.TrainingTypes, "Id", "NameAr");
            return View(Training);
        }

        // GET: Admin/Trainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training Training = db.Trainings.Find(id);
            if (Training == null)
            {
                return HttpNotFound();
            }


            ViewBag.trainingType = new SelectList(db.TrainingTypes, "Id", "NameAr");
            return View(Training);
        }

        // POST: Admin/Trainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,TrainingTypeId,NameAr,NameEn,DescriptionAr,DescriptionEn,Price,HourlyDuration,LecturesCount,Location,DetailsAr,DetailsEn,TargetAr,TargetEn,ContentDetailsAr,ContentDetailsEn,CoursePersonsAr,CoursePersonsEn,_TrainingDate,Image,Code")] Training Training, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(Training._TrainingDate))
                {
                    var date = DateTime.ParseExact(Training._TrainingDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    Training.TrainingDate = date;
                }

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Template/img/training"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    Training.Image = guid;
                }

                db.Entry(Training).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.trainingType = new SelectList(db.TrainingTypes, "Id", "NameAr");
            return View(Training);
        }

        // GET: Admin/Trainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training Training = db.Trainings.Find(id);
            if (Training == null)
            {
                return HttpNotFound();
            }
            return View(Training);
        }

        // POST: Admin/Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training Training = db.Trainings.Find(id);
            db.Trainings.Remove(Training);
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
