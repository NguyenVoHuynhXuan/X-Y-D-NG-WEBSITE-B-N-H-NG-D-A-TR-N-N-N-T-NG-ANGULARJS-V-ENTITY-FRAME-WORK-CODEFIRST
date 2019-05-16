using System.Web.Mvc;
using System.Web.Routing;

namespace uStora.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });


            //admin
            routes.MapRoute(
               name: "admin",
               url: "ustora-admin.htm",
               defaults: new { controller = "Admin", action = "Index", tagId = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //products by tag
            routes.MapRoute(
               name: "Products by tag",
               url: "products/tags-{tagId}.htm",
               defaults: new { controller = "Product", action = "ProductsByTag", tagId = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Product category",
                url: "category/{alias}-{id}.htm",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "uStora.Web.Controllers" }
            );
            //Detail
            routes.MapRoute(
                name: "Product Detail",
                url: "product/{alias}-{id}.htm",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "uStora.Web.Controllers" }
            );
            //search
            routes.MapRoute(
               name: "Search",
               url: "search.htm",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //shop
            routes.MapRoute(
               name: "Shop",
               url: "shop.htm",
               defaults: new { controller = "Product", action = "Shop", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //page
            routes.MapRoute(
               name: "Page",
               url: "trang-{alias}.htm",
               defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //wishlist
            routes.MapRoute(
               name: "wishlist",
               url: "san-pham-yeu-thich.htm",
               defaults: new { controller = "Wishlist", action = "Index", alias = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //shopping cart 
            routes.MapRoute(
               name: "Shopping cart",
               url: "gio-hang.htm",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //No order cart 
            routes.MapRoute(
               name: "No order",
               url: "no-order.htm",
               defaults: new { controller = "Live", action = "NoOrder", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //checkout completed 
            routes.MapRoute(
               name: "Checkout completed",
               url: "xem-trang-thai-mat-hang.htm",
               defaults: new { controller = "ShoppingCart", action = "CheckOutSuccess", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            routes.MapRoute(
               name: "Confirm Order",
               url: "xac-nhan-don-hang.htm",
               defaults: new { controller = "ShoppingCart", action = "ConfirmOrder", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
              );
            routes.MapRoute(
               name: "Cancel Order",
               url: "huy-don-hang.htm",
               defaults: new { controller = "ShoppingCart", action = "CancelOrder", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
              );
            //contact
            routes.MapRoute(
               name: "Contact",
               url: "contact.htm",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //ExternalLoginCallback
            routes.MapRoute(
               name: "ExternalLoginCallback",
               url: "xac-thuc-tai-khoan.htm",
               defaults: new { controller = "Account", action = "ExternalLoginCallback", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            routes.MapRoute(
              name: "Register",
              url: "register.htm",
              defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
              namespaces: new string[] { "uStora.Web.Controllers" }
          );
            routes.MapRoute(
              name: "Manage",
              url: "tai-khoan.htm",
              defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "uStora.Web.Controllers" }
          );
            routes.MapRoute(
              name: "ChangePassword", 
              url: "thay-doi-mat-khau.htm",
              defaults: new { controller = "Manage", action = "ChangePassword", id = UrlParameter.Optional },
              namespaces: new string[] { "uStora.Web.Controllers" }
          );
            routes.MapRoute(
             name: "SetPassword", 
             url: "dat-lai-mat-khau.htm",
             defaults: new { controller = "Manage", action = "SetPassword", id = UrlParameter.Optional },
             namespaces: new string[] { "uStora.Web.Controllers" }
         );
            routes.MapRoute(
            name: "ConfirmEmail",
            url: "kich-hoat-thanh-cong.htm",
            defaults: new { controller = "Account", action = "ConfirmEmail", id = UrlParameter.Optional },
            namespaces: new string[] { "uStora.Web.Controllers" }
        );
            routes.MapRoute(
            name: "ForgotPassword",
            url: "quen-mat-khau.htm",
            defaults: new { controller = "Account", action = "ForgotPassword", id = UrlParameter.Optional },
            namespaces: new string[] { "uStora.Web.Controllers" }
        );
            routes.MapRoute(
            name: "ForgotPasswordConfirmation",
            url: "xac-nhan-mat-khau.htm",
            defaults: new { controller = "Account", action = "ForgotPasswordConfirmation", id = UrlParameter.Optional },
            namespaces: new string[] { "uStora.Web.Controllers" }
        );
            //live
            routes.MapRoute(
               name: "Live",
               url: "vi-tri-truc-tuyen.htm",
               defaults: new { controller = "Live", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //login
            routes.MapRoute(
               name: "Login client",
               url: "login.htm",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );
            //logout
            routes.MapRoute(
               name: "Logout client",
               url: "logout.htm",
               defaults: new { controller = "Account", action = "LogOut", id = UrlParameter.Optional },
               namespaces: new string[] { "uStora.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "uStora.Web.Controllers" }
            );
        }
    }
}