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
    [RoutePrefix("api/vehicle")]
    [Authorize]
    public class VehicleController : ApiControllerBase
    {
        private IVehicleService _vehicleService;
        public VehicleController(IErrorService errorService,
            IVehicleService vehicleService) : base(errorService)
        {
            _vehicleService = vehicleService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "UpdateUser")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _vehicleService.FindById(id);

                var responseData = Mapper.Map<Vehicle, VehicleViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _vehicleService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<VehicleViewModel>()
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
        [Authorize(Roles = "AddUser")]
        public HttpResponseMessage Create(HttpRequestMessage request, VehicleViewModel VehicleVm)
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
                    var newVehicle = new Vehicle();
                    newVehicle.UpdateVehicle(VehicleVm);
                    newVehicle.CreatedBy = User.Identity.Name;
                    _vehicleService.Add(newVehicle);
                    _vehicleService.SaveChanges();

                    var responseData = Mapper.Map<Vehicle, VehicleViewModel>(newVehicle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        [Authorize(Roles = "UpdateUser")]
        public HttpResponseMessage Update(HttpRequestMessage request, VehicleViewModel VehicleVm)
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
                    var dbVehicle = _vehicleService.FindById(VehicleVm.ID);

                    dbVehicle.UpdateVehicle(VehicleVm);
                    dbVehicle.UpdatedBy = User.Identity.Name;
                    _vehicleService.Update(dbVehicle);
                    _vehicleService.SaveChanges();

                    var responseData = Mapper.Map<Vehicle, VehicleViewModel>(dbVehicle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        [Authorize(Roles = "DeleteUser")]
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
                    _vehicleService.IsDeleted(id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        [Authorize(Roles = "DeleteUser")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string selectedVehicles)
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
                    var listVehicles = new JavaScriptSerializer().Deserialize<List<int>>(selectedVehicles);
                    foreach (var item in listVehicles)
                    {
                        _vehicleService.Delete(item);
                    }

                    _vehicleService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listVehicles.Count);
                }

                return response;
            });
        }
    }
}
