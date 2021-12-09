using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRaoTin.Models
{
    public class BinhLuan
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ngày đăng")]
        public DateTime PublishDay { get; set; }

        [Display(Name = "Nội dung bình luận")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Người đăng")]
        public string CustomerID { get; set; }
        public ApplicationUser Customer { get; set; }

        [ForeignKey("TinTuc")]
        [Display(Name = "Mã tin tức")]
        public int TinTucId { get; set; }
        public TinTuc TinTuc { get; set; }
    }
}