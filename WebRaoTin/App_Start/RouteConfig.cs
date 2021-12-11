using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebRaoTin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GioiThieu",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "GioiThieu", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "PhanHoi",
                url: "gop-y-phan-hoi",
                defaults: new { controller = "Home", action = "PhanHoi", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Xac Nhan Email",
                url: "xac-nhan-email",
                defaults: new { controller = "Account", action = "ConfirmEmail", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Quen mat khau",
                url: "quen-mat-khau",
                defaults: new { controller = "Account", action = "ForgotPassword", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Lam moi mat khau",
                url: "lam-moi-mat-khau",
                defaults: new { controller = "Account", action = "ResetPassword", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Xac nhan lam moi mat khau",
                url: "xac-nhan-lam-moi-mat-khau",
                defaults: new { controller = "Account", action = "ResetPasswordConfirmation", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Doi mat khau",
                url: "doi-mat-khau",
                defaults: new { controller = "Manage", action = "ChangePassword", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Dang Nhap Bang Ung Dung Khac",
                url: "dang-nhap-bang-ung-dung-khac",
                defaults: new { controller = "Account", action = "ExternalLoginCallback", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Dang Nhap",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Dang Ky",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin SP",
                url: "danh-sach-tin-san-pham",
                defaults: new { controller = "SanPhams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin loai SP",
                url: "danh-sach-tin-san-pham-loai",
                defaults: new { controller = "SanPhams", action = "Index_LoaiSP", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );
            

            routes.MapRoute(
                name: "Danh sach tin DV",
                url: "danh-sach-tin-dich-vu",
                defaults: new { controller = "DichVus", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin loai DV",
                url: "danh-sach-tin-dich-vu-loai",
                defaults: new { controller = "DichVus", action = "Index_LoaiDV", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin BDS",
                url: "danh-sach-tin-bat-dong-san",
                defaults: new { controller = "BatDongSans", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin loai BDS",
                url: "danh-sach-tin-bat-dong-san-loai",
                defaults: new { controller = "BatDongSans", action = "Index_LoaiBDS", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin VL",
                url: "danh-sach-tin-viec-lam",
                defaults: new { controller = "ViecLams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach tin loai VL",
                url: "danh-sach-tin-viec-lam-loai",
                defaults: new { controller = "ViecLams", action = "Index_LoaiVL", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );


            routes.MapRoute(
                name: "Chi tiet CV",
                url: "{id}-chi-tiet-cv",
                defaults: new { controller = "PhieuXetUngTuyens", action = "Details", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Danh sach CV",
                url: "{TinTucId}-danh-sach-cv",
                defaults: new { controller = "PhieuXetUngTuyens", action = "Index", TinTucId = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Lich su dang tin",
                url: "{id}-lich-su-dang-tin",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Index_ofUser",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Lich su binh luan",
                url: "{id}-lich-su-binh-luan",
                defaults: new
                {
                    controller = "BinhLuans",
                    action = "CMT_ofUser",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );


            routes.MapRoute(
                name: "TinTuc Details",
                url: "{id}-chi-tiet-tin-tuc",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Details",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "TinTuc Edit",
                url: "{id}-chinh-sua-tin-tuc",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Chỉnh sửa trang cá nhân",
                url: "{id}-sua-trang-ca-nhan",
                defaults: new
                {
                    controller = "Users",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Trang cá nhân",
                url: "{id}-trang-ca-nhan",
                defaults: new
                {
                    controller = "Users",
                    action = "Details",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            

            routes.MapRoute(
                name: "Đăng tin",
                url: "dang-tin-tuc",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Create",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Trang chủ",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Controllers"
                }
            );

            
        }
    }
}
