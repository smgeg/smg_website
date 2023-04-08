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
    public class GalleriesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Galleries
        public ActionResult Index()
        {
            return View(db.Galleries.OrderByDescending(r => r.Id).ToList());
        }

        // GET: Admin/Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Date,_date,NameAr,NameEn,DescriptionAr,DescriptionEn,LocationAr,LocationEn")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(gallery._date))
                {
                    var date = DateTime.ParseExact(gallery._date, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    gallery.Date = date;
                }

                gallery.CreatedOn = DateTime.Now;
                gallery.CreatedBy = User.Identity.Name;

                db.Galleries.Add(gallery);
                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var guid = Guid.NewGuid().ToString();
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/gallery/" + gallery.Id); //Path 
                        Directory.CreateDirectory(path); 
                        guid = guid + ".jpg";

                        //set the image path
                        string imgPath = Path.Combine(path, guid);
                        file.SaveAs(imgPath);
                        db.GalleryImages.Add(new GalleryImage
                        {
                            GalleryId = gallery.Id,
                            Image = guid
                        });
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Admin/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Date,_date,NameAr,NameEn,DescriptionAr,DescriptionEn,LocationAr,LocationEn")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                //date
                if (!string.IsNullOrEmpty(gallery._date))
                {
                    var date = DateTime.ParseExact(gallery._date, "dd-MM-yyyy", Thread.CurrentThread.CurrentUICulture);

                    gallery.Date = date;
                }

                gallery.UpdatedOn = DateTime.Now;
                gallery.UpdatedBy = User.Identity.Name;

                db.Entry(gallery).State = EntityState.Modified;

                db.Entry(gallery).Property(p => p.CreatedOn).IsModified = false;
                db.Entry(gallery).Property(p => p.CreatedBy).IsModified = false;

                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    var gallerImages = db.GalleryImages.Where(w=> w.GalleryId == gallery.Id);
                    if (gallerImages.Any())
                    {
                        gallerImages.ForEachAsync(f =>
                        {
                            db.GalleryImages.Remove(f);
                        });
                    }
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var guid = Guid.NewGuid().ToString();
                        string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/gallery/" + gallery.Id); //Path 
                        //Directory.Delete(path);
                        guid = guid + ".jpg";

                        //set the image path
                        string imgPath = Path.Combine(path, guid);
                        file.SaveAs(imgPath);
                        db.GalleryImages.Add(new GalleryImage
                        {
                            GalleryId = gallery.Id,
                            Image = guid
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
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
