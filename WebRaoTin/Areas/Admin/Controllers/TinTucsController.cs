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
using Newtonsoft.Json;


namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class TinTucsController : Controller
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

        public ActionResult Index(string searchString, int? page)
        {
            int recordsPerPage = 10;

            if (!page.HasValue)
            {
                page = 1; // set initial page value
            }
            ViewBag.Keyword = searchString;

            var tinTucs = db.TinTucs.Include(t => t.Customer).ToList();
            var sanpham = db.SanPhams.Include(t => t.TinTuc).Include(t => t.LoaiSanPham).ToList();

            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    tinTucs = tinTucs.Where(s => s.Title.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }
            catch (Exception ex) { }
            tinTucs.OrderByDescending(v => v.Id);

            var finalList = tinTucs.OrderByDescending(v => v.Id).ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }
        public TinTucsController()
        {
            GetRoleUser();
            SanPhams = db.SanPhams.ToList();
            BatDongSans = db.BatDongSans.ToList();
            DichVus = db.DichVus.ToList();
            ViecLams = db.ViecLams.ToList();
            BinhLuans = db.BinhLuans.ToList();

            LoaiSanPhams = db.LoaiSanPhams.Where(p => p.Status.Equals("Công khai")).ToList();
            LoaiBatDongSans = db.LoaiBatDongSans.Where(p => p.Status.Equals("Công khai")).ToList();
            LoaiDichVus = db.LoaiDichVus.Where(p => p.Status.Equals("Công khai")).ToList();
            LoaiViecLams = db.LoaiViecLams.Where(p => p.Status.Equals("Công khai")).ToList();


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

        public string ngaygiohethanTT(DateTime hanchot)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(hanchot.Ticks - DateTime.Now.Ticks);
            double delta = ts.TotalSeconds;

            if (delta < 0) return "Đã hết hạn";

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "Còn 1 giây nữa" : "Còn " + ts.Seconds + "giây nữa";

            if (delta < 2 * MINUTE)
                return "Còn 1 phút nữa";

            if (delta < 45 * MINUTE)
                return "Còn " + ts.Minutes + " phút nữa";

            if (delta < 90 * MINUTE)
                return "Còn 1 giờ nữa";

            if (delta < 24 * HOUR)
                return "Còn " + ts.Hours + " phút nữa";

            if (delta < 48 * HOUR)
                return "Còn 1 ngày nữa";

            if (delta < 30 * DAY)
                return "Còn " + ts.Days + " ngày nữa";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "Còn 1 tháng nữa" : "Còn " + months + " tháng nữa";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "Còn 1 năm nữa" : "Còn " + years + " năm nữa";
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
                if (dem == 4) break;
                viecLams.Add(item);
                dem++;
            }

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in viecLams)
            {
                TinTucsViewModel tinTucsViewModel;
               

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
                if (dem == 4) break;
                sanPhams.Add(item);
                dem++;
            }

            List<TinTucsViewModel> a = new List<TinTucsViewModel>();
            foreach (var item in sanPhams)
            {
                TinTucsViewModel tinTucsViewModel;
          

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

        public void TinTuc_4NoiBat()
        {
            var dsTinTuc = db.TinTucs.ToList().OrderByDescending(v => v.LuotXem);
            List<TinTuc> tinTucs = new List<TinTuc>(); int dem = 0;
            foreach (var item in dsTinTuc)
            {
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


            ViewBag.TinTuc_4NoiBat = a;
            ViewBag.TinTuc_TenDM_4NoiBat = tenDanhMucs;
        }


        [Authorize]
        public ActionResult Index_ofUser(string searchString, int? page, string id)
        {
            int recordsPerPage = 500;

            if (!page.HasValue)
            {
                page = 1; // set initial page value
            }
            ViewBag.Keyword = searchString;

            var tinTucs_Temp = db.TinTucs.Include(t => t.Customer).ToList();
            var user_current = db.Users.Find(id);
            ViewBag.user_current = user_current;


            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    tinTucs_Temp = tinTucs_Temp.Where(s => s.Title.Contains(searchString)).ToList();
                }
            }
            catch (Exception ex) { }

            var tinTucs = tinTucs_Temp.OrderByDescending(v => v.Id).Where(p => p.CustomerID.Equals(id));




            List<TinTucsViewModel> dsTinTuc = new List<TinTucsViewModel>();
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

                        dsTinTuc.Add(tinTucsViewModel);

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

                        dsTinTuc.Add(tinTucsViewModel);
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

                        dsTinTuc.Add(tinTucsViewModel);
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

                        dsTinTuc.Add(tinTucsViewModel);
                        break;
                    }


                }

            }

            ViewBag.TinTuc_TenDM_4Lastest = tenDanhMucs;

            var finalDSTT = dsTinTuc.ToPagedList(page.Value, recordsPerPage);

            return View(finalDSTT);
        }

        // GET: Admin/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            TinTuc_4NoiBat();
            ApplicationUser user = null;
            foreach (var item in db.Users.ToList())
            {
                if (item.Id.Equals(User.Identity.GetUserId()))
                {
                    user = item;
                }
            }
            ViewBag.currentUser = user;

            int kiemtra_hasSentCV = 0;
            PhieuXetUngTuyen phieuXetUngTuyen = new PhieuXetUngTuyen();
            foreach (var item in db.PhieuXetUngTuyens.ToList())
            {
                if (item.CustomerID.Equals(User.Identity.GetUserId()) && item.ViecLam.TinTucId == id)
                {
                    kiemtra_hasSentCV = 1;
                    phieuXetUngTuyen = item;
                }

            }
            ViewBag.phieuXetUngTuyen = phieuXetUngTuyen;
            ViewBag.kiemtra_hasSentCV = kiemtra_hasSentCV;



            List<BinhLuanViewModel> dsBLcuaTT = new List<BinhLuanViewModel>();
            foreach (var item in BinhLuans)
            {
                if (item.TinTucId.Equals(id))
                {



                    BinhLuanViewModel bl = new BinhLuanViewModel()
                    {

                        CustomerID = item.CustomerID,
                        Description = item.Description,
                        PublishDay = item.PublishDay,
                        Id = item.Id,
                        TinTucId = item.TinTucId
                    };
                    dsBLcuaTT.Add(bl);
                }
            }

            foreach (var nguoidung in db.Users.ToList())
            {
                foreach (var item in dsBLcuaTT)
                {
                    if (item.CustomerID.Equals(nguoidung.Id))
                    {
                        item.CustomerEmail = nguoidung.Email;
                    }
                }
            }

            ViewBag.dsBLcuaTT = dsBLcuaTT;

            TinTucsViewModel tinTucsViewModel = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TinTuc tinTuc = db.TinTucs.Find(id);
            tinTuc.LuotXem++;
            db.Entry(tinTuc).State = EntityState.Modified;
            db.SaveChanges();

            if (tinTuc == null)
            {
                return HttpNotFound();
            }

            foreach (var item in SanPhams)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;

                    tinTucsViewModel.LuaChon = "1";
                    TinSP_4Lastest();

                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in DichVus)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageDichVu.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;
                    tinTucsViewModel.LuaChon = "2";
                    TinDV_4Lastest();
                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in BatDongSans)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageBatDongSan.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;
                    tinTucsViewModel.LuaChon = "3";
                    TinBDS_4Lastest();
                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in ViecLams)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string NgayHetHan = "";
                    NgayHetHan = ngaygiohethanTT(tinTucsViewModel.EndDayTinTucs.Value);
                    ViewBag.NgayHetHan = NgayHetHan;
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;
                    tinTucsViewModel.LuaChon = "4";
                    TinVL_4Lastest();
                    return View(tinTucsViewModel);
                }
            }

            return View(tinTucsViewModel);
        }

        // GET: Admin/TinTucs/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role");

            return View();
        }





        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TinTucsViewModel tinTucsViewModel, HttpPostedFileBase[] image1, HttpPostedFileBase[] image2, HttpPostedFileBase[] image3, HttpPostedFileBase[] image4, HttpPostedFileBase video, FormCollection formCollection)
        {


            if (tinTucsViewModel.EndDayTinTucs.Value.CompareTo(DateTime.Now) < 0)
            {
                ModelState.AddModelError("", "Ngày hết hạn tin tức phải ở tương lai !");
                return View(tinTucsViewModel);
            }

            // Lấy danh sách sản phẩm 


            List<TinTuc> listTinTuc = db.TinTucs.ToList();

            // Nếu đếm danh sách = 0 thì tin đầu tiên chắc chắn ID phải = 1.
            // Nếu đếm danh sách = 1 thì tin tiếp theo chắc chắn ID phải = 2. (Cái này làm đề phòng db.TinTucs.Last ko dùng dc)
            // Nhận ID ở cuối danh sách để đưa giá trị cho những table khác
            if (listTinTuc.Count == 0) tinTucsViewModel.IdTinTucs = 1;
            else if (listTinTuc.Count < 2) tinTucsViewModel.IdTinTucs = 2;
            else tinTucsViewModel.IdTinTucs = listTinTuc.Last().Id + 1;


            TinTuc tinTuc = new TinTuc()
            {
                Title = tinTucsViewModel.Title,
                CustomerID = User.Identity.GetUserId(),
                Status = "Công khai",
                ContractPhoneNumber = tinTucsViewModel.ContractPhoneNumber,
                Contract = tinTucsViewModel.Contract,
                PublishDay = DateTime.Now,
                EndDay = tinTucsViewModel.EndDayTinTucs.Value,
                LuotXem = 0

            };

            List<ApplicationUser> users = db.Users.ToList();
            foreach (var item in users)
            {
                if (item.Id.Equals(tinTuc.CustomerID)) tinTuc.Customer = item;
            }




            if (tinTuc.PublishDay == null) tinTuc.PublishDay = DateTime.Now;


            db.TinTucs.Add(tinTuc); db.SaveChanges();

            int demSP = 0;
            int demDV = 0;
            int demBDS = 0;
            int demVL = 0;
            if (tinTucsViewModel.LuaChon.Equals("1"))
            {
                foreach (var image in image1)
                {
                    if (demSP == 4) break;
                    if (image?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageSanPham = tinTucsViewModel.ImageSanPham + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);

                    }
                    demSP++;

                }
                // Lưu folder chứa hình sản phẩm


                LoaiSanPham loaiSanPham = new LoaiSanPham();
                foreach (var item in LoaiSanPhams)
                {
                    if (tinTucsViewModel.IdLoaiSanPham.Equals(item.Id)) loaiSanPham = item;
                }

                if (tinTucsViewModel.NameBatDongSan == null || tinTucsViewModel.NameDichVu == null || tinTucsViewModel.NameSanPham == null ||
                     tinTucsViewModel.NameViecLam == null)
                {
                    tinTucsViewModel.NameBatDongSan = "";
                    tinTucsViewModel.NameDichVu = "";
                    tinTucsViewModel.NameSanPham = "";
                    tinTucsViewModel.NameViecLam = "";
                }

                SanPham sanPham = new SanPham()
                {
                    Name = tinTucsViewModel.NameSanPham,
                    LoaiSanPhamId = tinTucsViewModel.IdLoaiSanPham,
                    Location = tinTucsViewModel.LocationSanPham,
                    Description = tinTucsViewModel.DescriptionSanPham,
                    Image = tinTucsViewModel.ImageSanPham,
                    Price = tinTucsViewModel.PriceSanPham,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiSanPham = loaiSanPham
                };

                db.SanPhams.Add(sanPham); db.SaveChanges();
                return RedirectToAction("Index", "Home");
            };

            if (tinTucsViewModel.LuaChon.Equals("2"))
            {
                // Lưu folder chứa hình dịch vụ
                foreach (var image in image2)
                {
                    if (image?.ContentLength > 0)
                    {
                        if (demDV == 4) break;
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageDichVu = tinTucsViewModel.ImageDichVu + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);
                        demDV++;
                    }
                }


                LoaiDichVu loaiDichVu = new LoaiDichVu();
                foreach (var item in LoaiDichVus)
                {
                    if (tinTucsViewModel.IdLoaiDichVu.Equals(item.Id)) loaiDichVu = item;
                }


                DichVu dichVu = new DichVu()
                {
                    Name = tinTucsViewModel.NameDichVu,
                    LoaiDichVuId = tinTucsViewModel.IdLoaiDichVu,
                    Description = tinTucsViewModel.DescriptionDichVu,
                    Image = tinTucsViewModel.ImageDichVu,
                    Price = tinTucsViewModel.PriceDichVu,
                    Location = tinTucsViewModel.LocationDichVu,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiDichVu = loaiDichVu

                };

                db.DichVus.Add(dichVu); db.SaveChanges();
                return RedirectToAction("Index", "Home");
            };


            if (tinTucsViewModel.LuaChon.Equals("3"))
            {

                // Lưu folder chứa video bất động sản
                if (video?.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(video.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer"), fileName);
                    var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer"));
                    tinTucsViewModel.VideoBatDongSan = "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer/" + fileName;
                    video.SaveAs(path);

                }

                // Lưu folder chứa hình bất động sản
                foreach (var image in image3)
                {
                    if (image?.ContentLength > 0)
                    {
                        if (demBDS == 4) break;
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageBatDongSan = tinTucsViewModel.ImageBatDongSan + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);
                        demBDS++;
                    }
                }


                LoaiBatDongSan loaiBatDongSan = new LoaiBatDongSan();
                foreach (var item in LoaiBatDongSans)
                {
                    if (tinTucsViewModel.IdLoaiBatDongSan.Equals(item.Id)) loaiBatDongSan = item;
                }

                BatDongSan batDongSan = new BatDongSan()
                {
                    Name = tinTucsViewModel.NameBatDongSan,
                    LoaiBatDongSanId = tinTucsViewModel.IdLoaiBatDongSan,
                    Description = tinTucsViewModel.DescriptionBatDongSan,
                    Area = tinTucsViewModel.Area,
                    Image = tinTucsViewModel.ImageBatDongSan,
                    Price = tinTucsViewModel.PriceBatDongSan,
                    Location = tinTucsViewModel.LocationBatDongSan,
                    Video = tinTucsViewModel.VideoBatDongSan,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiBatDongSan = loaiBatDongSan



                };
                db.BatDongSans.Add(batDongSan); db.SaveChanges();
                return RedirectToAction("Index", "Home");
            };
            if (tinTucsViewModel.LuaChon.Equals("4"))
            {
                foreach (var image in image4)
                {
                    if (image?.ContentLength > 0)
                    {
                        if (demVL == 4) break;
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageViecLam = tinTucsViewModel.ImageViecLam + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);
                        demVL++;
                    }

                }
                // Lưu folder chứa hình việc làm


                LoaiViecLam loaiViecLam = new LoaiViecLam();
                foreach (var item in LoaiViecLams)
                {
                    if (tinTucsViewModel.IdLoaiViecLam.Equals(item.Id)) loaiViecLam = item;
                }



                ViecLam viecLam = new ViecLam()
                {

                    Name = tinTucsViewModel.NameViecLam,
                    Require = tinTucsViewModel.Require,
                    Location = tinTucsViewModel.LocationViecLam,
                    Description = tinTucsViewModel.DescriptioViecLamn,
                    Image = tinTucsViewModel.ImageViecLam,
                    Salary = tinTucsViewModel.Salary,
                    Benefit = tinTucsViewModel.Benefit,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    LoaiViecLam = loaiViecLam,
                    TinTuc = tinTuc,
                    LoaiViecLamId = tinTucsViewModel.IdLoaiViecLam



                };

                db.ViecLams.Add(viecLam); db.SaveChanges();
                return RedirectToAction("Index", "Home");
            };
            return View(tinTucsViewModel);
        }

        // GET: Admin/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            TinTucsViewModel tinTucsViewModel = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }

            foreach (var item in SanPhams)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None).ToList();


                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;


                    tinTucsViewModel.LuaChon = "1";
                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in DichVus)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageDichVu.Split(chuoiSplit, StringSplitOptions.None).ToList();

                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;

                    tinTucsViewModel.LuaChon = "2";
                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in BatDongSans)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageBatDongSan.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;
                    tinTucsViewModel.LuaChon = "3";
                    return View(tinTucsViewModel);
                }
            }

            foreach (var item in ViecLams)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc, item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count - 1; i++)
                    {
                        images[i] = images[i] + ".jpg";
                    }
                    images = images.Where(p => p.EndsWith(".jpg")).ToList();
                    ViewBag.HinhAnh = images;
                    tinTucsViewModel.LuaChon = "4";
                    return View(tinTucsViewModel);
                }
            }

            return View(tinTucsViewModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTucsViewModel tinTucsViewModel, HttpPostedFileBase[] image1, HttpPostedFileBase[] image2, HttpPostedFileBase[] image3, HttpPostedFileBase[] image4, HttpPostedFileBase video, FormCollection formCollection)
        {

            if (tinTucsViewModel.EndDayTinTucs.Value.CompareTo(DateTime.Now) < 0)
            {
                ModelState.AddModelError("", "Ngày hết hạn tin tức phải ở tương lai !");
                return View(tinTucsViewModel);
            }

            // Lấy danh sách sản phẩm 
            List<SanPham> SanPhams = new List<SanPham>();


            // Lấy danh sách việc làm 

            List<ViecLam> ViecLams = new List<ViecLam>();

            // Lấy danh sách tin tức
            List<TinTuc> TinTucs = new List<TinTuc>();

            // Lấy danh sách dịch vụ
            List<DichVu> DichVus = new List<DichVu>();


            // Lấy danh sách bất động sản 
            List<BatDongSan> BatDongSans = new List<BatDongSan>();

            List<TinTuc> listTinTuc = db.TinTucs.ToList();

            // Nếu đếm danh sách = 0 thì tin đầu tiên chắc chắn ID phải = 1.
            // Nếu đếm danh sách = 1 thì tin tiếp theo chắc chắn ID phải = 2. (Cái này làm đề phòng db.TinTucs.Last ko dùng dc)
            // Nhận ID ở cuối danh sách để đưa giá trị cho những table khác




            TinTuc tinTuc = new TinTuc()
            {
                Title = tinTucsViewModel.Title,
                CustomerID = User.Identity.GetUserId(),
                Status = tinTucsViewModel.Status,
                ContractPhoneNumber = tinTucsViewModel.ContractPhoneNumber,
                Contract = tinTucsViewModel.Contract,

                EndDay = tinTucsViewModel.EndDayTinTucs.Value,
                Id = tinTucsViewModel.IdTinTucs,
                PublishDay = tinTucsViewModel.PublishDayTinTucs.Value,
                LuotXem = tinTucsViewModel.LuotXem

            };

            List<ApplicationUser> users = db.Users.ToList();
            foreach (var item in users)
            {
                if (item.Id.Equals(tinTuc.CustomerID)) tinTuc.Customer = item;
            }




            var existingEntity = db.TinTucs.Find(tinTucsViewModel.IdTinTucs);
            db.Entry(existingEntity).CurrentValues.SetValues(tinTuc);
            db.SaveChanges();


            int demSP = 0;
            int demDV = 0;
            int demBDS = 0;
            int demVL = 0;


            if (tinTucsViewModel.LuaChon.Equals("1"))
            {
                if (image1.Length == 0)
                {
                    foreach (var item in SanPhams)
                    {
                        if (item.Id == tinTucsViewModel.IdSanPham)
                        {
                            tinTucsViewModel.ImageSanPham = item.Image;
                        }
                    }

                }

                // Lưu folder chứa hình sản phẩm
                foreach (var image in image1)
                {
                    if (demSP == 4) break;
                    if (image?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageSanPham = tinTucsViewModel.ImageSanPham + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);

                    }
                    demSP++;
                }



                LoaiSanPham loaiSanPham = new LoaiSanPham();
                foreach (var item in LoaiSanPhams)
                {
                    if (tinTucsViewModel.IdLoaiSanPham.Equals(item.Id)) loaiSanPham = item;
                }


                SanPham sanPham = new SanPham()
                {
                    Name = tinTucsViewModel.NameSanPham,
                    LoaiSanPhamId = tinTucsViewModel.IdLoaiSanPham,
                    Location = tinTucsViewModel.LocationSanPham,
                    Description = tinTucsViewModel.DescriptionSanPham,
                    Image = tinTucsViewModel.ImageSanPham,
                    Price = tinTucsViewModel.PriceSanPham,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiSanPham = loaiSanPham,
                    Id = tinTucsViewModel.IdSanPham


                };

                var existingEntity1 = db.SanPhams.Find(tinTucsViewModel.IdSanPham);
                if (String.IsNullOrEmpty(sanPham.Image))
                {
                    foreach (var item in db.SanPhams.ToList())
                    {
                        if (item.Id == sanPham.Id)
                        {
                            sanPham.Image = item.Image; break;
                        }
                    }
                }
                db.Entry(existingEntity1).CurrentValues.SetValues(sanPham);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = sanPham.Image.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;

                return RedirectToAction("Details", "TinTucs", new { id = tinTucsViewModel.IdTinTucs });
            };

            if (tinTucsViewModel.LuaChon.Equals("2"))
            {
                if (image2.Length == 0)
                {
                    foreach (var item in DichVus)
                    {
                        if (item.Id == tinTucsViewModel.IdDichVu)
                        {
                            tinTucsViewModel.ImageDichVu = item.Image;
                        }
                    }

                }
                // Lưu folder chứa hình dịch vụ
                foreach (var image in image2)
                {
                    if (demDV == 4) break;
                    if (image?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageDichVu = tinTucsViewModel.ImageDichVu + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);

                    }
                    demDV++;
                }

                LoaiDichVu loaiDichVu = new LoaiDichVu();
                foreach (var item in LoaiDichVus)
                {
                    if (tinTucsViewModel.IdLoaiDichVu.Equals(item.Id)) loaiDichVu = item;
                }


                DichVu dichVu = new DichVu()
                {
                    Name = tinTucsViewModel.NameDichVu,
                    LoaiDichVuId = tinTucsViewModel.IdLoaiDichVu,
                    Description = tinTucsViewModel.DescriptionDichVu,
                    Image = tinTucsViewModel.ImageDichVu,
                    Price = tinTucsViewModel.PriceDichVu,
                    Location = tinTucsViewModel.LocationDichVu,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiDichVu = loaiDichVu,
                    Id = tinTucsViewModel.IdDichVu
                };

                var existingEntity2 = db.DichVus.Find(tinTucsViewModel.IdDichVu);

                if (String.IsNullOrEmpty(dichVu.Image))
                {
                    foreach (var item in db.DichVus.ToList())
                    {
                        if (item.Id == dichVu.Id)
                        {
                            dichVu.Image = item.Image; break;
                        }
                    }
                }
                db.Entry(existingEntity2).CurrentValues.SetValues(dichVu);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = dichVu.Image.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return RedirectToAction("Details", "TinTucs", new { id = tinTucsViewModel.IdTinTucs });
            };


            if (tinTucsViewModel.LuaChon.Equals("3"))
            {
                if (image3.Length == 0)
                {
                    foreach (var item in BatDongSans)
                    {
                        if (item.Id == tinTucsViewModel.IdBatDongSan)
                        {
                            tinTucsViewModel.ImageBatDongSan = item.Image;
                        }
                    }

                }

                if (video?.ContentLength == 0)
                {
                    foreach (var item in BatDongSans)
                    {
                        if (item.Id == tinTucsViewModel.IdBatDongSan)
                        {
                            tinTucsViewModel.VideoBatDongSan = item.Video;
                        }
                    }

                }
                //
                // Lưu folder chứa video bất động sản
                if (video?.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(video.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer"), fileName);
                    var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer"));
                    tinTucsViewModel.VideoBatDongSan = "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Trailer/" + fileName;
                    video.SaveAs(path);

                }

                // Lưu folder chứa hình bất động sản
                foreach (var image in image3)
                {
                    if (demBDS == 4) break;
                    if (image?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageBatDongSan = tinTucsViewModel.ImageBatDongSan + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);

                    }
                    demBDS++;
                }

                LoaiBatDongSan loaiBatDongSan = new LoaiBatDongSan();
                foreach (var item in LoaiBatDongSans)
                {
                    if (tinTucsViewModel.IdLoaiBatDongSan.Equals(item.Id)) loaiBatDongSan = item;
                }

                BatDongSan batDongSan = new BatDongSan()
                {
                    Name = tinTucsViewModel.NameBatDongSan,
                    LoaiBatDongSanId = tinTucsViewModel.IdLoaiBatDongSan,
                    Description = tinTucsViewModel.DescriptionBatDongSan,
                    Area = tinTucsViewModel.Area,
                    Image = tinTucsViewModel.ImageBatDongSan,
                    Price = tinTucsViewModel.PriceBatDongSan,
                    Location = tinTucsViewModel.LocationBatDongSan,
                    Video = tinTucsViewModel.VideoBatDongSan,
                    TinTucId = tinTucsViewModel.IdTinTucs,
                    TinTuc = tinTuc,
                    LoaiBatDongSan = loaiBatDongSan,
                    Id = tinTucsViewModel.IdBatDongSan


                };
                var existingEntity3 = db.BatDongSans.Find(tinTucsViewModel.IdBatDongSan);
                if (String.IsNullOrEmpty(batDongSan.Image))
                {
                    foreach (var item in db.BatDongSans.ToList())
                    {
                        if (item.Id == batDongSan.Id)
                        {
                            batDongSan.Image = item.Image; break;
                        }
                    }
                }

                if (String.IsNullOrEmpty(batDongSan.Video))
                {
                    foreach (var item in db.BatDongSans.ToList())
                    {
                        if (item.Id == batDongSan.Id)
                        {
                            batDongSan.Video = item.Video; break;
                        }
                    }
                }
                db.Entry(existingEntity3).CurrentValues.SetValues(batDongSan);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = batDongSan.Image.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return RedirectToAction("Details", "TinTucs", new { id = tinTucsViewModel.IdTinTucs });
            };

            if (tinTucsViewModel.LuaChon.Equals("4"))
            {
                if (image4.Length == 0)
                {
                    foreach (var item in ViecLams)
                    {
                        if (item.Id == tinTucsViewModel.IdViecLam)
                        {
                            tinTucsViewModel.ImageViecLam = item.Image;
                        }
                    }

                }
                // Lưu folder chứa hình việc làm
                foreach (var image in image4)
                {
                    if (demVL == 4) break;
                    if (image?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);

                        var path = Path.Combine(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"), fileName);
                        var dir = Directory.CreateDirectory(Server.MapPath("~/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image"));
                        tinTucsViewModel.ImageViecLam = tinTucsViewModel.ImageViecLam + "/Content/TinTuc/TinTucID" + tinTucsViewModel.IdTinTucs.ToString() + "/Image/" + fileName;
                        image.SaveAs(path);

                    }
                    demVL++;
                }


                LoaiViecLam loaiViecLam = new LoaiViecLam();
                foreach (var item in LoaiViecLams)
                {
                    if (tinTucsViewModel.IdLoaiViecLam.Equals(item.Id)) loaiViecLam = item;
                }



                ViecLam viecLam = new ViecLam()
                {

                    Name = tinTucsViewModel.NameViecLam,
                    Require = tinTucsViewModel.Require,
                    LoaiViecLamId = tinTucsViewModel.IdLoaiViecLam,
                    Location = tinTucsViewModel.LocationViecLam,
                    Description = tinTucsViewModel.DescriptioViecLamn,
                    Image = tinTucsViewModel.ImageViecLam,
                    Salary = tinTucsViewModel.Salary,
                    Benefit = tinTucsViewModel.Benefit,
                    TinTucId = tinTucsViewModel.IdTinTucs,

                    Id = tinTucsViewModel.IdViecLam





                };

                var existingEntity4 = db.ViecLams.Find(tinTucsViewModel.IdViecLam);
                if (String.IsNullOrEmpty(viecLam.Image))
                {
                    foreach (var item in db.ViecLams.ToList())
                    {
                        if (item.Id == viecLam.Id)
                        {
                            viecLam.Image = item.Image; break;
                        }
                    }
                }
                db.Entry(existingEntity4).CurrentValues.SetValues(viecLam);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = viecLam.Image.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return RedirectToAction("Details", "TinTucs", new { id = tinTucsViewModel.IdTinTucs });
            };
            return RedirectToAction("Details", "TinTucs", new { id = tinTucsViewModel.IdTinTucs });
        }



        public ActionResult Edit_HideStatus(int? id)
        {
            var tinTuc = db.TinTucs.Find(id);
            tinTuc.Status = "Ẩn";
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_ofUser", "TinTucs", new { id = User.Identity.GetUserId() });
            }

            return RedirectToAction("Index_ofUser", "TinTucs", new { id = User.Identity.GetUserId() });
        }

        public ActionResult Edit_ShowStatus(int? id)
        {
            var tinTuc = db.TinTucs.Find(id);
            tinTuc.Status = "Công khai";
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_ofUser", "TinTucs", new { id = User.Identity.GetUserId() });
            }

            return RedirectToAction("Index_ofUser", "TinTucs", new { id = User.Identity.GetUserId() });
        }


        public ActionResult Edit_LockStatus(int? id)
        {
            var tinTuc = db.TinTucs.Find(id);
            tinTuc.Status = "Đã khóa";
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "TinTucs");
            }

            return RedirectToAction("Index", "TinTucs");
        }

        public ActionResult Edit_UnlockStatus(int? id)
        {
            var tinTuc = db.TinTucs.Find(id);
            tinTuc.Status = "Công khai";
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "TinTucs");
            }

            return RedirectToAction("Index", "TinTucs");
        }

        public ActionResult Chart()
        {
            List<DataPoint> bieudoLoaiTT = new List<DataPoint>();
            List<DataPoint> bieudoSLLoaiTT = new List<DataPoint>();

            List<DataPoint> bieudoTinSP = new List<DataPoint>();
            List<DataPoint> bieudoTinSP2 = new List<DataPoint>();
            List<DataPoint> bieudoTinSP3 = new List<DataPoint>();

            List<DataPoint> bieudoTinDV = new List<DataPoint>();
            List<DataPoint> bieudoTinDV2 = new List<DataPoint>();
            List<DataPoint> bieudoTinDV3 = new List<DataPoint>();

            List<DataPoint> bieudoTinBDS = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS2 = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS3 = new List<DataPoint>();

            List<DataPoint> bieudoTinVL = new List<DataPoint>();
            List<DataPoint> bieudoTinVL2 = new List<DataPoint>();
            List<DataPoint> bieudoTinVL3 = new List<DataPoint>();

            // Biểu đồ tỉ lệ số tin tức trong các loại tin tức
            float tilephantramSP = (float) db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.TinTuc).ToList().Count / db.TinTucs.ToList().Count;
            float tilephantramDV = (float)db.DichVus.ToList().Count / db.TinTucs.ToList().Count;
            float tilephantramVL = (float)db.ViecLams.ToList().Count / db.TinTucs.ToList().Count;
            float tilephantramBDS = (float)db.BatDongSans.ToList().Count / db.TinTucs.ToList().Count;

            double tileSP = (double)Math.Round(tilephantramSP * 100);
            double tileDV = (double)Math.Round(tilephantramDV * 100);
            double tileBDS = (double)Math.Round(tilephantramBDS * 100);
            double tileVL = (double)Math.Round(100 - tileSP - tileDV - tileBDS);

            bieudoLoaiTT.Add(new DataPoint("Sản phẩm", tileSP));
            bieudoLoaiTT.Add(new DataPoint("Dịch vụ", tileDV));
            bieudoLoaiTT.Add(new DataPoint("Bất động sản", tileBDS));
            bieudoLoaiTT.Add(new DataPoint("Việc Làm", tileVL));


            ViewBag.bieudoLoaiTT = JsonConvert.SerializeObject(bieudoLoaiTT);

            ViewBag.SoLuongTinTuc = db.TinTucs.ToList().Count;
            ViewBag.SoLuongTinSP = db.SanPhams.ToList().Count;
            ViewBag.SoLuongTinDV = db.DichVus.ToList().Count;
            ViewBag.SoLuongTinBDS = db.BatDongSans.ToList().Count;
            ViewBag.SoLuongTinVL = db.ViecLams.ToList().Count;

            bieudoSLLoaiTT.Add(new DataPoint("Sản phẩm", db.SanPhams.ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Dịch vụ", db.DichVus.ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Bất động sản", db.BatDongSans.ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Việc Làm", db.ViecLams.ToList().Count));

            


            // Biểu độ tỉ lệ loại danh mục trong tin tức loại sản phẩm


            foreach (var item in db.LoaiSanPhams.ToList())
            {
                

                
                    int soluong = db.SanPhams.ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Count;
                    bieudoTinSP.Add(new DataPoint(item.Name, soluong));
                    bieudoTinSP2.Add(new DataPoint(item.Name, soluong));
                    bieudoTinSP3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiSanPhams.ToList().Last())
                {

                    double ttmax1 = bieudoTinSP.Max(s => s.Y.Value);
                    bieudoTinSP2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinSP2.Max(y => y.Y.Value);
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }

            }

            double MostTTSP = bieudoTinSP.Max(s => s.Y.Value);
            double FewestTTSP = bieudoTinSP.Min(s => s.Y.Value);
            string MostTTSP_review = "Loại tin tức sản phẩm có số lượng nhiều nhất là: ";
            string FewestTTSP_review = "Loại tin tức sản phẩm có số lượng ít nhất là: ";

            double MostTTSP2 = bieudoTinSP2.Max(s => s.Y.Value);
            string MostTTSP2_review = "Loại tin tức sản phẩm có số lượng nhiều thứ 2 là: ";

            double MostTTSP3 = bieudoTinSP3.Max(s => s.Y.Value);
            string MostTTSP3_review = "Loại tin tức sản phẩm có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiSanPhams.ToList())
            {



                int soluong = db.SanPhams.ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Count;

                if (soluong == MostTTSP)
                {
                    MostTTSP_review = MostTTSP_review + item.Name + ", ";
                }

                if (soluong == MostTTSP2)
                {
                    MostTTSP2_review = MostTTSP2_review + item.Name + ", ";
                }

                if (soluong == MostTTSP3)
                {
                    MostTTSP3_review = MostTTSP3_review + item.Name + ", ";
                }

                if (soluong == FewestTTSP)
                {
                    FewestTTSP_review = FewestTTSP_review + item.Name + ", ";
                }


            }

            MostTTSP_review =  MostTTSP_review.Remove(MostTTSP_review.Length - 2);
            MostTTSP2_review = MostTTSP2_review.Remove(MostTTSP2_review.Length - 2);
            MostTTSP3_review = MostTTSP3_review.Remove(MostTTSP3_review.Length - 2);

            FewestTTSP_review = FewestTTSP_review.Remove(FewestTTSP_review.Length - 2);

            ViewBag.ReviewLSPMost = MostTTSP_review + " với " + MostTTSP + " tin tức." ;
            ViewBag.ReviewLSP2Most = MostTTSP2_review + " với " + MostTTSP2 + " tin tức.";
            ViewBag.ReviewLSP3Most = MostTTSP3_review + " với " + MostTTSP3 + " tin tức.";

            ViewBag.ReviewLSPFewest = FewestTTSP_review + " với " + FewestTTSP + " tin tức.";

            ViewBag.bieudoTTLoaiSP = JsonConvert.SerializeObject(bieudoTinSP);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại dịch vụ
            foreach (var item in db.LoaiDichVus.ToList())
            {

                

                   
                


                
                    int soluong = db.DichVus.ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Count;
                    bieudoTinDV.Add(new DataPoint(item.Name, soluong));
                    bieudoTinDV2.Add(new DataPoint(item.Name, soluong));
                    bieudoTinDV3.Add(new DataPoint(item.Name, soluong));



                    if (item == db.LoaiDichVus.ToList().Last())
                    {

                        double ttmax1 = bieudoTinDV.Max(s => s.Y.Value);
                        bieudoTinDV2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                        double ttmax2 = bieudoTinDV2.Max(y => y.Y.Value);
                        bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                        bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                    }

            }

            double MostTTDV = bieudoTinDV.Max(s => s.Y.Value);
            double FewestTTDV = bieudoTinDV.Min(s => s.Y.Value);
            string MostTTDV_review = "Loại tin tức dịch vụ có số lượng nhiều nhất là: ";
            string FewestTTDV_review = "Loại tin tức dịch vụ có số lượng ít nhất là: ";

            double MostTTDV2 = bieudoTinDV2.Max(s => s.Y.Value);
            string MostTTDV2_review = "Loại tin tức dịch vụ có số lượng nhiều thứ 2 là: ";

            double MostTTDV3 = bieudoTinDV3.Max(s => s.Y.Value);
            string MostTTDV3_review = "Loại tin tức dịch vụ có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiDichVus.ToList())
            {



                int soluong = db.DichVus.ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Count;

                if (soluong == MostTTDV)
                {
                    MostTTDV_review = MostTTDV_review + item.Name + ", ";
                }

                if (soluong == MostTTDV2)
                {
                    MostTTDV2_review = MostTTDV2_review + item.Name + ", ";
                }

                if (soluong == MostTTDV3)
                {
                    MostTTDV3_review = MostTTDV3_review + item.Name + ", ";
                }

                if (soluong == FewestTTDV)
                {
                    FewestTTDV_review = FewestTTDV_review + item.Name + ", ";
                }


            }




            MostTTDV_review = MostTTDV_review.Remove(MostTTDV_review.Length - 2);
            MostTTDV2_review = MostTTDV2_review.Remove(MostTTDV2_review.Length - 2);
            MostTTDV3_review = MostTTDV3_review.Remove(MostTTDV3_review.Length - 2);

            FewestTTDV_review = FewestTTDV_review.Remove(FewestTTDV_review.Length - 2);

            ViewBag.ReviewLDVMost = MostTTDV_review + " với " + MostTTDV + " tin tức.";
            ViewBag.ReviewLDV2Most = MostTTDV2_review + " với " + MostTTDV2 + " tin tức.";
            ViewBag.ReviewLDV3Most = MostTTDV3_review + " với " + MostTTDV3 + " tin tức.";

            ViewBag.ReviewLDVFewest = FewestTTDV_review + " với " + FewestTTDV + " tin tức.";


            ViewBag.bieudoTTLoaiDV = JsonConvert.SerializeObject(bieudoTinDV);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại bất động sản
            foreach (var item in db.LoaiBatDongSans.ToList())
            {


                int soluong = db.BatDongSans.ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Count;
                bieudoTinBDS.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS2.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiBatDongSans.ToList().Last())
                {

                    double ttmax1 = bieudoTinBDS.Max(s => s.Y.Value);
                    bieudoTinBDS2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinBDS2.Max(y => y.Y.Value);
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }
            }

            double MostTTBDS = bieudoTinBDS.Max(s => s.Y.Value);
            double FewestTTBDS = bieudoTinBDS.Min(s => s.Y.Value);
            string MostTTBDS_review = "Loại tin tức bất động sản có số lượng nhiều nhất là: ";
            string FewestTTBDS_review = "Loại tin tức bất động sản có số lượng ít nhất là: ";

            double MostTTBDS2 = bieudoTinBDS2.Max(s => s.Y.Value);
            string MostTTBDS2_review = "Loại tin tức bất động sản có số lượng nhiều thứ 2 là: ";

            double MostTTBDS3 = bieudoTinBDS3.Max(s => s.Y.Value);
            string MostTTBDS3_review = "Loại tin tức bất động sản có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiBatDongSans.ToList())
            {



                int soluong = db.BatDongSans.ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Count;

                if (soluong == MostTTBDS)
                {
                    MostTTBDS_review = MostTTBDS_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS2)
                {
                    MostTTBDS2_review = MostTTBDS2_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS3)
                {
                    MostTTBDS3_review = MostTTBDS3_review + item.Name + ", ";
                }

                if (soluong == FewestTTBDS)
                {
                    FewestTTBDS_review = FewestTTBDS_review + item.Name + ", ";
                }


            }




            MostTTBDS_review = MostTTBDS_review.Remove(MostTTBDS_review.Length - 2);
            MostTTBDS2_review = MostTTBDS2_review.Remove(MostTTBDS2_review.Length - 2);
            MostTTBDS3_review = MostTTBDS3_review.Remove(MostTTBDS3_review.Length - 2);

            FewestTTBDS_review = FewestTTBDS_review.Remove(FewestTTBDS_review.Length - 2);

            ViewBag.ReviewLBDSMost = MostTTBDS_review + " với " + MostTTBDS + " tin tức.";
            ViewBag.ReviewLBDS2Most = MostTTBDS2_review + " với " + MostTTBDS2 + " tin tức.";
            ViewBag.ReviewLBDS3Most = MostTTBDS3_review + " với " + MostTTBDS3 + " tin tức.";

            ViewBag.ReviewLBDSFewest = FewestTTBDS_review + " với " + FewestTTBDS + " tin tức.";

            ViewBag.bieudoTTLoaiBDS = JsonConvert.SerializeObject(bieudoTinBDS);
            // Biểu độ tỉ lệ loại danh mục trong tin tức loại việc làm
            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Count;
                bieudoTinVL.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL2.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiViecLams.ToList().Last())
                {

                    double ttmax1 = bieudoTinVL.Max(s => s.Y.Value);
                    bieudoTinVL2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinVL2.Max(y => y.Y.Value);
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }

            }
            

            double MostTTVL = bieudoTinVL.Max(s => s.Y.Value);
            double FewestTTVL = bieudoTinVL.Min(s => s.Y.Value);
            string MostTTVL_review = "Loại tin tức việc làm có số lượng nhiều nhất là: ";
            string FewestTTVL_review = "Loại tin tức việc làm có số lượng ít nhất là: ";

            double MostTTVL2 = bieudoTinVL2.Max(s => s.Y.Value);
            string MostTTVL2_review = "Loại tin tức việc làm có số lượng nhiều thứ 2 là: ";

            double MostTTVL3 = bieudoTinVL3.Max(s => s.Y.Value);
            string MostTTVL3_review = "Loại tin tức việc làm có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Count;

                if (soluong == MostTTVL)
                {
                    MostTTVL_review = MostTTVL_review + item.Name + ", ";
                }

                if (soluong == MostTTVL2)
                {
                    MostTTVL2_review = MostTTVL2_review + item.Name + ", ";
                }

                if (soluong == MostTTVL3)
                {
                    MostTTVL3_review = MostTTVL3_review + item.Name + ", ";
                }

                if (soluong == FewestTTVL)
                {
                    FewestTTVL_review = FewestTTVL_review + item.Name + ", ";
                }


            }


            MostTTVL_review = MostTTVL_review.Remove(MostTTVL_review.Length - 2);
            MostTTVL2_review = MostTTVL2_review.Remove(MostTTVL2_review.Length - 2);
            MostTTVL3_review = MostTTVL3_review.Remove(MostTTVL3_review.Length - 2);

            FewestTTVL_review = FewestTTVL_review.Remove(FewestTTVL_review.Length - 2);

            ViewBag.ReviewLVLMost = MostTTVL_review + " với " + MostTTVL + " tin tức.";
            ViewBag.ReviewLVL2Most = MostTTVL2_review + " với " + MostTTVL2 + " tin tức.";
            ViewBag.ReviewLVL3Most = MostTTVL3_review + " với " + MostTTVL3 + " tin tức.";

            ViewBag.ReviewLVLFewest = FewestTTVL_review + " với " + FewestTTVL + " tin tức.";

            ViewBag.bieudoTTLoaiVL = JsonConvert.SerializeObject(bieudoTinVL);
            //Tổng quát 

            //Tin tức nhiều tin nhất
            double maxTT = (double)bieudoSLLoaiTT.Max(y => y.Y);
            int demmaxTT = 0;
            List<DataPoint> dsmaxTT = new List<DataPoint>();
            List<DataPoint> dsmaxTile = new List<DataPoint>();
            string tenTTMax = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == maxTT)
                {
                    demmaxTT++;
                    dsmaxTT.Add(bieudoSLLoaiTT[i]);
                    dsmaxTile.Add(bieudoLoaiTT[i]);
                    if (demmaxTT == 1) tenTTMax = tenTTMax + bieudoLoaiTT[i].Label;
                    else tenTTMax = tenTTMax + " và " + bieudoLoaiTT[i].Label;
                }
                
            }

            for (int i = 0; i < dsmaxTT.Count; i++)
            {
                if (demmaxTT <= 1)
                {
                    string danhgiaMaxTT = "Tin tức " + dsmaxTT[i].Label.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% tổng số tin tức với số lượng là : " + dsmaxTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                }
                else
                {
                    string danhgiaMaxTT = "Tin tức " + tenTTMax.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% so với tổng số tin tức với số lượng là : " + dsmaxTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                    break;
                }
            }

            //Tin tức ít tin nhất

            double minTT = (double)bieudoSLLoaiTT.Min(y => y.Y);
            int demminTT = 0;
            List<DataPoint> dsminTT = new List<DataPoint>();
            List<DataPoint> dsminTile = new List<DataPoint>();
            string tenTTMin = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == minTT)
                {
                    demminTT++;
                    dsminTT.Add(bieudoSLLoaiTT[i]);
                    dsminTile.Add(bieudoLoaiTT[i]);
                    if (demminTT == 1) tenTTMin = tenTTMin + bieudoLoaiTT[i].Label;
                    else tenTTMin = tenTTMin + " và " + bieudoLoaiTT[i].Label;
                }

            }

            for (int i = 0; i < dsminTT.Count; i++)
            {
                if (demminTT <= 1)
                {
                    string danhgiaMinTT = "Tin tức " + dsminTT[i].Label.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% tổng số tin tức với số lượng là : " + dsminTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                }
                else
                {
                    string danhgiaMinTT = "Tin tức " + tenTTMin.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% so với tổng số tin tức với số lượng là : " + dsminTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                    break;
                }
            }
            

            // Review tỉ lệ phân bố tin tức
            double khoangcach = (double)bieudoLoaiTT.Max(y => y.Y) - (double)bieudoLoaiTT.Min(y => y.Y);
            if (khoangcach >= 10)
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức không đồng đều do tỉ lệ cách biệt giữa tin " + tenTTMax.ToLower() + " với tin " + tenTTMin.ToLower() + " là: " + khoangcach +"%" ;
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            else
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức đồng đều do tỉ lệ cách biệt giữa các tin tức đều < 10% ";
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            return View();
        }
        public ActionResult ChartMonth(DateTime? dateTime)
        {

            if (!dateTime.HasValue)
            {
                dateTime = DateTime.Now; // set initial page value
            }
            ViewBag.Date = dateTime.Value;
            List<DataPoint> bieudoLoaiTT = new List<DataPoint>();
            List<DataPoint> bieudoSLLoaiTT = new List<DataPoint>();

            List<DataPoint> bieudoTinSP = new List<DataPoint>();
            List<DataPoint> bieudoTinSP2 = new List<DataPoint>();
            List<DataPoint> bieudoTinSP3 = new List<DataPoint>();

            List<DataPoint> bieudoTinDV = new List<DataPoint>();
            List<DataPoint> bieudoTinDV2 = new List<DataPoint>();
            List<DataPoint> bieudoTinDV3 = new List<DataPoint>();

            List<DataPoint> bieudoTinBDS = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS2 = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS3 = new List<DataPoint>();

            List<DataPoint> bieudoTinVL = new List<DataPoint>();
            List<DataPoint> bieudoTinVL2 = new List<DataPoint>();
            List<DataPoint> bieudoTinVL3 = new List<DataPoint>();

            // Biểu đồ tỉ lệ số tin tức trong các loại tin tức
            float tilephantramSP = (float)db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count / db.TinTucs.Where(p => p.PublishDay.Month == dateTime.Value.Month).ToList().Count;
            float tilephantramDV = (float)db.DichVus.Include(s => s.LoaiDichVu).Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count / db.TinTucs.Where(p => p.PublishDay.Month == dateTime.Value.Month).ToList().Count;
            float tilephantramVL = (float)db.ViecLams.Include(s => s.LoaiViecLam).Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count / db.TinTucs.Where(p => p.PublishDay.Month == dateTime.Value.Month).ToList().Count;
            float tilephantramBDS = (float)db.BatDongSans.Include(s => s.LoaiBatDongSan).Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count / db.TinTucs.Where(p => p.PublishDay.Month == dateTime.Value.Month).ToList().Count;

            double tileSP = (double)Math.Round(tilephantramSP * 100);
            double tileDV = (double)Math.Round(tilephantramDV * 100);
            double tileBDS = (double)Math.Round(tilephantramBDS * 100);
            double tileVL = (double)Math.Round(100 - tileSP - tileDV - tileBDS);

            bieudoLoaiTT.Add(new DataPoint("Sản phẩm", tileSP));
            bieudoLoaiTT.Add(new DataPoint("Dịch vụ", tileDV));
            bieudoLoaiTT.Add(new DataPoint("Bất động sản", tileBDS));
            bieudoLoaiTT.Add(new DataPoint("Việc Làm", tileVL));


            ViewBag.bieudoLoaiTT = JsonConvert.SerializeObject(bieudoLoaiTT);

            ViewBag.SoLuongTinTuc = db.TinTucs.Where(p => (p.PublishDay.Month == dateTime.Value.Month)).ToList().Count;
            ViewBag.SoLuongTinSP = db.SanPhams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count;
            ViewBag.SoLuongTinDV = db.DichVus.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count;
            ViewBag.SoLuongTinBDS = db.BatDongSans.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count;
            ViewBag.SoLuongTinVL = db.ViecLams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count;

            bieudoSLLoaiTT.Add(new DataPoint("Sản phẩm", db.SanPhams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Dịch vụ", db.DichVus.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Bất động sản", db.BatDongSans.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count));
            bieudoSLLoaiTT.Add(new DataPoint("Việc Làm", db.ViecLams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Count));




            // Biểu độ tỉ lệ loại danh mục trong tin tức loại sản phẩm


            foreach (var item in db.LoaiSanPhams.ToList())
            {



                int soluong = db.SanPhams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Count;
                bieudoTinSP.Add(new DataPoint(item.Name, soluong));
                bieudoTinSP2.Add(new DataPoint(item.Name, soluong));
                bieudoTinSP3.Add(new DataPoint(item.Name, soluong));

                if (bieudoTinSP.Count == 0) bieudoTinSP.Add(new DataPoint("Không có", 0));

                if (item == db.LoaiSanPhams.ToList().Last())
                {

                    double ttmax1 = bieudoTinSP.Max(s => s.Y.Value);
                    bieudoTinSP2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    if (bieudoTinSP2 .Count == 0) bieudoTinSP2.Add(new DataPoint("Không có", 0));
                    double ttmax2 = bieudoTinSP2.Max(y => y.Y.Value);
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                    if (bieudoTinSP3.Count == 0) bieudoTinSP3.Add(new DataPoint("Không có", 0));
                }

            }

            double MostTTSP = bieudoTinSP.Max(s => s.Y.Value);
            double FewestTTSP = bieudoTinSP.Min(s => s.Y.Value);
            string MostTTSP_review = "Loại tin tức sản phẩm có số lượng nhiều nhất là: ";
            string FewestTTSP_review = "Loại tin tức sản phẩm có số lượng ít nhất là: ";

            double MostTTSP2 = bieudoTinSP2.Max(s => s.Y.Value);
            string MostTTSP2_review = "Loại tin tức sản phẩm có số lượng nhiều thứ 2 là: ";

            double MostTTSP3 = bieudoTinSP3.Max(s => s.Y.Value);
            string MostTTSP3_review = "Loại tin tức sản phẩm có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiSanPhams.ToList())
            {



                int soluong = db.SanPhams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Count;

                if (soluong == MostTTSP)
                {
                    MostTTSP_review = MostTTSP_review + item.Name + ", ";
                }

                if (soluong == MostTTSP2)
                {
                    MostTTSP2_review = MostTTSP2_review + item.Name + ", ";
                }

                if (soluong == MostTTSP3)
                {
                    MostTTSP3_review = MostTTSP3_review + item.Name + ", ";
                }

                if (soluong == FewestTTSP)
                {
                    FewestTTSP_review = FewestTTSP_review + item.Name + ", ";
                }


            }

            MostTTSP_review = MostTTSP_review.Remove(MostTTSP_review.Length - 2);
            MostTTSP2_review = MostTTSP2_review.Remove(MostTTSP2_review.Length - 2);
            MostTTSP3_review = MostTTSP3_review.Remove(MostTTSP3_review.Length - 2);

            FewestTTSP_review = FewestTTSP_review.Remove(FewestTTSP_review.Length - 2);

            ViewBag.ReviewLSPMost = MostTTSP_review + " với " + MostTTSP + " tin tức.";
            ViewBag.ReviewLSP2Most = MostTTSP2_review + " với " + MostTTSP2 + " tin tức.";
            ViewBag.ReviewLSP3Most = MostTTSP3_review + " với " + MostTTSP3 + " tin tức.";

            ViewBag.ReviewLSPFewest = FewestTTSP_review + " với " + FewestTTSP + " tin tức.";

            ViewBag.bieudoTTLoaiSP = JsonConvert.SerializeObject(bieudoTinSP);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại dịch vụ
            foreach (var item in db.LoaiDichVus.ToList())
            {








                int soluong = db.DichVus.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Count;
                bieudoTinDV.Add(new DataPoint(item.Name, soluong));
                bieudoTinDV2.Add(new DataPoint(item.Name, soluong));
                bieudoTinDV3.Add(new DataPoint(item.Name, soluong));

                if (bieudoTinDV.Count == 0) bieudoTinDV.Add(new DataPoint("Không có", 0));

                if (item == db.LoaiDichVus.ToList().Last())
                {

                    double ttmax1 = bieudoTinDV.Max(s => s.Y.Value);
                    bieudoTinDV2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    if (bieudoTinDV2.Count == 0) bieudoTinDV2.Add(new DataPoint("Không có", 0));

                    double ttmax2 = bieudoTinDV2.Max(y => y.Y.Value);
                    bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax2));
                    if (bieudoTinDV3.Count == 0) bieudoTinDV3.Add(new DataPoint("Không có", 0));
                }

            }

            double MostTTDV = bieudoTinDV.Max(s => s.Y.Value);
            double FewestTTDV = bieudoTinDV.Min(s => s.Y.Value);
            string MostTTDV_review = "Loại tin tức dịch vụ có số lượng nhiều nhất là: ";
            string FewestTTDV_review = "Loại tin tức dịch vụ có số lượng ít nhất là: ";

            double MostTTDV2 = bieudoTinDV2.Max(s => s.Y.Value);
            string MostTTDV2_review = "Loại tin tức dịch vụ có số lượng nhiều thứ 2 là: ";

            double MostTTDV3 = bieudoTinDV3.Max(s => s.Y.Value);
            string MostTTDV3_review = "Loại tin tức dịch vụ có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiDichVus.ToList())
            {



                int soluong = db.DichVus.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Count;

                if (soluong == MostTTDV)
                {
                    MostTTDV_review = MostTTDV_review + item.Name + ", ";
                }

                if (soluong == MostTTDV2)
                {
                    MostTTDV2_review = MostTTDV2_review + item.Name + ", ";
                }

                if (soluong == MostTTDV3)
                {
                    MostTTDV3_review = MostTTDV3_review + item.Name + ", ";
                }

                if (soluong == FewestTTDV)
                {
                    FewestTTDV_review = FewestTTDV_review + item.Name + ", ";
                }


            }




            MostTTDV_review = MostTTDV_review.Remove(MostTTDV_review.Length - 2);
            MostTTDV2_review = MostTTDV2_review.Remove(MostTTDV2_review.Length - 2);
            MostTTDV3_review = MostTTDV3_review.Remove(MostTTDV3_review.Length - 2);

            FewestTTDV_review = FewestTTDV_review.Remove(FewestTTDV_review.Length - 2);

            ViewBag.ReviewLDVMost = MostTTDV_review + " với " + MostTTDV + " tin tức.";
            ViewBag.ReviewLDV2Most = MostTTDV2_review + " với " + MostTTDV2 + " tin tức.";
            ViewBag.ReviewLDV3Most = MostTTDV3_review + " với " + MostTTDV3 + " tin tức.";

            ViewBag.ReviewLDVFewest = FewestTTDV_review + " với " + FewestTTDV + " tin tức.";


            ViewBag.bieudoTTLoaiDV = JsonConvert.SerializeObject(bieudoTinDV);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại bất động sản
            foreach (var item in db.LoaiBatDongSans.ToList())
            {


                int soluong = db.BatDongSans.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Count;
                bieudoTinBDS.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS2.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS3.Add(new DataPoint(item.Name, soluong));

                if (bieudoTinBDS.Count == 0) bieudoTinBDS.Add(new DataPoint("Không có", 0));

                if (item == db.LoaiBatDongSans.ToList().Last())
                {

                    double ttmax1 = bieudoTinBDS.Max(s => s.Y.Value);
                    bieudoTinBDS2.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    if (bieudoTinBDS2.Count == 0) bieudoTinBDS2.Add(new DataPoint("Không có", 0));
                    double ttmax2 = bieudoTinBDS2.Max(y => y.Y.Value);
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                    if (bieudoTinBDS3.Count == 0) bieudoTinBDS3.Add(new DataPoint("Không có", 0));
                }
            }

            double MostTTBDS = bieudoTinBDS.Max(s => s.Y.Value);
            double FewestTTBDS = bieudoTinBDS.Min(s => s.Y.Value);
            string MostTTBDS_review = "Loại tin tức bất động sản có số lượng nhiều nhất là: ";
            string FewestTTBDS_review = "Loại tin tức bất động sản có số lượng ít nhất là: ";

            double MostTTBDS2 = bieudoTinBDS2.Max(s => s.Y.Value);
            string MostTTBDS2_review = "Loại tin tức bất động sản có số lượng nhiều thứ 2 là: ";

            double MostTTBDS3 = bieudoTinBDS3.Max(s => s.Y.Value);
            string MostTTBDS3_review = "Loại tin tức bất động sản có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiBatDongSans.ToList())
            {



                int soluong = db.BatDongSans.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Count;

                if (soluong == MostTTBDS)
                {
                    MostTTBDS_review = MostTTBDS_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS2)
                {
                    MostTTBDS2_review = MostTTBDS2_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS3)
                {
                    MostTTBDS3_review = MostTTBDS3_review + item.Name + ", ";
                }

                if (soluong == FewestTTBDS)
                {
                    FewestTTBDS_review = FewestTTBDS_review + item.Name + ", ";
                }


            }




            MostTTBDS_review = MostTTBDS_review.Remove(MostTTBDS_review.Length - 2);
            MostTTBDS2_review = MostTTBDS2_review.Remove(MostTTBDS2_review.Length - 2);
            MostTTBDS3_review = MostTTBDS3_review.Remove(MostTTBDS3_review.Length - 2);

            FewestTTBDS_review = FewestTTBDS_review.Remove(FewestTTBDS_review.Length - 2);

            ViewBag.ReviewLBDSMost = MostTTBDS_review + " với " + MostTTBDS + " tin tức.";
            ViewBag.ReviewLBDS2Most = MostTTBDS2_review + " với " + MostTTBDS2 + " tin tức.";
            ViewBag.ReviewLBDS3Most = MostTTBDS3_review + " với " + MostTTBDS3 + " tin tức.";

            ViewBag.ReviewLBDSFewest = FewestTTBDS_review + " với " + FewestTTBDS + " tin tức.";

            ViewBag.bieudoTTLoaiBDS = JsonConvert.SerializeObject(bieudoTinBDS);
            // Biểu độ tỉ lệ loại danh mục trong tin tức loại việc làm
            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Count;
                bieudoTinVL.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL2.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL3.Add(new DataPoint(item.Name, soluong));

                if (bieudoTinVL.Count == 0) bieudoTinVL.Add(new DataPoint("Không có", 0));

                if (item == db.LoaiViecLams.ToList().Last())
                {

                    double ttmax1 = bieudoTinVL.Max(s => s.Y.Value);
                    bieudoTinVL2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    if (bieudoTinVL2.Count == 0) bieudoTinVL2.Add(new DataPoint("Không có", 0));
                    double ttmax2 = bieudoTinVL2.Max(y => y.Y.Value);
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax2));
                    if (bieudoTinVL3.Count == 0) bieudoTinVL3.Add(new DataPoint("Không có", 0));
                }

            }


            double MostTTVL = bieudoTinVL.Max(s => s.Y.Value);
            double FewestTTVL = bieudoTinVL.Min(s => s.Y.Value);
            string MostTTVL_review = "Loại tin tức việc làm có số lượng nhiều nhất là: ";
            string FewestTTVL_review = "Loại tin tức việc làm có số lượng ít nhất là: ";

            double MostTTVL2 = bieudoTinVL2.Max(s => s.Y.Value);
            string MostTTVL2_review = "Loại tin tức việc làm có số lượng nhiều thứ 2 là: ";

            double MostTTVL3 = bieudoTinVL3.Max(s => s.Y.Value);
            string MostTTVL3_review = "Loại tin tức việc làm có số lượng nhiều thứ 3 là: ";

            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.Include(s => s.TinTuc).Where(p => (p.TinTuc.PublishDay.Month == dateTime.Value.Month)).ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Count;

                if (soluong == MostTTVL)
                {
                    MostTTVL_review = MostTTVL_review + item.Name + ", ";
                }

                if (soluong == MostTTVL2)
                {
                    MostTTVL2_review = MostTTVL2_review + item.Name + ", ";
                }

                if (soluong == MostTTVL3)
                {
                    MostTTVL3_review = MostTTVL3_review + item.Name + ", ";
                }

                if (soluong == FewestTTVL)
                {
                    FewestTTVL_review = FewestTTVL_review + item.Name + ", ";
                }


            }


            MostTTVL_review = MostTTVL_review.Remove(MostTTVL_review.Length - 2);
            MostTTVL2_review = MostTTVL2_review.Remove(MostTTVL2_review.Length - 2);
            MostTTVL3_review = MostTTVL3_review.Remove(MostTTVL3_review.Length - 2);

            FewestTTVL_review = FewestTTVL_review.Remove(FewestTTVL_review.Length - 2);

            ViewBag.ReviewLVLMost = MostTTVL_review + " với " + MostTTVL + " tin tức.";
            ViewBag.ReviewLVL2Most = MostTTVL2_review + " với " + MostTTVL2 + " tin tức.";
            ViewBag.ReviewLVL3Most = MostTTVL3_review + " với " + MostTTVL3 + " tin tức.";

            ViewBag.ReviewLVLFewest = FewestTTVL_review + " với " + FewestTTVL + " tin tức.";

            ViewBag.bieudoTTLoaiVL = JsonConvert.SerializeObject(bieudoTinVL);
            //Tổng quát 

            //Tin tức nhiều tin nhất
            double maxTT = (double)bieudoSLLoaiTT.Max(y => y.Y);
            int demmaxTT = 0;
            List<DataPoint> dsmaxTT = new List<DataPoint>();
            List<DataPoint> dsmaxTile = new List<DataPoint>();
            string tenTTMax = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == maxTT)
                {
                    demmaxTT++;
                    dsmaxTT.Add(bieudoSLLoaiTT[i]);
                    dsmaxTile.Add(bieudoLoaiTT[i]);
                    if (demmaxTT == 1) tenTTMax = tenTTMax + bieudoLoaiTT[i].Label;
                    else tenTTMax = tenTTMax + " và " + bieudoLoaiTT[i].Label;
                }

            }

            for (int i = 0; i < dsmaxTT.Count; i++)
            {
                if (demmaxTT <= 1)
                {
                    string danhgiaMaxTT = "Tin tức " + dsmaxTT[i].Label.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% tổng số tin tức với số lượng là : " + dsmaxTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                }
                else
                {
                    string danhgiaMaxTT = "Tin tức " + tenTTMax.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% so với tổng số tin tức với số lượng là : " + dsmaxTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                    break;
                }
            }

            //Tin tức ít tin nhất

            double minTT = (double)bieudoSLLoaiTT.Min(y => y.Y);
            int demminTT = 0;
            List<DataPoint> dsminTT = new List<DataPoint>();
            List<DataPoint> dsminTile = new List<DataPoint>();
            string tenTTMin = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == minTT)
                {
                    demminTT++;
                    dsminTT.Add(bieudoSLLoaiTT[i]);
                    dsminTile.Add(bieudoLoaiTT[i]);
                    if (demminTT == 1) tenTTMin = tenTTMin + bieudoLoaiTT[i].Label;
                    else tenTTMin = tenTTMin + " và " + bieudoLoaiTT[i].Label;
                }

            }

            for (int i = 0; i < dsminTT.Count; i++)
            {
                if (demminTT <= 1)
                {
                    string danhgiaMinTT = "Tin tức " + dsminTT[i].Label.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% tổng số tin tức với số lượng là : " + dsminTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                }
                else
                {
                    string danhgiaMinTT = "Tin tức " + tenTTMin.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% so với tổng số tin tức với số lượng là : " + dsminTT[i].Y + " tin tức.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                    break;
                }
            }


            // Review tỉ lệ phân bố tin tức
            double khoangcach = (double)bieudoLoaiTT.Max(y => y.Y) - (double)bieudoLoaiTT.Min(y => y.Y);
            if (khoangcach >= 10)
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức không đồng đều do tỉ lệ cách biệt giữa tin " + tenTTMax.ToLower() + " với tin " + tenTTMin.ToLower() + " là: " + khoangcach + "%";
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            else
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức đồng đều do tỉ lệ cách biệt giữa các tin tức đều < 10% ";
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            return View();
        }
        public ActionResult ChartViewer()
        {
            List<DataPoint> bieudoLoaiTT = new List<DataPoint>();
            List<DataPoint> bieudoSLLoaiTT = new List<DataPoint>();

            List<DataPoint> bieudoTinSP = new List<DataPoint>();
            List<DataPoint> bieudoTinSP2 = new List<DataPoint>();
            List<DataPoint> bieudoTinSP3 = new List<DataPoint>();

            List<DataPoint> bieudoTinDV = new List<DataPoint>();
            List<DataPoint> bieudoTinDV2 = new List<DataPoint>();
            List<DataPoint> bieudoTinDV3 = new List<DataPoint>();

            List<DataPoint> bieudoTinBDS = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS2 = new List<DataPoint>();
            List<DataPoint> bieudoTinBDS3 = new List<DataPoint>();

            List<DataPoint> bieudoTinVL = new List<DataPoint>();
            List<DataPoint> bieudoTinVL2 = new List<DataPoint>();
            List<DataPoint> bieudoTinVL3 = new List<DataPoint>();

            // Biểu đồ tỉ lệ số tin tức trong các loại tin tức
            float tilephantramSP = (float)db.SanPhams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem) / db.TinTucs.ToList().Sum(s => s.LuotXem);
            float tilephantramDV = (float)db.DichVus.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem) / db.TinTucs.ToList().Sum(s => s.LuotXem);
            float tilephantramVL = (float)db.ViecLams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem) / db.TinTucs.ToList().Sum(s => s.LuotXem);
            float tilephantramBDS = (float)db.BatDongSans.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem) / db.TinTucs.ToList().Sum(s => s.LuotXem);

            double tileSP = (double)Math.Round(tilephantramSP * 100);
            double tileDV = (double)Math.Round(tilephantramDV * 100);
            double tileBDS = (double)Math.Round(tilephantramBDS * 100);
            double tileVL = (double)Math.Round(100 - tileSP - tileDV - tileBDS);

            bieudoLoaiTT.Add(new DataPoint("Sản phẩm", tileSP));
            bieudoLoaiTT.Add(new DataPoint("Dịch vụ", tileDV));
            bieudoLoaiTT.Add(new DataPoint("Bất động sản", tileBDS));
            bieudoLoaiTT.Add(new DataPoint("Việc Làm", tileVL));


            ViewBag.bieudoLoaiTT = JsonConvert.SerializeObject(bieudoLoaiTT);

            ViewBag.SoLuongTinTuc = db.TinTucs.ToList().Sum(s => s.LuotXem);
            ViewBag.SoLuongTinSP = db.SanPhams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem);
            ViewBag.SoLuongTinDV = db.DichVus.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem);
            ViewBag.SoLuongTinBDS = db.BatDongSans.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem);
            ViewBag.SoLuongTinVL = db.ViecLams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem);

            bieudoSLLoaiTT.Add(new DataPoint("Sản phẩm", db.SanPhams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem)));
            bieudoSLLoaiTT.Add(new DataPoint("Dịch vụ", db.DichVus.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem)));
            bieudoSLLoaiTT.Add(new DataPoint("Bất động sản", db.BatDongSans.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem)));
            bieudoSLLoaiTT.Add(new DataPoint("Việc Làm", db.ViecLams.Include(s => s.TinTuc).ToList().Sum(s => s.TinTuc.LuotXem)));




            // Biểu độ tỉ lệ loại danh mục trong tin tức loại sản phẩm


            foreach (var item in db.LoaiSanPhams.ToList())
            {



                int soluong = db.SanPhams.Include(s => s.TinTuc).ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);
                bieudoTinSP.Add(new DataPoint(item.Name, soluong));
                bieudoTinSP2.Add(new DataPoint(item.Name, soluong));
                bieudoTinSP3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiSanPhams.ToList().Last())
                {

                    double ttmax1 = bieudoTinSP.Max(s => s.Y.Value);
                    bieudoTinSP2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinSP2.Max(y => y.Y.Value);
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinSP3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }

            }

            double MostTTSP = bieudoTinSP.Max(s => s.Y.Value);
            double FewestTTSP = bieudoTinSP.Min(s => s.Y.Value);
            string MostTTSP_review = "Loại tin tức sản phẩm có số lượt xem nhiều nhất là: ";
            string FewestTTSP_review = "Loại tin tức sản phẩm có số lượng ít nhất là: ";

            double MostTTSP2 = bieudoTinSP2.Max(s => s.Y.Value);
            string MostTTSP2_review = "Loại tin tức sản phẩm có số lượt xem nhiều thứ 2 là: ";

            double MostTTSP3 = bieudoTinSP3.Max(s => s.Y.Value);
            string MostTTSP3_review = "Loại tin tức sản phẩm có số lượt xem nhiều thứ 3 là: ";

            foreach (var item in db.LoaiSanPhams.ToList())
            {



                int soluong = db.SanPhams.Include(s => s.TinTuc).ToList().Where(s => s.LoaiSanPhamId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);

                if (soluong == MostTTSP)
                {
                    MostTTSP_review = MostTTSP_review + item.Name + ", ";
                }

                if (soluong == MostTTSP2)
                {
                    MostTTSP2_review = MostTTSP2_review + item.Name + ", ";
                }

                if (soluong == MostTTSP3)
                {
                    MostTTSP3_review = MostTTSP3_review + item.Name + ", ";
                }

                if (soluong == FewestTTSP)
                {
                    FewestTTSP_review = FewestTTSP_review + item.Name + ", ";
                }


            }

            MostTTSP_review = MostTTSP_review.Remove(MostTTSP_review.Length - 2);
            MostTTSP2_review = MostTTSP2_review.Remove(MostTTSP2_review.Length - 2);
            MostTTSP3_review = MostTTSP3_review.Remove(MostTTSP3_review.Length - 2);

            FewestTTSP_review = FewestTTSP_review.Remove(FewestTTSP_review.Length - 2);

            ViewBag.ReviewLSPMost = MostTTSP_review + " với " + MostTTSP + " lượt xem.";
            ViewBag.ReviewLSP2Most = MostTTSP2_review + " với " + MostTTSP2 + " lượt xem.";
            ViewBag.ReviewLSP3Most = MostTTSP3_review + " với " + MostTTSP3 + " lượt xem.";

            ViewBag.ReviewLSPFewest = FewestTTSP_review + " với " + FewestTTSP + " lượt xem.";

            ViewBag.bieudoTTLoaiSP = JsonConvert.SerializeObject(bieudoTinSP);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại dịch vụ
            foreach (var item in db.LoaiDichVus.ToList())
            {








                int soluong = db.DichVus.Include(s => s.TinTuc).ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);
                bieudoTinDV.Add(new DataPoint(item.Name, soluong));
                bieudoTinDV2.Add(new DataPoint(item.Name, soluong));
                bieudoTinDV3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiDichVus.ToList().Last())
                {

                    double ttmax1 = bieudoTinDV.Max(s => s.Y.Value);
                    bieudoTinDV2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinDV2.Max(y => y.Y.Value);
                    bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinDV3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }

            }

            double MostTTDV = bieudoTinDV.Max(s => s.Y.Value);
            double FewestTTDV = bieudoTinDV.Min(s => s.Y.Value);
            string MostTTDV_review = "Loại tin tức dịch vụ có số lượt xem nhiều nhất là: ";
            string FewestTTDV_review = "Loại tin tức dịch vụ có số lượng ít nhất là: ";

            double MostTTDV2 = bieudoTinDV2.Max(s => s.Y.Value);
            string MostTTDV2_review = "Loại tin tức dịch vụ có số lượt xem nhiều thứ 2 là: ";

            double MostTTDV3 = bieudoTinDV3.Max(s => s.Y.Value);
            string MostTTDV3_review = "Loại tin tức dịch vụ có số lượt xem nhiều thứ 3 là: ";

            foreach (var item in db.LoaiDichVus.ToList())
            {



                int soluong = db.DichVus.Include(s => s.TinTuc).ToList().Where(s => s.LoaiDichVuId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);

                if (soluong == MostTTDV)
                {
                    MostTTDV_review = MostTTDV_review + item.Name + ", ";
                }

                if (soluong == MostTTDV2)
                {
                    MostTTDV2_review = MostTTDV2_review + item.Name + ", ";
                }

                if (soluong == MostTTDV3)
                {
                    MostTTDV3_review = MostTTDV3_review + item.Name + ", ";
                }

                if (soluong == FewestTTDV)
                {
                    FewestTTDV_review = FewestTTDV_review + item.Name + ", ";
                }


            }




            MostTTDV_review = MostTTDV_review.Remove(MostTTDV_review.Length - 2);
            MostTTDV2_review = MostTTDV2_review.Remove(MostTTDV2_review.Length - 2);
            MostTTDV3_review = MostTTDV3_review.Remove(MostTTDV3_review.Length - 2);

            FewestTTDV_review = FewestTTDV_review.Remove(FewestTTDV_review.Length - 2);

            ViewBag.ReviewLDVMost = MostTTDV_review + " với " + MostTTDV + " lượt xem.";
            ViewBag.ReviewLDV2Most = MostTTDV2_review + " với " + MostTTDV2 + " lượt xem.";
            ViewBag.ReviewLDV3Most = MostTTDV3_review + " với " + MostTTDV3 + " lượt xem.";

            ViewBag.ReviewLDVFewest = FewestTTDV_review + " với " + FewestTTDV + " lượt xem.";


            ViewBag.bieudoTTLoaiDV = JsonConvert.SerializeObject(bieudoTinDV);

            // Biểu độ tỉ lệ loại danh mục trong tin tức loại bất động sản
            foreach (var item in db.LoaiBatDongSans.ToList())
            {


                int soluong = db.BatDongSans.Include(s => s.TinTuc).ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);
                bieudoTinBDS.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS2.Add(new DataPoint(item.Name, soluong));
                bieudoTinBDS3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiBatDongSans.ToList().Last())
                {

                    double ttmax1 = bieudoTinBDS.Max(s => s.Y.Value);
                    bieudoTinBDS2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinBDS2.Max(y => y.Y.Value);
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinBDS3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }
            }

            double MostTTBDS = bieudoTinBDS.Max(s => s.Y.Value);
            double FewestTTBDS = bieudoTinBDS.Min(s => s.Y.Value);
            string MostTTBDS_review = "Loại tin tức bất động sản có số lượt xem nhiều nhất là: ";
            string FewestTTBDS_review = "Loại tin tức bất động sản có số lượt xem ít nhất là: ";

            double MostTTBDS2 = bieudoTinBDS2.Max(s => s.Y.Value);
            string MostTTBDS2_review = "Loại tin tức bất động sản có số lượt xem nhiều thứ 2 là: ";

            double MostTTBDS3 = bieudoTinBDS3.Max(s => s.Y.Value);
            string MostTTBDS3_review = "Loại tin tức bất động sản có số lượt xem nhiều thứ 3 là: ";

            foreach (var item in db.LoaiBatDongSans.ToList())
            {



                int soluong = db.BatDongSans.Include(s => s.TinTuc).ToList().Where(s => s.LoaiBatDongSanId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);

                if (soluong == MostTTBDS)
                {
                    MostTTBDS_review = MostTTBDS_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS2)
                {
                    MostTTBDS2_review = MostTTBDS2_review + item.Name + ", ";
                }

                if (soluong == MostTTBDS3)
                {
                    MostTTBDS3_review = MostTTBDS3_review + item.Name + ", ";
                }

                if (soluong == FewestTTBDS)
                {
                    FewestTTBDS_review = FewestTTBDS_review + item.Name + ", ";
                }


            }




            MostTTBDS_review = MostTTBDS_review.Remove(MostTTBDS_review.Length - 2);
            MostTTBDS2_review = MostTTBDS2_review.Remove(MostTTBDS2_review.Length - 2);
            MostTTBDS3_review = MostTTBDS3_review.Remove(MostTTBDS3_review.Length - 2);

            FewestTTBDS_review = FewestTTBDS_review.Remove(FewestTTBDS_review.Length - 2);

            ViewBag.ReviewLBDSMost = MostTTBDS_review + " với " + MostTTBDS + " lượt xem.";
            ViewBag.ReviewLBDS2Most = MostTTBDS2_review + " với " + MostTTBDS2 + " lượt xem.";
            ViewBag.ReviewLBDS3Most = MostTTBDS3_review + " với " + MostTTBDS3 + " lượt xem.";

            ViewBag.ReviewLBDSFewest = FewestTTBDS_review + " với " + FewestTTBDS + " lượt xem.";

            ViewBag.bieudoTTLoaiBDS = JsonConvert.SerializeObject(bieudoTinBDS);
            // Biểu độ tỉ lệ loại danh mục trong tin tức loại việc làm
            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.Include(s => s.TinTuc).ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);
                bieudoTinVL.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL2.Add(new DataPoint(item.Name, soluong));
                bieudoTinVL3.Add(new DataPoint(item.Name, soluong));



                if (item == db.LoaiViecLams.ToList().Last())
                {

                    double ttmax1 = bieudoTinVL.Max(s => s.Y.Value);
                    bieudoTinVL2.RemoveAll(p => p.Y.Value.Equals(ttmax1));

                    double ttmax2 = bieudoTinVL2.Max(y => y.Y.Value);
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax1));
                    bieudoTinVL3.RemoveAll(p => p.Y.Value.Equals(ttmax2));

                }

            }


            double MostTTVL = bieudoTinVL.Max(s => s.Y.Value);
            double FewestTTVL = bieudoTinVL.Min(s => s.Y.Value);
            string MostTTVL_review = "Loại tin tức việc làm có số lượt xem nhiều nhất là: ";
            string FewestTTVL_review = "Loại tin tức việc làm có số lượt xem ít nhất là: ";

            double MostTTVL2 = bieudoTinVL2.Max(s => s.Y.Value);
            string MostTTVL2_review = "Loại tin tức việc làm có số lượt xem nhiều thứ 2 là: ";

            double MostTTVL3 = bieudoTinVL3.Max(s => s.Y.Value);
            string MostTTVL3_review = "Loại tin tức việc làm có số lượt xem nhiều thứ 3 là: ";

            foreach (var item in db.LoaiViecLams.ToList())
            {



                int soluong = db.ViecLams.Include(s => s.TinTuc).ToList().Where(s => s.LoaiViecLamId == item.Id).ToList().Sum(p => p.TinTuc.LuotXem);

                if (soluong == MostTTVL)
                {
                    MostTTVL_review = MostTTVL_review + item.Name + ", ";
                }

                if (soluong == MostTTVL2)
                {
                    MostTTVL2_review = MostTTVL2_review + item.Name + ", ";
                }

                if (soluong == MostTTVL3)
                {
                    MostTTVL3_review = MostTTVL3_review + item.Name + ", ";
                }

                if (soluong == FewestTTVL)
                {
                    FewestTTVL_review = FewestTTVL_review + item.Name + ", ";
                }


            }


            MostTTVL_review = MostTTVL_review.Remove(MostTTVL_review.Length - 2);
            MostTTVL2_review = MostTTVL2_review.Remove(MostTTVL2_review.Length - 2);
            MostTTVL3_review = MostTTVL3_review.Remove(MostTTVL3_review.Length - 2);

            FewestTTVL_review = FewestTTVL_review.Remove(FewestTTVL_review.Length - 2);

            ViewBag.ReviewLVLMost = MostTTVL_review + " với " + MostTTVL + " lượt xem.";
            ViewBag.ReviewLVL2Most = MostTTVL2_review + " với " + MostTTVL2 + " lượt xem.";
            ViewBag.ReviewLVL3Most = MostTTVL3_review + " với " + MostTTVL3 + " lượt xem.";

            ViewBag.ReviewLVLFewest = FewestTTVL_review + " với " + FewestTTVL + " lượt xem.";

            ViewBag.bieudoTTLoaiVL = JsonConvert.SerializeObject(bieudoTinVL);
            //Tổng quát 

            //Tin tức nhiều tin nhất
            double maxTT = (double)bieudoSLLoaiTT.Max(y => y.Y);
            int demmaxTT = 0;
            List<DataPoint> dsmaxTT = new List<DataPoint>();
            List<DataPoint> dsmaxTile = new List<DataPoint>();
            string tenTTMax = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == maxTT)
                {
                    demmaxTT++;
                    dsmaxTT.Add(bieudoSLLoaiTT[i]);
                    dsmaxTile.Add(bieudoLoaiTT[i]);
                    if (demmaxTT == 1) tenTTMax = tenTTMax + bieudoLoaiTT[i].Label;
                    else tenTTMax = tenTTMax + " và " + bieudoLoaiTT[i].Label;
                }

            }

            for (int i = 0; i < dsmaxTT.Count; i++)
            {
                if (demmaxTT <= 1)
                {
                    string danhgiaMaxTT = "Tin tức " + dsmaxTT[i].Label.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% tổng số tin tức với số lượt xem là : " + dsmaxTT[i].Y + " lượt xem.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                }
                else
                {
                    string danhgiaMaxTT = "Tin tức " + tenTTMax.ToLower() + " chiếm tỉ lệ cao nhất " + dsmaxTile[i].Y + "% so với tổng số tin tức với số lượt xem là : " + dsmaxTT[i].Y + " lượt xem.";
                    ViewBag.ReviewTTMaxTongQuat = danhgiaMaxTT;
                    break;
                }
            }

            //Tin tức ít tin nhất

            double minTT = (double)bieudoSLLoaiTT.Min(y => y.Y);
            int demminTT = 0;
            List<DataPoint> dsminTT = new List<DataPoint>();
            List<DataPoint> dsminTile = new List<DataPoint>();
            string tenTTMin = "";
            for (int i = 0; i < bieudoSLLoaiTT.Count; i++)
            {
                if (bieudoSLLoaiTT[i].Y.Value == minTT)
                {
                    demminTT++;
                    dsminTT.Add(bieudoSLLoaiTT[i]);
                    dsminTile.Add(bieudoLoaiTT[i]);
                    if (demminTT == 1) tenTTMin = tenTTMin + bieudoLoaiTT[i].Label;
                    else tenTTMin = tenTTMin + " và " + bieudoLoaiTT[i].Label;
                }

            }

            for (int i = 0; i < dsminTT.Count; i++)
            {
                if (demminTT <= 1)
                {
                    string danhgiaMinTT = "Tin tức " + dsminTT[i].Label.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% tổng số tin tức với số lượt xem là : " + dsminTT[i].Y + " lượt xem.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                }
                else
                {
                    string danhgiaMinTT = "Tin tức " + tenTTMin.ToLower() + " chiếm tỉ lệ thấp nhất " + dsminTile[i].Y + "% so với tổng số tin tức với số lượt xem là : " + dsminTT[i].Y + " lượt xem.";
                    ViewBag.ReviewTTMinTongQuat = danhgiaMinTT;
                    break;
                }
            }


            // Review tỉ lệ phân bố tin tức
            double khoangcach = (double)bieudoLoaiTT.Max(y => y.Y) - (double)bieudoLoaiTT.Min(y => y.Y);
            if (khoangcach >= 10)
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức không đồng đều do tỉ lệ cách biệt giữa tin " + tenTTMax.ToLower() + " với tin " + tenTTMin.ToLower() + " là: " + khoangcach + "%";
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            else
            {
                string ReviewTilePB = "Tỉ lệ phân bố giữa các loại tin tức đồng đều do tỉ lệ cách biệt giữa các tin tức đều < 10% ";
                ViewBag.ReviewKhoangCach = ReviewTilePB;
            }
            return View();
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
