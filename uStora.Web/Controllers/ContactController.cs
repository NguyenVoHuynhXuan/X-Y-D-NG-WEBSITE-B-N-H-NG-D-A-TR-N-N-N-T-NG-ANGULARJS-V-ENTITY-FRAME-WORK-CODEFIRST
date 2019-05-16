using AutoMapper;
using System.Web.Mvc;
using uStora.Common;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Infrastructure.Extensions;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
        }

        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetContactDetail();
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedback feedback = new Feedback();
            feedback.UpdateFeedback(feedbackViewModel);
            _feedbackService.Add(feedback);
            _feedbackService.SaveChanges();

            var feedBackModel = feedback;

            string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/templates/contactTemplate.html"));
            content = content.Replace("{{Name}}", feedbackViewModel.Name);
            content = content.Replace("{{Email}}", feedbackViewModel.Email);
            content = content.Replace("{{Message}}", feedbackViewModel.Message);
            content = content.Replace("{{Address}}", feedbackViewModel.Address);
            content = content.Replace("{{Phone}}", feedbackViewModel.Phone);
            content = content.Replace("{{Website}}", feedbackViewModel.Website);
            var adminEmail = ConfigHelper.GetByKey("AdminEmail");
            MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ Website.", content);

            feedbackViewModel.ContactDetail = GetContactDetail();
            return Json(new { data = feedBackModel, JsonRequestBehavior.AllowGet });
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var contact = _contactDetailService.GetDefaultContact();
            var contactVm = Mapper.Map<ContactDetail, ContactDetailViewModel>(contact);
            return contactVm;
        }
    }
}