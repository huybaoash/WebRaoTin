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
    public class PhieuXetUngTuyensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/PhieuXetUngTuyens
        public ActionResult Index()
        {
            var phieuXetUngTuyens = db.PhieuXetUngTuyens.Include(p => p.Customer);
            return View(phieuXetUngTuyens.ToList());
        }

        // GET: Admin/PhieuXetUngTuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            if (phieuXetUngTuyen == null)
            {
                return HttpNotFound();
            }
            return View(phieuXetUngTuyen);
        }

        // GET: Admin/PhieuXetUngTuyens/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role");
            return View();
        }

        // POST: Admin/PhieuXetUngTuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,PublishDay,Status,CustomerID")] PhieuXetUngTuyen phieuXetUngTuyen)
        {
            if (ModelState.IsValid)
            {
                db.PhieuXetUngTuyens.Add(phieuXetUngTuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", phieuXetUngTuyen.CustomerID);
            return View(phieuXetUngTuyen);
        }

        // GET: Admin/PhieuXetUngTuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            if (phieuXetUngTuyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", phieuXetUngTuyen.CustomerID);
            return View(phieuXetUngTuyen);
        }

        // POST: Admin/PhieuXetUngTuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,PublishDay,Status,CustomerID")] PhieuXetUngTuyen phieuXetUngTuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuXetUngTuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role", phieuXetUngTuyen.CustomerID);
            return View(phieuXetUngTuyen);
        }

        // GET: Admin/PhieuXetUngTuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            if (phieuXetUngTuyen == null)
            {
                return HttpNotFound();
            }
            return View(phieuXetUngTuyen);
        }

        // POST: Admin/PhieuXetUngTuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            db.PhieuXetUngTuyens.Remove(phieuXetUngTuyen);
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
