using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebRaoTin.Models;

namespace WebRaoTin.ViewModel
{
    public class TinTucsViewModel
    {

        public int IdTinTucs { get; set; }
        
        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }


        [Display(Name = "Ngày đăng")]

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat( ApplyFormatInEditMode = true)]
        public DateTime? PublishDayTinTucs { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Ngày hết hạn tin tức")]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? EndDayTinTucs { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Thông tin liên lạc")]
        public string Contract { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]

        [Display(Name = "Số điện thoại liên hệ")]
        public string ContractPhoneNumber { get; set; }
        
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }


        [Display(Name = "Người đăng")]
        public string CustomerID { get; set; }
        [Display(Name = "Tên người đăng")]
        public string CustomerName { get; set; }

        [Display(Name = "Email người đăng")]
        public string CustomerEmail { get; set; }
        public int IdLoaiViecLam { get; set; }
        public int IdViecLam { get; set; }
        [Display(Name = "Tên loại việc làm")]
        public string TenLoaiViecLam { get; set; }

        [Display(Name = "Tên công việc")]
        public string NameViecLam { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Nội dung công việc")]
        public string DescriptioViecLamn { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Yêu cầu")]
        public string Require { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Quyền lợi")]
        public string Benefit { get; set; }


        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Lương")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Địa điểm")]
        public string LocationViecLam { get; set; }

        
        [Display(Name = "Hình ảnh")]
        public string ImageViecLam { get; set; }


        public int IdBatDongSan { get; set; }

        public int IdLoaiBatDongSan { get; set; }

        [Display(Name = "Tên loại bất động sản")]
        public string TenLoaiBatDongSan { get; set; }

        [Display(Name = "Tên bất động sản")]
        public string NameBatDongSan { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal PriceBatDongSan { get; set; }

        
        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Diện tích")]
        public int Area { get; set; }


        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Hình ảnh")]

        public string ImageBatDongSan { get; set; }
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Video")]
        
        public string VideoBatDongSan { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Nội dung ")]
        public string DescriptionBatDongSan { get; set; }
        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Địa điểm ")]
        public string LocationBatDongSan { get; set; }

       
        public int IdDichVu { get; set; }

        public int IdLoaiDichVu { get; set; }

        [Display(Name = "Tên loại dịch vụ")]
        public string TenLoaiDichVu { get; set; }

        [Display(Name = "Tên dịch vụ")]
        public string NameDichVu { get; set; }

       
        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal PriceDichVu { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Hình ảnh")]
        public string ImageDichVu { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Nội dung dịch vụ")]
        public string DescriptionDichVu { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Địa điểm")]
        public string LocationDichVu { get; set; }


        public int IdSanPham { get; set; }
        public int IdLoaiSanPham { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        public string TenLoaiSanPham { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string NameSanPham { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public decimal PriceSanPham { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Hình ảnh")]
        public string ImageSanPham { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Nội dung sản phẩm")]
        public string DescriptionSanPham { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Nơi bán")]
        public string LocationSanPham { get; set; }



        public string LuaChon { get; set; }

        
        public TinTucsViewModel()
        {

        }

        public TinTucsViewModel(int idTinTucs, string title, DateTime publishDayTinTucs, DateTime endDayTinTucs, string contract, string contractPhoneNumber, string status, string customerID, string customerName,string customerEmail, int idLoaiVietLam, int idViecLam, string tenLoaiViecLam, string nameViecLam, string descriptioViecLamn, string require, string benefit, decimal salary, string locationViecLam, string imageViecLam, int idBatDongSan, int idLoaiBatDongSan, string tenLoaiBatDongSan, string nameBatDongSan, decimal priceBatDongSan, int area, string imageBatDongSan, string videoBatDongSan, string descriptionBatDongSan, string locationBatDongSan, int idDichVu, int idLoaiDichVu, string tenLoaiDichVu, string nameDichVu, decimal priceDichVu, string imageDichVu, string descriptionDichVu, string locationDichVu, int idSanPham, int idLoaiSanPham, string tenLoaiSanPham, string nameSanPham, decimal priceSanPham, string imageSanPham, string descriptionSanPham, string locationSanPham, string luaChon)
        {
            IdTinTucs = idTinTucs;
            Title = title;
            PublishDayTinTucs = publishDayTinTucs;
            EndDayTinTucs = endDayTinTucs;
            Contract = contract;
            ContractPhoneNumber = contractPhoneNumber;
            Status = status;
            CustomerID = customerID;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            IdLoaiViecLam = idLoaiVietLam;
            IdViecLam = idViecLam;
            TenLoaiViecLam = tenLoaiViecLam;
            NameViecLam = nameViecLam;
            DescriptioViecLamn = descriptioViecLamn;
            Require = require;
            Benefit = benefit;
            Salary = salary;
            LocationViecLam = locationViecLam;
            ImageViecLam = imageViecLam;
            IdBatDongSan = idBatDongSan;
            IdLoaiBatDongSan = idLoaiBatDongSan;
            TenLoaiBatDongSan = tenLoaiBatDongSan;
            NameBatDongSan = nameBatDongSan;
            PriceBatDongSan = priceBatDongSan;
            Area = area;
            ImageBatDongSan = imageBatDongSan;
            VideoBatDongSan = videoBatDongSan;
            DescriptionBatDongSan = descriptionBatDongSan;
            LocationBatDongSan = locationBatDongSan;
            IdDichVu = idDichVu;
            IdLoaiDichVu = idLoaiDichVu;
            TenLoaiDichVu = tenLoaiDichVu;
            NameDichVu = nameDichVu;
            PriceDichVu = priceDichVu;
            ImageDichVu = imageDichVu;
            DescriptionDichVu = descriptionDichVu;
            LocationDichVu = locationDichVu;
            IdSanPham = idSanPham;
            IdLoaiSanPham = idLoaiSanPham;
            TenLoaiSanPham = tenLoaiSanPham;
            NameSanPham = nameSanPham;
            PriceSanPham = priceSanPham;
            ImageSanPham = imageSanPham;
            DescriptionSanPham = descriptionSanPham;
            LocationSanPham = locationSanPham;
            LuaChon = luaChon;
        }


        public TinTucsViewModel(TinTuc tinTuc,SanPham sp)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            this.Contract = tinTuc.Contract;
            this.ContractPhoneNumber = tinTuc.ContractPhoneNumber;
            this.CustomerID = tinTuc.CustomerID;
            
            foreach(var item in db.Users)
            {
                if (item.Id.Equals(tinTuc.CustomerID)) 
                { 
                    this.CustomerName = item.FullName;
                    this.CustomerEmail = item.Email;
                }
            }
            this.DescriptionSanPham = sp.Description;
            this.EndDayTinTucs = tinTuc.EndDay;
            this.IdLoaiSanPham = sp.LoaiSanPhamId;

            foreach (var item in db.LoaiSanPhams)
            {
                if (item.Id.Equals(sp.LoaiSanPhamId)) this.TenLoaiSanPham = item.Name;
            }

            this.IdSanPham = sp.Id;
            this.IdTinTucs = tinTuc.Id;
            this.ImageSanPham = sp.Image;
            this.LocationSanPham = sp.Location;
            this.NameSanPham = sp.Name;
            this.PriceSanPham = sp.Price;
            this.PublishDayTinTucs = tinTuc.PublishDay;
            this.Status = tinTuc.Status;
            this.Title = tinTuc.Title;
        }

        public TinTucsViewModel(TinTuc tinTuc, BatDongSan bds)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            this.Contract = tinTuc.Contract;
            this.ContractPhoneNumber = tinTuc.ContractPhoneNumber;
            this.CustomerID = tinTuc.CustomerID;

            foreach (var item in db.Users)
            {
                if (item.Id.Equals(tinTuc.CustomerID))
                {
                    this.CustomerName = item.FullName;
                    this.CustomerEmail = item.Email;
                }
            }
            this.DescriptionBatDongSan = bds.Description;
            this.EndDayTinTucs = tinTuc.EndDay;
            this.IdLoaiBatDongSan = bds.LoaiBatDongSanId;

            foreach (var item in db.LoaiBatDongSans)
            {
                if (item.Id.Equals(bds.LoaiBatDongSanId)) this.TenLoaiBatDongSan = item.Name;
            }

            this.IdBatDongSan = bds.Id;
            this.IdTinTucs = tinTuc.Id;
            this.ImageBatDongSan = bds.Image;
            this.LocationBatDongSan = bds.Location;
            this.NameBatDongSan = bds.Name;
            this.PriceBatDongSan = bds.Price;
            this.PublishDayTinTucs = tinTuc.PublishDay;
            this.Status = tinTuc.Status;
            this.Title = tinTuc.Title;
            this.VideoBatDongSan = bds.Video;
            this.Area = bds.Area;
            
        }

        public TinTucsViewModel(TinTuc tinTuc, DichVu dv)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            this.Contract = tinTuc.Contract;
            this.ContractPhoneNumber = tinTuc.ContractPhoneNumber;
            this.CustomerID = tinTuc.CustomerID;

            foreach (var item in db.Users)
            {
                if (item.Id.Equals(tinTuc.CustomerID))
                {
                    this.CustomerName = item.FullName;
                    this.CustomerEmail = item.Email;
                }
            }
            this.DescriptionDichVu = dv.Description;
            this.EndDayTinTucs = tinTuc.EndDay;
            this.IdLoaiDichVu = dv.LoaiDichVuId;

            foreach (var item in db.LoaiDichVus)
            {
                if (item.Id.Equals(dv.LoaiDichVuId)) this.TenLoaiDichVu = item.Name;
            }

            this.IdDichVu = dv.Id;
            this.IdTinTucs = tinTuc.Id;
            this.ImageDichVu = dv.Image;
            this.LocationDichVu = dv.Location;
            this.NameDichVu = dv.Name;
            this.PriceDichVu = dv.Price;
            this.PublishDayTinTucs = tinTuc.PublishDay;
            this.Status = tinTuc.Status;
            this.Title = tinTuc.Title;
            

        }

        public TinTucsViewModel(TinTuc tinTuc, ViecLam vl)
        {



            ApplicationDbContext db = new ApplicationDbContext();

            this.Contract = tinTuc.Contract;
            this.ContractPhoneNumber = tinTuc.ContractPhoneNumber;
            this.CustomerID = tinTuc.CustomerID;

            foreach (var item in db.Users)
            {
                if (item.Id.Equals(tinTuc.CustomerID))
                {
                    this.CustomerName = item.FullName;
                    this.CustomerEmail = item.Email;
                }
            }
            this.DescriptioViecLamn = vl.Description;
            this.EndDayTinTucs = tinTuc.EndDay;
            this.IdLoaiViecLam = vl.LoaiViecLamId;

            foreach (var item in db.LoaiViecLams)
            {
                if (item.Id.Equals(vl.LoaiViecLamId)) this.TenLoaiViecLam = item.Name;
            }

            this.Require = vl.Require;
            this.IdViecLam = vl.Id;
            this.IdTinTucs = tinTuc.Id;
            this.ImageViecLam = vl.Image;
            this.LocationViecLam = vl.Location;
            this.NameViecLam = vl.Name;
            this.Salary = vl.Salary;
            this.Benefit = vl.Benefit;
            this.PublishDayTinTucs = tinTuc.PublishDay;
            this.Status = tinTuc.Status;
            this.Title = tinTuc.Title;
            
            

        }



       




    }


}