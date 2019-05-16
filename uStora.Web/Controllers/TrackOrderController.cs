using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using uStora.Service;

namespace uStora.Web.Controllers
{
    [Authorize]
    public class TrackOrderController : Controller
    {
        private ITrackOrderService _trackOrderService;

        public TrackOrderController(ITrackOrderService trackOrderService)
        {
            _trackOrderService = trackOrderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLocation()
        {
            //Hue: lng = 107.563874 lat = 16.450173
            var locData = _trackOrderService.GetLocation(User.Identity.GetUserId());
            if (locData.Count() > 0)
            {
                return Json(new
                {
                    data = locData,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}