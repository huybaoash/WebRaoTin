using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRaoTin.Models
{
    public class ChiTietPhieuXetTuyen
    {
        [Key] public int Id { get; set; }

        [ForeignKey("PhieuXetUngTuyen")]
        [Display(Name = "Mã phiếu xét ứng tuyển")]
        public int PhieuXetUngTuyenId { get; set; }
        public PhieuXetUngTuyen PhieuXetUngTuyen { get; set; }

        [ForeignKey("ViecLam")]
        [Display(Name = "Mã việc làm")]
        public int ViecLamId { get; set; }
        public ViecLam ViecLam { get; set; }


        [Display(Name = "Đôi lời về bản thân")]
        public string AboutYou { get; set; }

        [Display(Name = "Học vấn")]
        public string Education { get; set; }

        [Display(Name = "Kinh nghiệm làm việc")]
        public string Experience { get; set; }

    }
}