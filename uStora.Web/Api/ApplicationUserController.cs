using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using uStora.Common.Exceptions;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.App_Start;
using uStora.Web.Infrastructure.Core;
using uStora.Web.Infrastructure.Extensions;
using uStora.Web.Models;

namespace uStora.Web.Api
{
    [Authorize]
    [RoutePrefix("api/applicationUser")]
    public class ApplicationUserController : ApiControllerBase
    {
        private ICommonService _commonService;
        private IApplicationUserService _userService;
        private IApplicationUserService _appUser;
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;

        public ApplicationUserController(
            IApplicationGroupService appGroupService,
            IApplicationUserService appUser,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService,
            ICommonService commonService,
            IApplicationUserService userService)
            : base(errorService)
        {
            _userService = userService;
            _appRoleService = appRoleService;
            _appUser = appUser;
            _appGroupService = appGroupService;
            _userManager = userManager;
            _commonService = commonService;
        }

        #region Methods
        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "ViewUser, Admin")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _commonService.GetUsers(filter);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                IEnumerable<ApplicationUserViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(query);

                PaginationSet<ApplicationUserViewModel> pagedSet = new PaginationSet<ApplicationUserViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [Route("getbyname")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetUserByUsername(HttpRequestMessage request, string username)
        {
            var user = _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không tìm thấy theo yêu cầu.");
            }
            else
            {
                var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user.Result);
                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
            }
        }


        [Route("detail/{id}")]
        [HttpGet]
        [Authorize(Roles = "UpdateUser, Admin")]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            var user = _userManager.FindByIdAsync(id);
            _appUser.SetViewed(id);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }
            else
            {
                var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user.Result);
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                applicationUserViewModel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "AddUser, Admin")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                newAppUser.UpdateUser(applicationUserViewModel);
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        _appGroupService.SaveChanges();

                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "UpdateUser, Admin")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(appUserViewModel.Id);
                try
                {
                    appUser.UpdateUser(appUserViewModel);
                    if (appUserViewModel.EmailConfirmed.Value)
                        appUser.EmailConfirmed = true;
                    else
                        appUser.EmailConfirmed = false;
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in appUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = appUserViewModel.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(appUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(appUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, appUserViewModel.Id);
                        _appGroupService.SaveChanges();
                        return request.CreateResponse(HttpStatusCode.OK, appUserViewModel);
                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException)
                {
                    return request.CreateErrorResponse(HttpStatusCode.Conflict, "Tên không được trùng");
                }
                catch (Exception dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.InternalServerError, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpGet]
        [Route("listdriver")]
        public HttpResponseMessage GetUsersByGroupId(HttpRequestMessage request, int groupId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ApplicationUserViewModel> listUsers = new List<ApplicationUserViewModel>(); ;
                var listUserId = _appUser.GetUserIdByGroupId(groupId);
                foreach (var userId in listUserId)
                {
                    listUsers.Add(Mapper.Map<ApplicationUser, ApplicationUserViewModel>(_appUser.GetUserById(userId)));
                }
                response = request.CreateResponse(HttpStatusCode.OK, listUsers);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeleteUser, Admin")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            _userService.IsDeleted(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
        #endregion
    }
}