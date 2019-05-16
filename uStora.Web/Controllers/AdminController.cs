using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    public class AdminController : Controller
    {
        private INotificationService _notificationService;
        public AdminController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNotificationFeedbacks()
        {
            var notificationRegisterTime = Session["FeedbackTime"] != null ? Convert.ToDateTime(Session["FeedbackTime"]) : DateTime.Now;
            var unViewedFeedbackList = _notificationService.GetUnViewedFeedbacks(notificationRegisterTime);
            var feedbackAll = _notificationService.GetAllFeedbacks();

            if (unViewedFeedbackList.Count() > 0)
            {
                var unViewedFeedbackListVm = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(unViewedFeedbackList);
                Session["FeedbackTime"] = DateTime.Now;
                return Json(new
                {
                    data = unViewedFeedbackListVm,
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var feedbackAllVm = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(feedbackAll);
                Session["FeedbackTime"] = DateTime.Now;
                return Json(new
                {
                    data = feedbackAllVm,
                    status = true
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetNotificationUsers()
        {
            var notificationRegisterTime = Session["UserTime"] != null ? Convert.ToDateTime(Session["UserTime"]) : DateTime.Now;
            var userUnViewedList = _notificationService.GetUnViewedUsers(notificationRegisterTime);
            var userAll = _notificationService.GetAllUsers();
            var userListVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(userUnViewedList);
           
            if (userUnViewedList.Count() > 0)
            {
                var userUnViewedListVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(userUnViewedList);
                Session["UserTime"] = DateTime.Now;
                return Json(new
                {
                    data = userUnViewedListVm,
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userAllVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(userAll);
                Session["UserTime"] = DateTime.Now;
                return Json(new
                {
                    data = userAllVm,
                    status = true
                }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}