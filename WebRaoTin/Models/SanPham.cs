using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class SanPham
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:#,##00.##}")]
        public decimal Price { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Nội dung sản phẩm")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Địa điểm")]
        public string Location { get; set; }



        [ForeignKey("TinTuc")]
        [Display(Name = "Mã tin tức")]
        public int TinTucId { get; set; }
        public TinTuc TinTuc { get; set; }

        [ForeignKey("LoaiSanPham")]
        [Display(Name = "Mã Loại sản phẩm")]
        public int LoaiSanPhamId { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
    }
}