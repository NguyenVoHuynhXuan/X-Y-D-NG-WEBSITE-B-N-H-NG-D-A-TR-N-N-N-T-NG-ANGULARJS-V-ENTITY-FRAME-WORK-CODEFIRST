using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Infrastructure.Core;
using uStora.Web.Infrastructure.Extensions;
using uStora.Web.Models;

namespace uStora.Web.Api
{
    [RoutePrefix("api/feedback")]
    [Authorize]
    public class FeedbackController : ApiControllerBase
    {
        IFeedbackService _feedbackService;
        public FeedbackController(IErrorService errorService,
            IFeedbackService feedbackService) : base(errorService)
        {
            _feedbackService = feedbackService;
        }

        [Route("list")]
        [Authorize(Roles = "AddUser, Admin")]
        public HttpResponseMessage GetListFeedback(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _feedbackService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(query);
                var paginationSet = new PaginationSet<FeedbackViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "AddUser, Admin")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _feedbackService.FindById(id);
                model.Status = false;
                _feedbackService.SaveChanges();
                var responseData = Mapper.Map<Feedback, FeedbackViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }
        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        [Authorize(Roles = "UpdateUser, Admin")]
        public HttpResponseMessage Update(HttpRequestMessage request, FeedbackViewModel feedbackVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbFeedback = _feedbackService.FindById(feedbackVm.ID);

                    dbFeedback.UpdateFeedback(feedbackVm);
                    _feedbackService.Update(dbFeedback);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(dbFeedback);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        [Authorize(Roles = "DeleteUser, Admin")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var feedback = _feedbackService.FindById(id);
                    _feedbackService.Delete(feedback.ID);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(feedback);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        [Authorize(Roles = "DeleteUser, Admin")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string selectedFeedbacks)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listFeedback = new JavaScriptSerializer().Deserialize<List<int>>(selectedFeedbacks);
                    foreach (var item in listFeedback)
                    {
                        _feedbackService.Delete(item);
                    }

                    _feedbackService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listFeedback.Count);
                }

                return response;
            });
        }
    }
}
