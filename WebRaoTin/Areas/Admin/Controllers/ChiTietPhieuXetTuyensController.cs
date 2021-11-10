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
    public class ChiTietPhieuXetTuyensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ChiTietPhieuXetTuyens
        public ActionResult Index()
        {
            var chiTietPhieuXetTuyens = db.ChiTietPhieuXetTuyens.Include(c => c.PhieuXetUngTuyen).Include(c => c.ViecLam);
            return View(chiTietPhieuXetTuyens.ToList());
        }

        // GET: Admin/ChiTietPhieuXetTuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXetTuyen chiTietPhieuXetTuyen = db.ChiTietPhieuXetTuyens.Find(id);
            if (chiTietPhieuXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuXetTuyen);
        }

        // GET: Admin/ChiTietPhieuXetTuyens/Create
        public ActionResult Create()
        {
            ViewBag.PhieuXetUngTuyenId = new SelectList(db.PhieuXetUngTuyens, "Id", "Title");
            ViewBag.ViecLamId = new SelectList(db.ViecLams, "Id", "Name");
            return View();
        }

        // POST: Admin/ChiTietPhieuXetTuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhieuXetUngTuyenId,ViecLamId,AboutYou,Education,Experience")] ChiTietPhieuXetTuyen chiTietPhieuXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuXetTuyens.Add(chiTietPhieuXetTuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhieuXetUngTuyenId = new SelectList(db.PhieuXetUngTuyens, "Id", "Title", chiTietPhieuXetTuyen.PhieuXetUngTuyenId);
            ViewBag.ViecLamId = new SelectList(db.ViecLams, "Id", "Name", chiTietPhieuXetTuyen.ViecLamId);
            return View(chiTietPhieuXetTuyen);
        }

        // GET: Admin/ChiTietPhieuXetTuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXetTuyen chiTietPhieuXetTuyen = db.ChiTietPhieuXetTuyens.Find(id);
            if (chiTietPhieuXetTuyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhieuXetUngTuyenId = new SelectList(db.PhieuXetUngTuyens, "Id", "Title", chiTietPhieuXetTuyen.PhieuXetUngTuyenId);
            ViewBag.ViecLamId = new SelectList(db.ViecLams, "Id", "Name", chiTietPhieuXetTuyen.ViecLamId);
            return View(chiTietPhieuXetTuyen);
        }

        // POST: Admin/ChiTietPhieuXetTuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhieuXetUngTuyenId,ViecLamId,AboutYou,Education,Experience")] ChiTietPhieuXetTuyen chiTietPhieuXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuXetTuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhieuXetUngTuyenId = new SelectList(db.PhieuXetUngTuyens, "Id", "Title", chiTietPhieuXetTuyen.PhieuXetUngTuyenId);
            ViewBag.ViecLamId = new SelectList(db.ViecLams, "Id", "Name", chiTietPhieuXetTuyen.ViecLamId);
            return View(chiTietPhieuXetTuyen);
        }

        // GET: Admin/ChiTietPhieuXetTuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXetTuyen chiTietPhieuXetTuyen = db.ChiTietPhieuXetTuyens.Find(id);
            if (chiTietPhieuXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuXetTuyen);
        }

        // POST: Admin/ChiTietPhieuXetTuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuXetTuyen chiTietPhieuXetTuyen = db.ChiTietPhieuXetTuyens.Find(id);
            db.ChiTietPhieuXetTuyens.Remove(chiTietPhieuXetTuyen);
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
