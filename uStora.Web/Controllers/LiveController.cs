using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    [Authorize(Roles = "LiveLocation")]
    public class LiveController : Controller
    {
        private ITrackOrderService _trackOrderService;

        public LiveController(ITrackOrderService trackOrderService)
        {
            _trackOrderService = trackOrderService;
        }
       
        public ActionResult Index()
        {
            var trackOrders = _trackOrderService.GetByUserId(User.Identity.GetUserId());
            var trackOrderVm = Mapper.Map<IEnumerable<TrackOrder>, IEnumerable<TrackOrderViewModel>>(trackOrders);
            if (trackOrders.Count() > 0)
                return View(trackOrderVm);
            else
                return Redirect("/no-order.htm");
        }
        public JsonResult UpdateTrackOrder(string lng, string lat)
        {
            var trackOrders = _trackOrderService.GetByUserId(User.Identity.GetUserId());
            if (trackOrders.Count() > 0)
            {
                foreach (var item in trackOrders)
                {
                    item.Latitude = lat;
                    item.Longitude = lng;
                }
                _trackOrderService.SaveChanges();
                return Json(new
                {
                    data = lat + " - " + lng,
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        public ActionResult NoOrder()
        {
            return View();
        }
    }
}