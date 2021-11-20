using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;


namespace WebRaoTin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Chức vụ")]

        public string Role { get; set; }

        [Display(Name = "Họ tên")]

        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]

        public string HomeAdress { get; set; }

        [Display(Name = "Giới tính")]

        public string Gender { get; set; }

        [Display(Name = "Ngày sinh")]

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateBorn { get; set; }

        [Display(Name = "Ngày tham gia")]

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateJoin { get; set; }

        [Display(Name = "Trạng thái tài khoản")]

        public string Status { get; set; }

        [Display(Name = "Chứng minh nhân dân")]

        public string CMND { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BatDongSan> BatDongSans { get; set; }

        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<LoaiBatDongSan> LoaiBatDongSans { get; set; }
        public DbSet<LoaiDichVu> LoaiDichVus { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

        public DbSet<LoaiViecLam> LoaiViecLams { get; set; }

        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<ViecLam> ViecLams { get; set; }
        public DbSet<PhieuXetUngTuyen> PhieuXetUngTuyens { get; set; }

        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TinTuc>().HasRequired(c => c.Customer)
                .WithMany()
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinhLuan>().HasRequired(c => c.Customer)
               .WithMany()
               .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhieuXetUngTuyen>().HasRequired(c => c.Customer)
               .WithMany()
               .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        
    }
}