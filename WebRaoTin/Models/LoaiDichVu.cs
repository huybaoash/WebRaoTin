using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class LoaiDichVu
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên dịch vụ")]
        public string Name { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        private ICollection<LoaiDichVu> LoaiDichVus { get; set; }

        
    }
}