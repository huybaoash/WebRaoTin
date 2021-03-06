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
    public class LoaiDichVusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/LoaiDichVus
        public ActionResult Index()
        {
            return View(db.LoaiDichVus.ToList());
        }

        // GET: Admin/LoaiDichVus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDichVu loaiDichVu = db.LoaiDichVus.Find(id);
            if (loaiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(loaiDichVu);
        }

        // GET: Admin/LoaiDichVus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiDichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] LoaiDichVu loaiDichVu)
        {
            loaiDichVu.Status = "Công khai";
            if (ModelState.IsValid)
            {
                db.LoaiDichVus.Add(loaiDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiDichVu);
        }

        // GET: Admin/LoaiDichVus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiDichVu loaiDichVu = db.LoaiDichVus.Find(id);
            if (loaiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(loaiDichVu);
        }

        // POST: Admin/LoaiDichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status")] LoaiDichVu loaiDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiDichVu);
        }

        public ActionResult Hide(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiDichVu loaiDichVu = db.LoaiDichVus.Find(id);
                loaiDichVu.Status = "Ẩn";

                db.Entry(loaiDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Show(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiDichVu loaiDichVu = db.LoaiDichVus.Find(id);
                loaiDichVu.Status = "Công khai";

                db.Entry(loaiDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/LoaiDichVus/Delete/5
        

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
