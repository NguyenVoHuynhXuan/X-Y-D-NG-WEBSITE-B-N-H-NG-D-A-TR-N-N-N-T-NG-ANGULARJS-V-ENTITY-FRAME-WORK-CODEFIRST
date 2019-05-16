using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using uStora.Web.Mappings;
using uStora.Web.Models;

namespace uStora.Web
{
    public class MvcApplication : HttpApplication
    {
        private string con = ConfigurationManager.ConnectionStrings["uStoraConnection"].ConnectionString;
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            // Change current culture
            CultureInfo culture;
            if (Thread.CurrentThread.CurrentCulture.Name == "vi-VN")
                culture = CultureInfo.CreateSpecificCulture("vi-VN");
            else
                culture = CultureInfo.CreateSpecificCulture("en-US");

            CultureInfo.DefaultThreadCurrentCulture = culture;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
            SqlDependency.Start(con);

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent notiComponent = new NotificationComponent();
            var currentTime = DateTime.Now;
            HttpContext.Current.Session["FeedbackTime"] = currentTime;
            HttpContext.Current.Session["UserTime"] = currentTime;
            notiComponent.RegisterNotification(currentTime);
        }
        protected void Application_End()
        {
            SqlDependency.Stop(con);
        }
    }
}