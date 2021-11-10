using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class LoaiBatDongSan
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên loại bất động sản")]
        public string Name { get; set; }

        

        private ICollection<BatDongSan> BatDongSans { get; set; }
    }
}