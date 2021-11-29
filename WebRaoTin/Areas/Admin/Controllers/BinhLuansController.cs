using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebRaoTin.Models;

namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class BinhLuansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/BinhLuans
        public ActionResult Index()
        {
            var binhLuans = db.BinhLuans.Include(b => b.Customer).Include(b => b.TinTuc);
            return View(binhLuans.ToList());
        }

        public ActionResult CMT_ofUser(string id)
        {
            var binhLuans_Temp = db.BinhLuans.Include(b => b.Customer).Include(b => b.TinTuc).ToList();
            var binhLuans = binhLuans_Temp.Where(p => p.CustomerID.Equals(id)) ;
            
            

            return View(binhLuans);
        }

        // GET: Admin/BinhLuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role");
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title");
            return View();
        }

        // POST: Admin/BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PublishDay,Description,CustomerID,TinTucId")] BinhLuan binhLuan)
        {
            binhLuan.PublishDay = DateTime.Now;
            binhLuan.CustomerID = User.Identity.GetUserId();
            if (db.BinhLuans.ToList().Count == 0) binhLuan.Id = 1;
            else if (db.BinhLuans.ToList().Count < 2) binhLuan.Id = 2;
            else binhLuan.Id = db.BinhLuans.ToList().Last().Id + 1;

            if (ModelState.IsValid)
            {
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("Details", "TinTucs", new { id = binhLuan.TinTucId });
            }

            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", binhLuan.CustomerID);
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", binhLuan.TinTucId);
            return RedirectToAction("Details", "TinTucs",new { id = binhLuan.TinTucId });
        }

        // GET: Admin/BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", binhLuan.CustomerID);
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", binhLuan.TinTucId);
            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PublishDay,Description,CustomerID,TinTucId")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", binhLuan.CustomerID);
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", binhLuan.TinTucId);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            db.BinhLuans.Remove(binhLuan);
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

        public ActionResult XoaBL(int? id)
        {
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            db.BinhLuans.Remove(binhLuan);
            db.SaveChanges();
            return RedirectToAction("Details", "TinTucs", new { id = binhLuan.TinTucId });
        }
    }
}
