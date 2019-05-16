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
    [RoutePrefix("api/brand")]
    [Authorize]
    public class BrandController : ApiControllerBase
    {
        IBrandService _brandService;
        public BrandController(IErrorService errorService, IBrandService brandService) : base(errorService)
        {
            this._brandService = brandService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "UpdateUser, Admin")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _brandService.FindById(id);

                var responseData = Mapper.Map<Brand, BrandViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "ViewUser, Admin")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _brandService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<BrandViewModel>()
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

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "AddUser, Admin")]
        public HttpResponseMessage Create(HttpRequestMessage request, BrandViewModel brandVm)
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
                    var newBrand = new Brand();
                    newBrand.UpdateBrand(brandVm);
                    newBrand.CreatedDate = DateTime.Now;
                    newBrand.CreatedBy = User.Identity.Name;
                    _brandService.Add(newBrand);
                    _brandService.SaveChanges();

                    var responseData = Mapper.Map<Brand, BrandViewModel>(newBrand);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        [Authorize(Roles = "UpdateUser, Admin")]
        public HttpResponseMessage Update(HttpRequestMessage request, BrandViewModel brandVm)
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
                    var dbBrand = _brandService.FindById(brandVm.ID);

                    dbBrand.UpdateBrand(brandVm);
                    dbBrand.UpdatedDate = DateTime.Now;
                    dbBrand.UpdatedBy = User.Identity.Name;
                    _brandService.Update(dbBrand);
                    _brandService.SaveChanges();

                    var responseData = Mapper.Map<Brand, BrandViewModel>(dbBrand);
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
            _brandService.IsDeleted(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        [Authorize(Roles = "DeleteUser, Admin")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string selectedBrands)
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
                    var listBrands = new JavaScriptSerializer().Deserialize<List<int>>(selectedBrands);
                    foreach (var item in listBrands)
                    {
                        _brandService.Delete(item);
                    }

                    _brandService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listBrands.Count);
                }

                return response;
            });
        }
    }
}
