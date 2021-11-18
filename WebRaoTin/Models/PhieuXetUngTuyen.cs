using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class PhieuXetUngTuyen
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nội dung")]
        public string Description { get; set; }

        [Display(Name = "Ngày gửi")]

        /*[DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]*/
        public DateTime PublishDay { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Người gửi")]
        public string CustomerID { get; set; }
        public ApplicationUser Customer { get; set; }

        [ForeignKey("ViecLam")]
        [Display(Name = "Mã việc làm")]
        public int ViecLamId { get; set; }
        public ViecLam ViecLam { get; set; }

    }
}