using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using uStora.Common;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Infrastructure.Core;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public ActionResult Index(string searchString, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var wishlists = _wishlistService.GetWishlistByUserIdPaging(User.Identity.GetUserId(), searchString, page, pageSize, out totalRow);
            var wishlistVm = Mapper.Map<IEnumerable<Wishlist>, IEnumerable<WishlistViewModel>>(wishlists);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<WishlistViewModel>()
            {
                Items = wishlistVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        [HttpPost]
        public JsonResult Add(long productId)
        {
            try
            {
                var wishlist = new Wishlist();
                wishlist.ProductId = productId;
                wishlist.UserId = User.Identity.GetUserId();
                wishlist.CreatedDate = DateTime.Now;
                wishlist.CreatedBy = User.Identity.GetUserName();
                if (!_wishlistService.CheckContains(productId, wishlist.UserId))
                {
                    _wishlistService.Add(wishlist);
                    _wishlistService.SaveChanges();
                    return Json(new
                    {
                        status = 1
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = 2,
                        message = "Bạn đã thích sản phẩm này."
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    status = 0
                });
            }
        }

        public ActionResult Delete(int productId)
        {
            if (ModelState.IsValid)
            {
                _wishlistService.Delete(productId);
                _wishlistService.SaveChanges();
                ViewBag.WishlistDelSuccess = "Xóa thành công.";
                return RedirectToAction("Index", "Wishlist");
            }
            else
            {
                ModelState.AddModelError("wishlist_delerr", "Xóa không thành công");
                return View();
            }

        }
    }
}