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
    public class LoaiViecLamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/LoaiViecLams
        public ActionResult Index()
        {
            return View(db.LoaiViecLams.ToList());
        }

        // GET: Admin/LoaiViecLams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
            if (loaiViecLam == null)
            {
                return HttpNotFound();
            }
            return View(loaiViecLam);
        }

        // GET: Admin/LoaiViecLams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiViecLams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status")] LoaiViecLam loaiViecLam)
        {
            if (ModelState.IsValid)
            {
                db.LoaiViecLams.Add(loaiViecLam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiViecLam);
        }

        // GET: Admin/LoaiViecLams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
            if (loaiViecLam == null)
            {
                return HttpNotFound();
            }
            return View(loaiViecLam);
        }

        // POST: Admin/LoaiViecLams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status")] LoaiViecLam loaiViecLam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiViecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiViecLam);
        }


        public ActionResult Hide(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
                loaiViecLam.Status = "Ẩn";

                db.Entry(loaiViecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Show(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
                loaiViecLam.Status = "Công khai";

                db.Entry(loaiViecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/LoaiViecLams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
            if (loaiViecLam == null)
            {
                return HttpNotFound();
            }
            return View(loaiViecLam);
        }

        // POST: Admin/LoaiViecLams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiViecLam loaiViecLam = db.LoaiViecLams.Find(id);
            db.LoaiViecLams.Remove(loaiViecLam);
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
