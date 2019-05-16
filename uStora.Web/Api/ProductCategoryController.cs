using AutoMapper;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Infrastructure.Core;
using uStora.Web.Infrastructure.Extensions;
using uStora.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace uStora.Web.API
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize

        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        #endregion Initialize

        #region getall

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
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

        #endregion getall

        #region getallparents

        [Route("getallparents")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        #endregion getallparents

        #region getbyid

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "UpdateUser")]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.FindById(id);

                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        #endregion getbyid

        #region create

        [Route("create")]
        [AllowAnonymous]
        [HttpPost]
        [Authorize(Roles = "AddUser")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    var productCategory = new ProductCategory();

                    productCategory.UpdateProductCategory(productCategoryVm);

                    _productCategoryService.Add(productCategory);
                    _productCategoryService.SaveChanges();

                    var responeData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

                    response = request.CreateResponse(HttpStatusCode.Created, responeData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        #endregion create

        #region update

        [Route("update")]
        [AllowAnonymous]
        [HttpPut]
        [Authorize(Roles = "AddUser")]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    var productCategory = _productCategoryService.FindById(productCategoryVm.ID);

                    productCategory.UpdateProductCategory(productCategoryVm);

                    _productCategoryService.Update(productCategory);
                    _productCategoryService.SaveChanges();

                    var responeData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

                    response = request.CreateResponse(HttpStatusCode.Created, responeData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        #endregion update

        #region delete

        [Route("delete")]
        [AllowAnonymous]
        [HttpDelete]
        [Authorize(Roles = "DeleteUser")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    _productCategoryService.IsDeleted(id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        #endregion delete

        #region deletemulti

        [Route("deletemulti")]
        [AllowAnonymous]
        [HttpDelete]
        [Authorize(Roles = "DeleteUser")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string selectedProductCategories)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    var productCategories = new JavaScriptSerializer().Deserialize<List<int>>(selectedProductCategories);
                    foreach (var item in productCategories)
                    {
                        _productCategoryService.Delete(item);
                    }
                    _productCategoryService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, productCategories.Count());
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        #endregion deletemulti
    }
}