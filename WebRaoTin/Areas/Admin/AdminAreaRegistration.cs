using System.Web.Mvc;

namespace WebRaoTin.Areas.Admin
{
    
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            

            context.MapRoute(
                name: "Danh sach tin SP Admin",
                url: "danh-sach-tin-san-pham-admin",
                defaults: new { controller = "SanPhams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin loai SP Admin",
                url: "danh-sach-tin-san-pham-loai-admin",
                defaults: new { controller = "SanPhams", action = "Index_LoaiSP", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );


            context.MapRoute(
                name: "Danh sach tin DV Admin",
                url: "danh-sach-tin-dich-vu-admin",
                defaults: new { controller = "DichVus", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin loai DV Admin",
                url: "danh-sach-tin-dich-vu-loai-admin",
                defaults: new { controller = "DichVus", action = "Index_LoaiDV", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin BDS Admin",
                url: "danh-sach-tin-bat-dong-san-admin",
                defaults: new { controller = "BatDongSans", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin loai BDS Admin",
                url: "danh-sach-tin-bat-dong-san-loai-admin",
                defaults: new { controller = "BatDongSans", action = "Index_LoaiBDS", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin VL Admin",
                url: "danh-sach-tin-viec-lam-admin",
                defaults: new { controller = "ViecLams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin loai VL Admin",
                url: "danh-sach-tin-viec-lam-loai-admin",
                defaults: new { controller = "ViecLams", action = "Index_LoaiVL", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            

            context.MapRoute(
                name: "TinTuc Edit Admin",
                url: "{id}-chinh-sua-tin-tuc-admin",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiSP Edit Admin",
                url: "{id}-chinh-sua-tin-tuc-loai-sp-admin",
                defaults: new
                {
                    controller = "LoaiSanPhams",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiDV Edit Admin",
                url: "{id}-chinh-sua-tin-tuc-loai-dv-admin",
                defaults: new
                {
                    controller = "LoaiDichVus",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiBDS Edit Admin",
                url: "{id}-chinh-sua-tin-tuc-loai-bds-admin",
                defaults: new
                {
                    controller = "LoaiBatDongSans",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiVL Edit Admin",
                url: "{id}-chinh-sua-tin-tuc-loai-vl-admin",
                defaults: new
                {
                    controller = "LoaiViecLams",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Chỉnh sửa trang cá nhân Admin",
                url: "{id}-sua-trang-ca-nhan-admin",
                defaults: new
                {
                    controller = "Users",
                    action = "Edit",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Trang cá nhân Admin",
                url: "{id}-trang-ca-nhan-admin",
                defaults: new
                {
                    controller = "Users",
                    action = "Details",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );


            context.MapRoute(
                name: "Lich su dang tin Admin",
                url: "{id}-lich-su-dang-tin-admin",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Index_ofUser",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Lich su binh luan Admin",
                url: "{id}-lich-su-binh-luan-admin",
                defaults: new
                {
                    controller = "BinhLuans",
                    action = "CMT_ofUser",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc Admin Details",
                url: "{id}-chi-tiet-tin-tuc-admin",
                defaults: new
                {
                    controller = "TinTucs",
                    action = "Details",
                    
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiSP Admin Details",
                url: "{id}-chi-tiet-tin-tuc-loai-sp-admin",
                defaults: new
                {
                    controller = "LoaiSanPhams",
                    action = "Details",

                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiDV Admin Details",
                url: "{id}-chi-tiet-tin-tuc-loai-dv-admin",
                defaults: new
                {
                    controller = "LoaiDichVus",
                    action = "Details",

                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiBDS Admin Details",
                url: "{id}-chi-tiet-tin-tuc-loai-bds-admin",
                defaults: new
                {
                    controller = "LoaiBatDongSans",
                    action = "Details",

                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "TinTuc LoaiVL Admin Details",
                url: "{id}-chi-tiet-tin-tuc-loai-vl-admin",
                defaults: new
                {
                    controller = "LoaiViecLams",
                    action = "Details",

                    id = UrlParameter.Optional
                },
                namespaces: new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach tin tuc Admin",
                url: "danh-sach-tin-tuc-admin",
                defaults: new { controller = "TinTucs", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach loai SP Admin",
                url: "danh-sach-loai-san-pham-admin",
                defaults: new { controller = "LoaiSanPhams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach loai DV Admin",
                url: "danh-sach-loai-dich-vu-admin",
                defaults: new { controller = "LoaiDichVus", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach loai BDS Admin",
                url: "danh-sach-loai-bat-dong-san-admin",
                defaults: new { controller = "LoaiBatDongSans", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach loai VL Admin",
                url: "danh-sach-loai-viec-lam-admin",
                defaults: new { controller = "LoaiViecLams", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Danh sach nguoi dung Admin",
                url: "danh-sach-nguoi-dung-admin",
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Đăng tin tức Admin",
                url: "dang-tin-admin",
                defaults: new { controller = "TinTucs", action = "Create", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Thêm loại SP Admin",
                url: "them-loai-sp-admin",
                defaults: new { controller = "LoaiSanPhams", action = "Create", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Thêm loại DV Admin",
                url: "them-loai-dv-admin",
                defaults: new { controller = "LoaiDichVus", action = "Create", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Thêm loại BDS Admin",
                url: "them-loai-bds-admin",
                defaults: new { controller = "LoaiBatDongSans", action = "Create", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                name: "Thêm loại VL Admin",
                url: "them-loai-vl-admin",
                defaults: new { controller = "LoaiViecLams", action = "Create", id = UrlParameter.Optional },
                new[] { "WebRaoTin.Areas.Admin.Controllers"
                }
            );

            context.MapRoute(
                "Trang chủ Admin",
                "trang-chu-admin",
                new
                {
                    action = "Index",
                    controller = "Home",
                    id = UrlParameter.Optional
                },
                 new[] { "WebRaoTin.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    controller = "Home",
                    id = UrlParameter.Optional
                },
                 new[] { "WebRaoTin.Areas.Admin.Controllers" }
            );


        }
    }
}