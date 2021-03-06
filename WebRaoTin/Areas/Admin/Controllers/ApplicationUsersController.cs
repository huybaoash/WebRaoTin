using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebRaoTin.Models;
using Microsoft.AspNet.Identity.Owin;
using WebRaoTin.Controllers;
using System.Configuration;
using System.Data.SqlClient;
using PagedList;

namespace WebRaoTin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class UsersController : Controller
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
        public UsersController()
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

        // GET: Admin/Users
        

        public ActionResult Index(string searchString, int? page)
        {
            int recordsPerPage = 10;

            if (!page.HasValue)
            {
                page = 1; // set initial page value
            }
            ViewBag.Keyword = searchString;

            var users = db.Users.ToList();
            

            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    users = users.Where(s => s.UserName.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }
            catch (Exception ex) { }
            users.OrderByDescending(v => v.Id);

            var finalList = users.OrderByDescending(v => v.Id).ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(string id)
        {
            int demSLTT_dadang = 0;
            foreach (var item in db.TinTucs.ToList())
            {
                if (item.CustomerID.Equals(id)) demSLTT_dadang++;
            }
            ViewBag.SLTT = demSLTT_dadang;


            ApplicationUser user = null;
            foreach (var item in db.Users.ToList())
            {
                if (item.Id.Equals(User.Identity.GetUserId()))
                {
                    user = item;
                }
            }
            ViewBag.currentUser = user;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role,FullName,HomeAdress,Gender,DateBorn,Status,CMND,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult LockUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (User.Identity.GetUserId().Equals(id))
                {
                    return RedirectToAction("Index", "Users");
                }

                ApplicationUser applicationUser = db.Users.Find(id);
                applicationUser.Status = "Đã khóa";
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                var existingEntity = db.Users.Find(applicationUser.Id);

                db.Entry(existingEntity).CurrentValues.SetValues(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult UnlockUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = db.Users.Find(id);
                applicationUser.Status = "Hoạt động";
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                var existingEntity = db.Users.Find(applicationUser.Id);

                db.Entry(existingEntity).CurrentValues.SetValues(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role,FullName,HomeAdress,Gender,DateBorn,Status,CMND,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,DateJoin")] ApplicationUser applicationUser)
        {
            //ApplicationUser applicationUser = db.Users.Find(id);
            
            

            if (ModelState.IsValid)
            {
                var existingEntity = db.Users.Find(applicationUser.Id);
                
                db.Entry(existingEntity).CurrentValues.SetValues(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Details", "Users",new { Id = applicationUser.Id });
            }
            return View(applicationUser);
        }
        
        public ActionResult Edit_UpRole(string id)
        {
            var user = db.Users.Find(id);
            user.Role = "Quản trị viên";

            if (ModelState.IsValid)
            {

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            string ketnoi = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var sqlconnectstring = @"" + ketnoi;

            var connection = new SqlConnection(sqlconnectstring);
            connection.Open();


            // Tạo đối tượng SqlCommand
            var command = new SqlCommand();
            command.Connection = connection;

            // Câu truy vấn gồm: chèn dữ liệu vào và lấy định danh(Primary key) mới chèn vào
            string queryString = @"UPDATE [WebRaoTin].[dbo].[AspNetUserRoles] SET RoleId = 1 WHERE UserId = @UserId AND RoleId = 2 ";

            command.CommandText = queryString;
            command.Parameters.AddWithValue("@UserId", id);


            var rows_affected = command.ExecuteNonQuery();


            connection.Close();
            return RedirectToAction("Index", "Users");
            /*user.Roles.FirstOrDefault((p) =>
            {
                return p.UserId.Equals(id) && p.RoleId == "1";
            }).RoleId = "2";
            if (ModelState.IsValid)
            {

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Users");

            }


            return RedirectToAction("Index", "Users");*/
        }

        
        public ActionResult Edit_DownRole(string id)
        {
            var user = db.Users.Find(id);
            user.Role = "Người dùng";
            if (ModelState.IsValid)
            {

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            string ketnoi = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var sqlconnectstring = @"" + ketnoi;
            
            var connection = new SqlConnection(sqlconnectstring);
            connection.Open();


            // Tạo đối tượng SqlCommand
            var command = new SqlCommand();
            command.Connection = connection;

            // Câu truy vấn gồm: chèn dữ liệu vào và lấy định danh(Primary key) mới chèn vào
            string queryString = @"UPDATE [WebRaoTin].[dbo].[AspNetUserRoles] SET RoleId = 2 WHERE UserId = @UserId AND RoleId = 1 ";

            command.CommandText = queryString;
            command.Parameters.AddWithValue("@UserId", id);
            

            var rows_affected = command.ExecuteNonQuery();
            

            connection.Close();
            return RedirectToAction("Index", "Users");
            // This whole block is in a transaction scope so I just check recordability.

            /*user.Roles.FirstOrDefault((p) =>
            {
                return p.UserId.Equals(id) && p.RoleId == "2";
            }).RoleId = "1";
            if (ModelState.IsValid)
            {

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Users");

            }


            */
        }

        // GET: Admin/Users/Delete/5
       
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
