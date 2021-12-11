using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;
using WebRaoTin.Models;
using WebRaoTin.ViewModel;
using PagedList;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;

namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class PhieuXetUngTuyensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ICollection<SanPham> SanPhams = new List<SanPham>();
        ICollection<BatDongSan> BatDongSans = new List<BatDongSan>();
        ICollection<DichVu> DichVus = new List<DichVu>();
        ICollection<ViecLam> ViecLams = new List<ViecLam>();
        ICollection<BinhLuan> BinhLuans = new List<BinhLuan>();

        ICollection<LoaiSanPham> LoaiSanPhams = new List<LoaiSanPham>();
        ICollection<LoaiBatDongSan> LoaiBatDongSans = new List<LoaiBatDongSan>();
        ICollection<LoaiDichVu> LoaiDichVus = new List<LoaiDichVu>();
        ICollection<LoaiViecLam> LoaiViecLams = new List<LoaiViecLam>();

        private void GetRoleUser()
        {

            var dsUser = db.Users.ToList();
            ViewBag.dsUser = dsUser;


        }
        public PhieuXetUngTuyensController()
        {
            GetRoleUser();
            SanPhams = db.SanPhams.ToList();
            BatDongSans = db.BatDongSans.ToList();
            DichVus = db.DichVus.ToList();
            ViecLams = db.ViecLams.ToList();
            BinhLuans = db.BinhLuans.ToList();

            LoaiSanPhams = db.LoaiSanPhams.ToList();
            LoaiBatDongSans = db.LoaiBatDongSans.ToList();
            LoaiDichVus = db.LoaiDichVus.ToList();
            LoaiViecLams = db.LoaiViecLams.ToList();


            ViewBag.SanPhams = SanPhams;
            ViewBag.BatDongSans = BatDongSans;
            ViewBag.DichVus = DichVus;
            ViewBag.ViecLams = ViecLams;
            ViewBag.BinhLuans = BinhLuans;

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

        // GET: Admin/PhieuXetUngTuyens
        public ActionResult Index(int? TinTucId)
        {
            TinTuc tinTuc = db.TinTucs.Find(TinTucId);
            string NguoiDangID = tinTuc.CustomerID;
            ViewBag.NguoiDangID = NguoiDangID;
            var phieuXetUngTuyens = db.PhieuXetUngTuyens.Include(p => p.Customer).Include(p => p.ViecLam);

            return View(phieuXetUngTuyens.Where(t => t.ViecLam.TinTucId == TinTucId).ToList());
        }

        // GET: Admin/PhieuXetUngTuyens/Details/5
        public ActionResult Details(int? id)
        {
            int MaTinTuc = 0;


            foreach (var item in db.PhieuXetUngTuyens.ToList())
            {
                if (item.Id == id)
                {
                    foreach (var item2 in db.ViecLams.ToList())
                    {
                        if (item.ViecLamId == item2.Id)
                        {
                            MaTinTuc = item.ViecLam.TinTucId;
                            break;
                        }
                    }

                    object fileSavePath = Server.MapPath("~/Content/TinTuc/TinTucID" + MaTinTuc.ToString() + "/CV/") + item.Description;
                    ViewBag.fileSavePath = fileSavePath;
                    break;
                }
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            if (phieuXetUngTuyen == null)
            {
                return HttpNotFound();
            }

            TinTuc tinTuc = db.TinTucs.Find(MaTinTuc);
            phieuXetUngTuyen.ViecLam.TinTuc = tinTuc;
            string NguoiDangID = tinTuc.CustomerID;
            ViewBag.NguoiDangID = NguoiDangID;
            return View(phieuXetUngTuyen);
        }

        public ActionResult Details_Now(string userID, int? tintucID)
        {
            ViewBag.UserID_NguoiGuiCV = userID;
            int idPhieuXetUngTuyen = 0;



            foreach (var item in db.PhieuXetUngTuyens.ToList())
            {
                if (item.CustomerID.Equals(userID) && item.ViecLam.TinTucId == tintucID)
                {

                    idPhieuXetUngTuyen = item.Id;
                    object fileSavePath = Server.MapPath("~/Content/TinTuc/TinTucID" + tintucID + "/CV/") + item.Description;
                    ViewBag.fileSavePath = fileSavePath;
                    break;
                }
            }


            if (userID == null || tintucID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(idPhieuXetUngTuyen);
            if (phieuXetUngTuyen == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Details", "PhieuXetUngTuyens", new { id = phieuXetUngTuyen.Id });

        }

        // GET: Admin/PhieuXetUngTuyens/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role");
            ViewBag.ViecLamId = new SelectList(db.ViecLams, "Id", "Name");
            return View();
        }

        // POST: Admin/PhieuXetUngTuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhieuXetUngTuyen phieuXetUngTuyen, HttpPostedFileBase cv)
        {
            phieuXetUngTuyen.CustomerID = User.Identity.GetUserId();
            foreach (var item in db.Users.ToList())
            {
                if (item.Id.Equals(phieuXetUngTuyen.CustomerID)) phieuXetUngTuyen.Customer = item;
            }
            phieuXetUngTuyen.PublishDay = DateTime.Now;
            phieuXetUngTuyen.Status = "Đã gửi";
            int MaTinTuc = 0;
            foreach (var item in db.ViecLams.ToList())
            {
                if (phieuXetUngTuyen.ViecLamId.Equals(item.Id))
                {
                    MaTinTuc = item.TinTucId;
                }
            }
            if (cv?.ContentLength > 0)
            {
                var fileName = Path.GetFileName(cv.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + MaTinTuc.ToString() + "/CV"), fileName);
                var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + MaTinTuc.ToString() + "/CV"));
                phieuXetUngTuyen.Description = fileName;
                cv.SaveAs(path);

            }


            if (ModelState.IsValid)
            {
                db.PhieuXetUngTuyens.Add(phieuXetUngTuyen);
                db.SaveChanges();
                return RedirectToAction("Details", "TinTucs", new { id = MaTinTuc }); ;
            }


            return RedirectToAction("Details", "TinTucs", new { id = MaTinTuc }); ;
        }

        // GET: Admin/PhieuXetUngTuyens/Edit/5



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileResult DownloadFile(string fileName, int TinTucId)
        {
            //Build the File Path.
            string path = Server.MapPath("~/Content/TinTuc/TinTucID" + TinTucId.ToString() + "/CV/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public ActionResult XoaPST(int? id)
        {
            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            int MaTT = phieuXetUngTuyen.ViecLam.TinTucId;
            db.PhieuXetUngTuyens.Remove(phieuXetUngTuyen);
            db.SaveChanges();
            return RedirectToAction("Details", "TinTucs", new { id = MaTT });
        }

        public ActionResult AcceptedCV(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            phieuXetUngTuyen.Status = "Đã duyệt";

            if (ModelState.IsValid)
            {
                
                if (phieuXetUngTuyen == null)
                {
                    return HttpNotFound();
                }
                var existingEntity = db.PhieuXetUngTuyens.Find(phieuXetUngTuyen.Id);

                db.Entry(existingEntity).CurrentValues.SetValues(phieuXetUngTuyen);
                db.SaveChanges();
                return RedirectToAction("Index", new { TinTucId = phieuXetUngTuyen.ViecLam.TinTucId });
            }
            return RedirectToAction("Index","PhieuXetUngTuyens", new { TinTucId = phieuXetUngTuyen.ViecLam.TinTucId });
        }

        public ActionResult DeniedCV(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuXetUngTuyen phieuXetUngTuyen = db.PhieuXetUngTuyens.Find(id);
            phieuXetUngTuyen.Status = "Đã từ chối";

            if (ModelState.IsValid)
            {
               
                if (phieuXetUngTuyen == null)
                {
                    return HttpNotFound();
                }
                var existingEntity = db.PhieuXetUngTuyens.Find(phieuXetUngTuyen.Id);

                db.Entry(existingEntity).CurrentValues.SetValues(phieuXetUngTuyen);
                db.SaveChanges();
                return RedirectToAction("Index", new { TinTucId = phieuXetUngTuyen.ViecLam.TinTucId });
            }
            return RedirectToAction("Index", new { TinTucId = phieuXetUngTuyen.ViecLam.TinTucId });
        }
    }
}
