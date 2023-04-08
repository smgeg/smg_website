using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Context;
using Website.Models;

namespace Website.Areas.Admin.Controllers
{
    [Authorize]
    public class BannersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Banners
        public ActionResult Index()
        {
            return View(db.Banners.OrderByDescending(r => r.Id).ToList());
        }

        // GET: Admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banner = db.Banners.Find(id);
            if (Banner == null)
            {
                return HttpNotFound();
            }
            return View(Banner);
        }

        // GET: Admin/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn")] Banner Banner , HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //image
                var guid = Guid.NewGuid().ToString();

                Banner.Image = "default.png";
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/banner"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    Banner.Image = guid;
                }

                Banner.CreatedBy = User.Identity.Name;
                Banner.CreatedOn = DateTime.Now;
                db.Banners.Add(Banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Banner);
        }

        // GET: Admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banner = db.Banners.Find(id);
            if (Banner == null)
            {
                return HttpNotFound();
            }
            return View(Banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,DescriptionAr,DescriptionEn,Image")] Banner Banner, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {

                Banner.UpdatedBy = User.Identity.Name;
                Banner.UpdatedOn = DateTime.Now;

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/banner"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    Banner.Image = guid;
                }

                db.Entry(Banner).State = EntityState.Modified;

                db.Entry(Banner).Property(p => p.CreatedOn).IsModified = false;
                db.Entry(Banner).Property(p => p.CreatedBy).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Banner);
        }

        // GET: Admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner Banner = db.Banners.Find(id);
            if (Banner == null)
            {
                return HttpNotFound();
            }
            return View(Banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner Banner = db.Banners.Find(id);
            db.Banners.Remove(Banner);
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
