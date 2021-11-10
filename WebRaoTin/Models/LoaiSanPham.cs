using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace WebRaoTin.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        public string Name { get; set; }

        private ICollection<SanPham> SanPhams { get; set; }

        
    }
}