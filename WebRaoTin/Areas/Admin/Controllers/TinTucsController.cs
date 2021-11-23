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

namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize]
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
            
            var dsUser = db.Users.ToList();
            ViewBag.dsUser = dsUser;
            
            
        }
        public TinTucsController()
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


        // GET: Admin/TinTucs
        /*
        public ActionResult Index()
        {
            
            var tinTucs = db.TinTucs.Include(t => t.Customer);
            var sanpham = db.SanPhams.Include(t => t.TinTuc).Include(t => t.LoaiSanPham);

            return View(tinTucs.ToList());
        }
        */

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

        public ActionResult Index_ofUser(string searchString, int? page,string id)
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
            foreach(var item in db.PhieuXetUngTuyens.ToList())
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
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            
            foreach(var item in SanPhams)
            {
                if (item.TinTucId.Equals(id))
                {
                    tinTucsViewModel = new TinTucsViewModel(tinTuc,item);
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count-1; i++)
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
                    for (int i = 0; i < images.Count-1; i++)
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
                    for (int i = 0; i < images.Count-1; i++)
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
                    string[] chuoiSplit = new string[] { ".jpg" };
                    List<string> images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None).ToList();
                    for (int i = 0; i < images.Count-1; i++)
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
        public ActionResult Create()
        {
            
            ViewBag.CustomerID = new SelectList(db.Users, "Id", "Role");
            return View();
        }

        

        


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
            if (listTinTuc.Count == 0) tinTucsViewModel.IdTinTucs = 1;
            else if (listTinTuc.Count <2 ) tinTucsViewModel.IdTinTucs = 2;
            else tinTucsViewModel.IdTinTucs = listTinTuc.Last().Id + 1;


            TinTuc tinTuc = new TinTuc() { 
                Title = tinTucsViewModel.Title,
                CustomerID = User.Identity.GetUserId(),
                Status = "Công khai",
                ContractPhoneNumber = tinTucsViewModel.ContractPhoneNumber,
                Contract = tinTucsViewModel.Contract,
                PublishDay = DateTime.Now,
                EndDay = tinTucsViewModel.EndDayTinTucs.Value,
                
                
        };

            List<ApplicationUser> users = db.Users.ToList();
            foreach(var item in users)
            {
                if (item.Id.Equals(tinTuc.CustomerID)) tinTuc.Customer = item;
            }

            


            if (tinTuc.PublishDay == null) tinTuc.PublishDay = DateTime.Now;

           
            db.TinTucs.Add(tinTuc);db.SaveChanges();

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
                        tinTucsViewModel.NameDichVu= "";
                        tinTucsViewModel.NameSanPham= "";
                        tinTucsViewModel.NameViecLam = "";
                }

                SanPham sanPham = new SanPham(){
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

                db.SanPhams.Add(sanPham);db.SaveChanges();
                return View(tinTucsViewModel);
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
                return View(tinTucsViewModel);
            };


            if (tinTucsViewModel.LuaChon.Equals("3")) {

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
                foreach(var item in LoaiBatDongSans)
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
                return View(tinTucsViewModel);
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
                return View(tinTucsViewModel);
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
                    

                    for (int i = 0; i < images.Count-1; i++)
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
                    
                    for (int i = 0; i < images.Count-1; i++)
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
                    for (int i = 0; i < images.Count-1; i++)
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
                    for (int i = 0; i < images.Count-1; i++)
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTucsViewModel tinTucsViewModel, HttpPostedFileBase[] image1, HttpPostedFileBase[] image2, HttpPostedFileBase[] image3, HttpPostedFileBase[] image4, HttpPostedFileBase video, FormCollection formCollection)
        {

           
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
                if (image1.Length == 0 )
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
                db.Entry(existingEntity1).CurrentValues.SetValues(sanPham);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = tinTucsViewModel.ImageSanPham.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;

                return View(tinTucsViewModel);
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
                    Id = tinTucsViewModel.IdLoaiDichVu
                };

                var existingEntity2 = db.DichVus.Find(tinTucsViewModel.IdDichVu);
                db.Entry(existingEntity2).CurrentValues.SetValues(dichVu);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = tinTucsViewModel.ImageDichVu.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return View(tinTucsViewModel);
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
                db.Entry(existingEntity3).CurrentValues.SetValues(batDongSan);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = tinTucsViewModel.ImageBatDongSan.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return View(tinTucsViewModel);
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
                db.Entry(existingEntity4).CurrentValues.SetValues(viecLam);
                db.SaveChanges();

                string[] chuoiSplit = new string[] { ".jpg" };
                List<string> images = tinTucsViewModel.ImageViecLam.Split(chuoiSplit, StringSplitOptions.None).ToList();
                for (int i = 0; i < images.Count - 1; i++)
                {
                    images[i] = images[i] + ".jpg";
                }
                images = images.Where(p => p.EndsWith(".jpg")).ToList();
                ViewBag.HinhAnh = images;
                return View(tinTucsViewModel);
            };
            return View(tinTucsViewModel);
        }



        public ActionResult Edit_HideStatus( int? id)
        {
            var tinTuc = db.TinTucs.Find(id);
            tinTuc.Status = "Ẩn";
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_ofUser", "TinTucs", new { id = User.Identity.GetUserId() });
            }

            return RedirectToAction("Index_ofUser", "TinTucs", new {id = User.Identity.GetUserId() });
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

        // GET: Admin/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
