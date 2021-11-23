using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebRaoTin.Models;
using WebRaoTin.ViewModel;

namespace WebRaoTin.Areas.Admin.Controllers
{
    public class ViecLamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ICollection<SanPham> SanPhams = new List<SanPham>();
        ICollection<BatDongSan> BatDongSans = new List<BatDongSan>();
        ICollection<DichVu> DichVus = new List<DichVu>();
        ICollection<ViecLam> ViecLams = new List<ViecLam>();

        ICollection<LoaiSanPham> LoaiSanPhams = new List<LoaiSanPham>();
        ICollection<LoaiBatDongSan> LoaiBatDongSans = new List<LoaiBatDongSan>();
        ICollection<LoaiDichVu> LoaiDichVus = new List<LoaiDichVu>();
        ICollection<LoaiViecLam> LoaiViecLams = new List<LoaiViecLam>();

        private void GetRoleUser()
        {

            
        }
        public ViecLamsController()
        {
            GetRoleUser();
            SanPhams = db.SanPhams.ToList();
            BatDongSans = db.BatDongSans.ToList();
            DichVus = db.DichVus.ToList();
            ViecLams = db.ViecLams.ToList();

            LoaiSanPhams = db.LoaiSanPhams.ToList();
            LoaiBatDongSans = db.LoaiBatDongSans.ToList();
            LoaiDichVus = db.LoaiDichVus.ToList();
            LoaiViecLams = db.LoaiViecLams.ToList();


            ViewBag.SanPhams = SanPhams;
            ViewBag.BatDongSans = BatDongSans;
            ViewBag.DichVus = DichVus;
            ViewBag.ViecLams = ViecLams;


            ViewBag.LoaiSanPhams = LoaiSanPhams;
            ViewBag.LoaiBatDongSans = LoaiBatDongSans;
            ViewBag.LoaiDichVus = LoaiDichVus;
            ViewBag.LoaiViecLams = LoaiViecLams;

            ViewBag.SLLoaiSanPhams = LoaiSanPhams.Count();
            ViewBag.SLLoaiBatDongSans = LoaiBatDongSans.Count();
            ViewBag.SLLoaiDichVus = LoaiDichVus.Count();
            ViewBag.SLLoaiViecLams = LoaiViecLams.Count();

            ViewBag.SLSanPhams = SanPhams.Count();
            ViewBag.SLBatDongSans = BatDongSans.Count();
            ViewBag.SLDichVus = DichVus.Count();
            ViewBag.SLViecLams = ViecLams.Count();
        }
        // GET: Admin/ViecLams

        public string ngaygiodangTT(DateTime ngaydang)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - ngaydang.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 giây trước" : ts.Seconds + "giây trước";

            if (delta < 2 * MINUTE)
                return "1 phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "1 giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "Hôm qua";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 năm trước" : years + " năm trước";
            }
        }

        public ActionResult Index(string searchString, int? page)
        {
            int recordsPerPage = 10;

            if (!page.HasValue)
            {
                page = 1; // set initial page value
            }
            ViewBag.Keyword = searchString;

            var viecLams = db.ViecLams.Include(s => s.LoaiViecLam).Include(v => v.TinTuc).ToList();

            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    viecLams = viecLams.Where(s => s.TinTuc.Title.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }
            catch (Exception ex) { }
            viecLams.OrderByDescending(v => v.Id);
            var finalList = viecLams.OrderByDescending(v => v.Id).ToPagedList(page.Value, recordsPerPage);

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in viecLams)
            {
                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0,25) + " ...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageViecLam = images[0] + ".jpg";
                a.Add(tinTucsViewModel);

            }

            ViewBag.ngaygio = a;
            return View(finalList);
        }

        public ActionResult Index_LoaiVL(string searchString, int? page)
        {
            int recordsPerPage = 5;

            if (!page.HasValue)
            {
                page = 1; // set initial page value
            }
            ViewBag.Keyword = searchString;

            var viecLams = db.ViecLams.Include(s => s.LoaiViecLam).Include(v => v.TinTuc).ToList();

            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    viecLams = viecLams.Where(s => s.LoaiViecLam.Name.Equals(searchString)).ToList();
                }
            }
            catch (Exception ex) { }
            viecLams.OrderByDescending(v => v.Id);
            var finalList = viecLams.OrderByDescending(v => v.Id).ToPagedList(page.Value, recordsPerPage);

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in viecLams)
            {
                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0, 25) + " ...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageViecLam = images[0] + ".jpg";
                a.Add(tinTucsViewModel);

            }

            ViewBag.ngaygio = a;
            return View(finalList);
        }

        // GET: Admin/ViecLams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLams.Find(id);
            if (viecLam == null)
            {
                return HttpNotFound();
            }
            return View(viecLam);
        }

        // GET: Admin/ViecLams/Create
        public ActionResult Create()
        {
            ViewBag.LoaiViecLamId = new SelectList(db.LoaiViecLams, "Id", "Name");
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title");
            return View();
        }

        // POST: Admin/ViecLams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Require,Salary,Location,Image,TinTucId,LoaiViecLamId")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
                db.ViecLams.Add(viecLam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiViecLamId = new SelectList(db.LoaiViecLams, "Id", "Name", viecLam.LoaiViecLamId);
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", viecLam.TinTucId);
            return View(viecLam);
        }

        // GET: Admin/ViecLams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLams.Find(id);
            if (viecLam == null)
            {
                return HttpNotFound();
            }
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", viecLam.TinTucId);
            return View(viecLam);
        }

        // POST: Admin/ViecLams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Require,Salary,Location,Image,TinTucId,LoaiViecLamId")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiViecLamId = new SelectList(db.LoaiViecLams, "Id", "Name", viecLam.LoaiViecLamId);
            ViewBag.TinTucId = new SelectList(db.TinTucs, "Id", "Title", viecLam.TinTucId);
            return View(viecLam);
        }

        // GET: Admin/ViecLams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLams.Find(id);
            if (viecLam == null)
            {
                return HttpNotFound();
            }
            return View(viecLam);
        }

        // POST: Admin/ViecLams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViecLam viecLam = db.ViecLams.Find(id);
            db.ViecLams.Remove(viecLam);
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
