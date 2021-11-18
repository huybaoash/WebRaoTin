using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class ViecLam
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên công việc")]
        public string Name { get; set; }

        [Display(Name = "Nội dung công việc")]
        public string Description { get; set; }

        [Display(Name = "Yêu cầu")]
        public string Require { get; set; }

        [Display(Name = "Quyền lợi")]
        public string Benefit { get; set; }

        [Display(Name = "Lương")]
        [DisplayFormat(DataFormatString = "{0:#,##00.##}")]
        public decimal Salary { get; set; }

        [Display(Name = "Địa điểm")]
        public string Location { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [ForeignKey("TinTuc")]
        [Display(Name = "Mã tin tức")]
        public int TinTucId { get; set; }
        public TinTuc TinTuc { get; set; }

        [ForeignKey("LoaiViecLam")]
        [Display(Name = "Mã Loại việc làm")]
        public int LoaiViecLamId { get; set; }
        public LoaiViecLam LoaiViecLam { get; set; }


        private ICollection<ChiTietPhieuXetTuyen> ChiTietPhieuXetTuyens { get; set; }


       
    }
}