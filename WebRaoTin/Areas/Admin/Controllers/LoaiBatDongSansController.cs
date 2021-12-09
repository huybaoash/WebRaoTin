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
    public class LoaiBatDongSansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/LoaiBatDongSans
        public ActionResult Index()
        {
            return View(db.LoaiBatDongSans.ToList());
        }

        // GET: Admin/LoaiBatDongSans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBatDongSan loaiBatDongSan = db.LoaiBatDongSans.Find(id);
            if (loaiBatDongSan == null)
            {
                return HttpNotFound();
            }
            return View(loaiBatDongSan);
        }

        // GET: Admin/LoaiBatDongSans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiBatDongSans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] LoaiBatDongSan loaiBatDongSan)
        {
            loaiBatDongSan.Status = "Công khai";
            if (ModelState.IsValid)
            {
                db.LoaiBatDongSans.Add(loaiBatDongSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiBatDongSan);
        }

        // GET: Admin/LoaiBatDongSans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBatDongSan loaiBatDongSan = db.LoaiBatDongSans.Find(id);
            if (loaiBatDongSan == null)
            {
                return HttpNotFound();
            }
            return View(loaiBatDongSan);
        }

        public ActionResult Hide(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiBatDongSan loaiBatDongSan = db.LoaiBatDongSans.Find(id);
                loaiBatDongSan.Status = "Ẩn";

                db.Entry(loaiBatDongSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Show(int? id)
        {

            if (ModelState.IsValid)
            {
                LoaiBatDongSan loaiBatDongSan = db.LoaiBatDongSans.Find(id);
                loaiBatDongSan.Status = "Công khai";

                db.Entry(loaiBatDongSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/LoaiBatDongSans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status")] LoaiBatDongSan loaiBatDongSan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiBatDongSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiBatDongSan);
        }

        // GET: Admin/LoaiBatDongSans/Delete/5
       
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
