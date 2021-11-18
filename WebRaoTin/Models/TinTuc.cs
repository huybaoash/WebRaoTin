using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class TinTuc
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }


        [Display(Name = "Ngày đăng")]

        /*[DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]*/
        public DateTime PublishDay { get; set; }

        [Display(Name = "Ngày hết hạn")]

        /*[DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]*/
        public DateTime EndDay { get; set; }


        [Display(Name = "Thông tin liên lạc")]
        public string Contract { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Số điện thoại liên hệ")]
        public string ContractPhoneNumber { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }


        [Display(Name = "Người đăng")]
        public string CustomerID { get; set; }
        public ApplicationUser Customer { get; set; }


        
        private ICollection<ViecLam> ViecLams { get; set; }
        private ICollection<SanPham> SanPhams { get; set; }
        private ICollection<DichVu> DichVus { get; set; }
        private ICollection<BatDongSan> BatDongSans { get; set; }
        private ICollection<BinhLuan> BinhLuans { get; set; }
    }
}