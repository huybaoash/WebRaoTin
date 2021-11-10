using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class DichVu
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên dịch vụ")]
        public string Name { get; set; }

        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Nội dung dịch vụ")]
        public string Description { get; set; }

        [Display(Name = "Địa điểm")]
        public string Location { get; set; }

        [ForeignKey("TinTuc")]
        [Display(Name = "Mã tin tức")]
        public int TinTucId { get; set; }
        public TinTuc TinTuc { get; set; }

        [ForeignKey("LoaiDichVu")]
        [Display(Name = "Mã loại dịch vụ")]
        public int LoaiDichVuId { get; set; }
        public LoaiDichVu LoaiDichVu { get; set; }
    }
}