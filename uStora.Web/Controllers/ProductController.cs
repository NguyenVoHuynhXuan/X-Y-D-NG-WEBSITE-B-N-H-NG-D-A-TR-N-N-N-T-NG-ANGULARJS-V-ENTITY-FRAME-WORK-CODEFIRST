using AutoMapper;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using uStora.Common;
using uStora.Model.Models;
using uStora.Service;
using uStora.Service.ExportImport;
using uStora.Web.Infrastructure.Core;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IBrandService _brandService;
        private IProductCategoryService _productCategoryService;
        private IExportManagerService _exportManager;
        private IImportManagerService _importManager;

        public ProductController(IProductService productService,
            IProductCategoryService productCategoryService,
            IBrandService brandService, IExportManagerService exportManager,
            IImportManagerService importManager)
        {
            _importManager = importManager;
            _exportManager = exportManager;
            _productService = productService;
            _brandService = brandService;
            _productCategoryService = productCategoryService;
        }
        #region Methods
        public void IncreaseView(List<ViewCounterViewModel> viewCounterViewModel, long productId)
        {
            if (viewCounterViewModel != null)
            {
                if (!viewCounterViewModel.Any(x => x.ProductId == productId))
                {
                    _productService.IncreaseView(productId);
                    AddViewCounter(productId);
                }
            }
            else
            {
                viewCounterViewModel = new List<ViewCounterViewModel>();
                _productService.IncreaseView(productId);
                AddViewCounter(productId);
            }
        }

        public void AddViewCounter(long productId)
        {
            var viewCounterViewModel = (List<ViewCounterViewModel>)Session[CommonConstants.ViewCounterSession];
            if (viewCounterViewModel == null)
                viewCounterViewModel = new List<ViewCounterViewModel>();
            ViewCounterViewModel viewCounterVm = new ViewCounterViewModel();
            viewCounterVm.ProductId = productId;
            viewCounterViewModel.Add(viewCounterVm);
            Session[CommonConstants.ViewCounterSession] = viewCounterViewModel;
        }

        public ActionResult Detail(long id, string searchString, int? page)
        {
            var viewCounter = (List<ViewCounterViewModel>)Session[CommonConstants.ViewCounterSession];
            int defaultPageSize = int.Parse(ConfigHelper.GetByKey("pageSizeAjax"));
            var product = _productService.FindById(id);
            IncreaseView(viewCounter, id);
            var productVm = Mapper.Map<Product, ProductViewModel>(product);
            var relatedProducts = _productService.GetRelatedProducts(id, 5);
            var hotProducts = _productService.GetRelatedProducts(id, 5);
        

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productVm.MoreImages);
            var categoryVm = Mapper.Map<ProductCategory, ProductCategoryViewModel>(_productCategoryService.FindById(productVm.CategoryID));
            ViewBag.Category = categoryVm;
            ViewBag.MoreImages = listImages;
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetTagsByProduct(id));

            CommonController common = new CommonController(_productService);
            int currentPageIndex = page.HasValue ? page.Value : 1;
            productVm.ListProducts = common.ProductListAjax(page, searchString).ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxProductList", productVm.ListProducts);
            else
                return View(productVm);
        }

        public ActionResult Category(int id, int brandid = 0, int page = 1, string sort = "")
        {
            TempData["categoryID"] = id;
            TempData["categoryAlias"] = _productCategoryService.FindById(id).Alias;
            StringHelper.pageActive = "category";
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var product = _productService.GetByCategoryIDPaging(id, brandid, page, pageSize, sort, out totalRow);
            var productVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var category = _productCategoryService.FindById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Shop(int page = 1, int brandid = 0, string sort = "")
        {
            StringHelper.pageActive = "shop";
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var product = _productService.GetAllPaging(page, brandid, sort, pageSize, out totalRow);
            var productVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.SortKey = sort;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        [ChildActionOnly]
        public ActionResult BrandsList()
        {
            var model = _brandService.GetAll("");
            var brandVm = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(model);
            return PartialView(brandVm);
        }

        public JsonResult GetProductsByName(string keyword)
        {
            var product = _productService.GetProductsByName(keyword);
            return Json(new
            {
                data = product
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductsByTag(string tagId, int page = 1, int brandID = 0, string sort = "")
        {
            TempData["tagId"] = tagId;
            StringHelper.pageActive = "productbytag";
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var product = _productService.ProductsByTag(tagId, sort, brandID, page, pageSize, out totalRow);
            var productVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int brandid = 0, int page = 1, string sort = "")
        {
            StringHelper.pageActive = "search";
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var product = _productService.GetByKeywordPaging(keyword, brandid, page, pageSize, sort, out totalRow);
            var productVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(product);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
        #endregion
    }
}