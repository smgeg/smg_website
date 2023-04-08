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
    public class CourseApprovalsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/CourseApprovals
        public ActionResult Index()
        {
            return View(db.CourseApprovals.OrderByDescending(o=>o.Id).ToList());
        }

        // GET: Admin/CourseApprovals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseApproval CourseApproval = db.CourseApprovals.Find(id);
            if (CourseApproval == null)
            {
                return HttpNotFound();
            }
            return View(CourseApproval);
        }

        // GET: Admin/CourseApprovals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseApprovals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,_startDate,_endDate")] CourseApproval CourseApproval, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //start date
                if (!string.IsNullOrEmpty(CourseApproval._startDate))
                {
                    var date = DateTime.ParseExact(CourseApproval._startDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    CourseApproval.StartDate = date;
                }
                //end date
                if (!string.IsNullOrEmpty(CourseApproval._endDate))
                {
                    var date = DateTime.ParseExact(CourseApproval._endDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    CourseApproval.EndDate = date;
                }

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/course/approval"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    CourseApproval.Image = guid;
                }

                db.CourseApprovals.Add(CourseApproval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CourseApproval);
        }

        // GET: Admin/CourseApprovals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseApproval CourseApproval = db.CourseApprovals.Find(id);
            if (CourseApproval == null)
            {
                return HttpNotFound();
            }
            return View(CourseApproval);
        }

        // POST: Admin/CourseApprovals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,,_startDate,_endDate,Image")] CourseApproval CourseApproval, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //start date
                if (!string.IsNullOrEmpty(CourseApproval._startDate))
                {
                    var date = DateTime.ParseExact(CourseApproval._startDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    CourseApproval.StartDate = date;
                }
                //end date
                if (!string.IsNullOrEmpty(CourseApproval._endDate))
                {
                    var date = DateTime.ParseExact(CourseApproval._endDate, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    CourseApproval.EndDate = date;
                }

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/course/approval"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    CourseApproval.Image = guid;
                }

                db.Entry(CourseApproval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CourseApproval);
        }

        // GET: Admin/CourseApprovals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseApproval CourseApproval = db.CourseApprovals.Find(id);
            if (CourseApproval == null)
            {
                return HttpNotFound();
            }
            return View(CourseApproval);
        }

        // POST: Admin/CourseApprovals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseApproval CourseApproval = db.CourseApprovals.Find(id);
            db.CourseApprovals.Remove(CourseApproval);
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
