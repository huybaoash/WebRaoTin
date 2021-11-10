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
    public class BinhLuanViewModel
    {
        [Display(Name = "Mã bình luận")]
        public int Id { get; set; }
        [Display(Name = "Mã người dùng")]
        public string CustomerID { get; set; }

        [Display(Name = "Người dùng")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime? PublishDay { get; set; }

        [Display(Name = "Mã tin Tức")]
        public int TinTucId { get; set; }

        [Display(Name = "Nội dung bình luận")]
        public string Description { get; set; }
    }
}