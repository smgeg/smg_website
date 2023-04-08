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
    public class CoursesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Courses
        public ActionResult Index()
        {
            var courseTypes = db.CourseTypes.ToList();
            var data = db.Courses.OrderByDescending(r => r.Id).OrderByDescending(o => o.IsEnable).ToList();
            data.ForEach(f =>
            {
                if (courseTypes.Any(x => x.Id == f.CourseTypeId))
                    f.CourseTypeNameAr = courseTypes.FirstOrDefault(x => x.Id == f.CourseTypeId).NameAr;
            });
            return View(data);
        }

        // GET: Admin/Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var courseTypes = db.CourseTypes.ToList();

            if (courseTypes.Any(x => x.Id == course.CourseTypeId))
                course.CourseTypeNameAr = courseTypes.FirstOrDefault(x => x.Id == course.CourseTypeId).NameAr;

            return View(course);
        }

        // GET: Admin/Courses/Create
        public ActionResult Create()
        {
            ViewBag.courseType = new SelectList(db.CourseTypes, "Id", "NameAr");
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,CourseTypeId,NameAr,NameEn,DescriptionAr,DescriptionEn,Price,HourlyDuration,LecturesCount,Location,DetailsAr,DetailsEn,TargetAr,TargetEn,ContentDetailsAr,ContentDetailsEn,CoursePersonsAr,CoursePersonsEn,_CourseDate,Code,IsEnable,courseApprovals")] Course course , HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(course._CourseDate))
                {
                    var date = DateTime.ParseExact(course._CourseDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    course.CourseDate= date;
                }

                //image
                course.Image = "default.png";
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/course"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    course.Image = guid;
                }


                course.CreatedOn = DateTime.Now;
                course.CreatedBy = User.Identity.Name;

                db.Courses.Add(course);
                db.SaveChanges();

                if (course.courseApprovals.Any())
                {
                    foreach (var approval in course.courseApprovals)
                    {
                        db.CourseApprovalCollections.Add(new CourseApprovalCollection 
                        {
                            CourseId = course.Id,
                            CourseApprovalId = Convert.ToInt32(approval)
                        });
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.courseType = new SelectList(db.CourseTypes, "Id", "NameAr");
            return View(course);
        }

        // GET: Admin/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            if (db.CourseApprovalCollections.Any(a=> a.CourseId == id))
            {
                var approvals = db.CourseApprovalCollections.Where(a => a.CourseId == id).Select(s => s.CourseApprovalId.ToString()).ToList();
                course.courseApprovals = approvals;
            }
            ViewBag.courseType = new SelectList(db.CourseTypes, "Id", "NameAr");
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,CourseTypeId,NameAr,NameEn,DescriptionAr,DescriptionEn,Price,HourlyDuration,LecturesCount,Location,DetailsAr,DetailsEn,TargetAr,TargetEn,ContentDetailsAr,ContentDetailsEn,CoursePersonsAr,CoursePersonsEn,_CourseDate,Image,Code,IsEnable,courseApprovals")] Course course, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(course._CourseDate))
                {
                    var date = DateTime.ParseExact(course._CourseDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    course.CourseDate = date;
                }

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/course"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    course.Image = guid;
                }

                if (db.CourseApprovalCollections.Any(w => w.CourseId == course.Id))
                    foreach (var appro in db.CourseApprovalCollections.Where(w => w.CourseId == course.Id))
                    {
                        db.CourseApprovalCollections.Remove(appro);
                    }

                if (course.courseApprovals.Any())
                {
                    foreach (var approval in course.courseApprovals)
                    {
                        db.CourseApprovalCollections.Add(new CourseApprovalCollection
                        {
                            CourseId = course.Id,
                            CourseApprovalId = Convert.ToInt32(approval)
                        });
                    }
                    //db.SaveChanges();
                }

                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseType = new SelectList(db.CourseTypes, "Id", "NameAr");
            return View(course);
        }

        // GET: Admin/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCourseApprovals()
        {

            var approvals = db.CourseApprovals.Select(appro => new
            {
                appro.Id,
                Name = appro.NameAr
            });

            return Json(approvals, JsonRequestBehavior.AllowGet);
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
