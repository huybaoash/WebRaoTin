using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebRaoTin.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Không được để trống,")]
        [Display(Name = "Tên tài khoản (*)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Họ tên (*)")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Email (*)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Phone(ErrorMessage = "Nhập sai định dạng số điện thoại.")]
        [Display(Name = "Số điện thoại (*)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Giới tính (*)")]

        public string Gender { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "CMND (*)")]

        public string CMND { get; set; }


    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Không được để trống Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng địa chỉ Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Không được để trống.")]
        [EmailAddress(ErrorMessage = "Sai định dạng địa chỉ Email!")]
        [Display(Name = "Email (*)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Tên tài khoản (*)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Phải chọn giới tính")]
        [Display(Name = "Giới tính")]

        public string Gender { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "Địa chỉ")]


        public string HomeAdress { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Phone(ErrorMessage = "Nhập sai định dạng số điện thoại.")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [Display(Name = "CMND (*)")]

        public string CMND { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu (*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu xác nhận (*)")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Không được để trống.")]
        [EmailAddress(ErrorMessage = "Sai định dạng địa chỉ Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được để trống.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu (*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu xác nhận (*)")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Không được để trống.")]
        [EmailAddress(ErrorMessage = "Sai định dạng địa chỉ Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
