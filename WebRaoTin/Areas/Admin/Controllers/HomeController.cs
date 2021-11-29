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
using System.Web.Security;

namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class HomeController : Controller
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

           


        }
        public HomeController()
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

        public void TinBDS_4Lastest()
        {
            var dsBatDongSan = db.BatDongSans.Include(b => b.LoaiBatDongSan).Include(b => b.TinTuc).ToList().OrderByDescending(v => v.Id);
            List<BatDongSan> batDongSans = new List<BatDongSan>(); int dem = 0;
            foreach (var item in dsBatDongSan)
            {
                if (dem == 4) break;
                batDongSans.Add(item);
                dem++;
            }

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in batDongSans)
            {
                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0, 25) + "...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageBatDongSan.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageBatDongSan = images[0] + ".jpg";

                a.Add(tinTucsViewModel);

            }

            ViewBag.TinBDS_4Lastest = a;
        }

        public void TinDV_4Lastest()
        {
            var dsDichVu = db.DichVus.Include(b => b.LoaiDichVu).Include(b => b.TinTuc).ToList().OrderByDescending(v => v.Id);
            List<DichVu> dichVus = new List<DichVu>(); int dem = 0;
            foreach (var item in dsDichVu)
            {
                if (dem == 4) break;
                dichVus.Add(item);
                dem++;
            }


            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in dichVus)
            {
                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0, 25) + "...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageDichVu.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageDichVu = images[0] + ".jpg";
                
                a.Add(tinTucsViewModel);

            }

            ViewBag.TinDV_4Lastest = a;
        }

        public void TinVL_4Lastest()
        {
            var dsViecLam = db.ViecLams.Include(b => b.LoaiViecLam).Include(b => b.TinTuc).ToList().OrderByDescending(v => v.Id);
            List<ViecLam> viecLams = new List<ViecLam>(); int dem = 0;
            foreach (var item in dsViecLam)
            {
                if (item.TinTuc.Status.Equals("Ẩn") || item.TinTuc.Status.Equals("Đã khóa"))
                {
                    continue;
                }

                if (dem == 4) break;
                viecLams.Add(item);
                dem++;
            }

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in viecLams)
            {

                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0, 25) + "...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageViecLam = images[0] + ".jpg";

                a.Add(tinTucsViewModel);

            }

            ViewBag.TinVL_4Lastest = a;
        }

        public void TinSP_4Lastest()
        {
            
            var dssanPhams = db.SanPhams.Include(b => b.LoaiSanPham).Include(b => b.TinTuc).ToList().OrderByDescending(v => v.Id);
            List<SanPham> sanPhams = new List<SanPham>(); int dem = 0;
            foreach (var item in dssanPhams)
            {
                if (item.TinTuc.Status.Equals("Ẩn") || item.TinTuc.Status.Equals("Đã khóa"))
                {
                    continue;
                }
                if (dem == 4) break;
                sanPhams.Add(item);
                dem++;
            }

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in sanPhams)
            {
                TinTucsViewModel tinTucsViewModel;
                if (item.Location.Length > 25)
                {
                    String str1 = item.Location;
                    item.Location = str1.Substring(0, 25) + "...";

                }

                tinTucsViewModel = new TinTucsViewModel(item.TinTuc, item);
                tinTucsViewModel.LuaChon = ngaygiodangTT(item.TinTuc.PublishDay);

                string[] chuoiSplit = new string[] { ".jpg" };
                string[] images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None);
                tinTucsViewModel.ImageSanPham = images[0] + ".jpg";
                
                a.Add(tinTucsViewModel);

            }

            

            ViewBag.TinSP_4Lastest = a;
        }

        public void TinTuc_4Lastest()
        {
            var dsTinTuc = db.TinTucs.ToList().OrderByDescending(v => v.Id);
            List<TinTuc> tinTucs = new List<TinTuc>(); int dem = 0; 
            foreach (var item in dsTinTuc)
            {
                if (item.Status.Equals("Ẩn") || item.Status.Equals("Đã khóa"))
                {
                    continue;
                }
                if (dem == 4) break;
                tinTucs.Add(item);
                dem++;
            }
            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            List<string> tenDanhMucs = new List<string>();

            foreach (var tintuc in tinTucs)
            {
                

                foreach (var item2 in SanPhams)
                {
                    if (item2.TinTucId == tintuc.Id)
                    {
                        TinTucsViewModel tinTucsViewModel;
                        string tenDM = "SẢN PHẨM";
                        tenDanhMucs.Add(tenDM);
                        if (item2.Location.Length > 25)
                        {
                            String str1 = item2.Location;
                            item2.Location = str1.Substring(0, 25) + "...";

                        }

                        tinTucsViewModel = new TinTucsViewModel(item2.TinTuc, item2);
                        tinTucsViewModel.LuaChon = ngaygiodangTT(item2.TinTuc.PublishDay);

                        string[] chuoiSplit = new string[] { ".jpg" };
                        string[] images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None);
                        tinTucsViewModel.ImageSanPham = images[0] + ".jpg";
                        
                        a.Add(tinTucsViewModel);

                        break;
                    }
                    

                }

                foreach (var item3 in ViecLams)
                {
                    if (item3.TinTucId == tintuc.Id)
                    {
                        string tenDM = "VIỆC LÀM";
                        tenDanhMucs.Add(tenDM);
                        TinTucsViewModel tinTucsViewModel;
                        if (item3.Location.Length > 25)
                        {
                            String str1 = item3.Location;
                            item3.Location = str1.Substring(0, 25) + "...";

                        }

                        tinTucsViewModel = new TinTucsViewModel(item3.TinTuc, item3);
                        tinTucsViewModel.LuaChon = ngaygiodangTT(item3.TinTuc.PublishDay);

                        string[] chuoiSplit = new string[] { ".jpg" };
                        string[] images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None);
                        tinTucsViewModel.ImageViecLam = images[0] + ".jpg";

                        a.Add(tinTucsViewModel);
                        break;
                    }
                    

                }

                foreach (var item4 in DichVus)
                {
                    if (item4.TinTucId == tintuc.Id)
                    {
                        TinTucsViewModel tinTucsViewModel;
                        string tenDM = "DỊCH VỤ";
                        tenDanhMucs.Add(tenDM);
                        if (item4.Location.Length > 25)
                        {
                            String str1 = item4.Location;
                            item4.Location = str1.Substring(0, 25) + "...";

                        }

                        tinTucsViewModel = new TinTucsViewModel(item4.TinTuc, item4);
                        tinTucsViewModel.LuaChon = ngaygiodangTT(item4.TinTuc.PublishDay);

                        string[] chuoiSplit = new string[] { ".jpg" };
                        string[] images = tinTucsViewModel.ImageDichVu.Split(chuoiSplit, StringSplitOptions.None);
                        tinTucsViewModel.ImageDichVu = images[0] + ".jpg";

                        a.Add(tinTucsViewModel);
                        break;
                    }
                    

                }

                foreach (var item5 in BatDongSans)
                {
                    if (item5.TinTucId == tintuc.Id)
                    {
                        TinTucsViewModel tinTucsViewModel;
                        string tenDM = "BÂT ĐỘNG SẢN";
                        tenDanhMucs.Add(tenDM);
                        if (item5.Location.Length > 25)
                        {
                            String str1 = item5.Location;
                            item5.Location = str1.Substring(0, 25) + "...";

                        }

                        tinTucsViewModel = new TinTucsViewModel(item5.TinTuc, item5);
                        tinTucsViewModel.LuaChon = ngaygiodangTT(item5.TinTuc.PublishDay);

                        string[] chuoiSplit = new string[] { ".jpg" };
                        string[] images = tinTucsViewModel.ImageBatDongSan.Split(chuoiSplit, StringSplitOptions.None);
                        tinTucsViewModel.ImageBatDongSan = images[0] + ".jpg";
                        
                        a.Add(tinTucsViewModel);
                        break;
                    }
                    

                }

            }


            ViewBag.TinTuc_4Lastest = a;
            ViewBag.TinTuc_TenDM_4Lastest = tenDanhMucs;
        }

        public ActionResult Index()
        {
            TinBDS_4Lastest();
            TinDV_4Lastest();
            TinVL_4Lastest();
            TinSP_4Lastest();
            TinTuc_4Lastest();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}