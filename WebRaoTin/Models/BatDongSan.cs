using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class BatDongSan
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên bất động sản")]
        public string Name { get; set; }

        [Display(Name = "Diện tích")]
        public int Area { get; set; }



        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Video")]
        public string Video { get; set; }

        [Display(Name = "Nội dung ")]
        public string Description { get; set; }

        [Display(Name = "Địa điểm ")]
        public string Location { get; set; }



        [ForeignKey("TinTuc")]
        [Display(Name = "Mã tin tức")]
        public int TinTucId { get; set; }
        public TinTuc TinTuc { get; set; }

        [ForeignKey("LoaiBatDongSan")]
        [Display(Name = "Mã loại bất động sản")]
        public int LoaiBatDongSanId { get; set; }
        public LoaiBatDongSan LoaiBatDongSan { get; set; }

    }
}