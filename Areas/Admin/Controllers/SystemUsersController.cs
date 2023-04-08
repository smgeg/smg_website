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
    public class SystemUsersController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/AgricultureNetworkUsers
        public ActionResult Index()
        {
            return View(db.AgricultureNetworkUsers.Where(w=> w.IsActive).OrderByDescending(r=> r.Id));
        }

        // GET: Admin/AgricultureNetworkUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureNetworkUser AgricultureNetworkUser = db.AgricultureNetworkUsers.Find(id);
            if (AgricultureNetworkUser == null)
            {
                return HttpNotFound();
            }
            return View(AgricultureNetworkUser);
        }

        // GET: Admin/AgricultureNetworkUsers/Create
        public ActionResult Create(int? courseUserId)
        {
            if (courseUserId.HasValue)
            {
                var courseUser = db.RegisteredUsers.Find(courseUserId.Value);
                if (courseUser != null)
                {
                    var user = new AgricultureNetworkUser
                    { 
                        UserType = "u",
                        NameAr = courseUser.Name,
                        NameEn = courseUser.Name,
                        Email = courseUser.Email,
                        MobileNo = courseUser.Mobile,
                    };

                    return View(user);
                }
            }
            return View();
        }

        // POST: Admin/AgricultureNetworkUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,NameAr,NameEn,MobileNo,Email,UserName,Password,UserType,userCourses")] AgricultureNetworkUser agricultureNetworkUser, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //check for existance of username
                if (db.AgricultureNetworkUsers.Any(a=> a.UserName.ToLower() == agricultureNetworkUser.UserName.ToLower()))
                {
                    ModelState.AddModelError("UserName" , "هذا الاسم مسجل من قبل");
                    return View(agricultureNetworkUser);
                }

                agricultureNetworkUser.CreatedBy = User.Identity.Name;
                agricultureNetworkUser.CreatedOn = DateTime.Now;

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/agrinetworkusers"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    agricultureNetworkUser.Image = guid;
                }

                agricultureNetworkUser.IsActive = true;
                db.AgricultureNetworkUsers.Add(agricultureNetworkUser);
                db.SaveChanges();

                if (agricultureNetworkUser.userCourses.Any())
                {
                    foreach (var course in agricultureNetworkUser.userCourses)
                    {
                        db.UserCourseCollections.Add(new UserCourseCollection
                        {
                            UserId = agricultureNetworkUser.Id,
                            CourseId = Convert.ToInt32(course)
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(agricultureNetworkUser);
        }

        // GET: Admin/AgricultureNetworkUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureNetworkUser AgricultureNetworkUser = db.AgricultureNetworkUsers.Find(id);
            if (AgricultureNetworkUser == null)
            {
                return HttpNotFound();
            }

            if(db.UserCourseCollections.Any(a => a.UserId == id))
            {
                var courses = db.UserCourseCollections.Where(a => a.UserId == id).Select(s => s.CourseId.ToString()).ToList();
                AgricultureNetworkUser.userCourses = courses;
            }

            return View(AgricultureNetworkUser);
        }

        // POST: Admin/AgricultureNetworkUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,NameAr,NameEn,MobileNo,Email,UserName,Password,Image,UserType,userCourses")] AgricultureNetworkUser agricultureNetworkUser, HttpPostedFileBase _img)
        {
            if (ModelState.IsValid)
            {
                //check for existance of username
                var currentObj = db.AgricultureNetworkUsers.FirstOrDefault(a => a.Id == agricultureNetworkUser.Id);
                db.Entry(currentObj).State = EntityState.Detached;
                if (currentObj == null)
                {
                    return HttpNotFound();
                }

                string userName = currentObj.UserName;
                if (userName.ToLower() != agricultureNetworkUser.UserName.ToLower() && db.AgricultureNetworkUsers.Any(a => a.UserName.ToLower() == agricultureNetworkUser.UserName.ToLower()))
                {
                    ModelState.AddModelError("UserName", "هذا الاسم مسجل من قبل");
                    return View(agricultureNetworkUser);
                }

                agricultureNetworkUser.UpdatedBy = User.Identity.Name;
                agricultureNetworkUser.UpdatedOn = DateTime.Now;
                agricultureNetworkUser.IsActive = true;

                //image
                var guid = Guid.NewGuid().ToString();
                if (_img != null && _img.ContentLength > 0)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/template/img/agrinetworkusers"); //Path  
                    guid = guid + ".jpg";

                    //set the image path
                    string imgPath = Path.Combine(path, guid);
                    _img.SaveAs(imgPath);
                    agricultureNetworkUser.Image = guid;
                }


                if (db.UserCourseCollections.Any(w => w.UserId == agricultureNetworkUser.Id))
                    foreach (var course in db.UserCourseCollections.Where(w => w.UserId == agricultureNetworkUser.Id))
                    {
                        db.UserCourseCollections.Remove(course);
                    }

                if (agricultureNetworkUser.userCourses.Any())
                {
                    foreach (var course in agricultureNetworkUser.userCourses)
                    {
                        db.UserCourseCollections.Add(new UserCourseCollection
                        {
                            UserId = agricultureNetworkUser.Id,
                            CourseId = Convert.ToInt32(course)
                        });
                    }
                    //db.SaveChanges();
                }

                db.Entry(agricultureNetworkUser).State = EntityState.Modified;

                db.Entry(agricultureNetworkUser).Property(p => p.CreatedOn).IsModified = false;
                db.Entry(agricultureNetworkUser).Property(p => p.CreatedBy).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agricultureNetworkUser);
        }

        // GET: Admin/AgricultureNetworkUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureNetworkUser AgricultureNetworkUser = db.AgricultureNetworkUsers.Find(id);
            if (AgricultureNetworkUser == null)
            {
                return HttpNotFound();
            }
            return View(AgricultureNetworkUser);
        }

        // POST: Admin/AgricultureNetworkUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgricultureNetworkUser AgricultureNetworkUser = db.AgricultureNetworkUsers.Find(id);
            AgricultureNetworkUser.IsActive = false;
            //db.AgricultureNetworkUsers.Remove(AgricultureNetworkUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetUserCourses()
        {

            var approvals = db.Courses.Where(w=> w.IsEnable).Select(appro => new
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
