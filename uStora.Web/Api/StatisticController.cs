using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uStora.Common.ViewModels;
using uStora.Service;
using uStora.Web.Infrastructure.Core;

namespace uStora.Web.Api
{
    [Authorize]
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;

        public StatisticController(IErrorService errorService,
            IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetRevenue(HttpRequestMessage request, string fromDate, string toDate, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                int totalRow = model.Count();
                var revenueList = model.OrderByDescending(x => x.Date).Skip(page * pageSize).Take(pageSize);
                var pagination = new PaginationSet<RevenueStatisticViewModel>()
                {
                    Items = revenueList,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            });
        }
    }
}