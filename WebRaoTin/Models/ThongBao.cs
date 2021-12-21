using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRaoTin.Models
{
    public class ThongBao
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ngày gửi")]
        public DateTime PublishDay { get; set; }

        [Display(Name = "Nội dung thông báo")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Đường dẫn")]
        [AllowHtml]
        public string Link { get; set; }

        [Display(Name = "Người nhận")]
        public string CustomerID { get; set; }
        public ApplicationUser Customer { get; set; }

        [Display(Name = "Trạng thái")]
        [AllowHtml]
        public string Status { get; set; }
    }
}