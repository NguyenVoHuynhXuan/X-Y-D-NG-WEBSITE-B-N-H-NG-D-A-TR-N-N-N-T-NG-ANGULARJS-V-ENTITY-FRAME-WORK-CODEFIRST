using AutoMapper;
using MvcPaging;
using System.Collections.Generic;
using System.Web.Mvc;
using uStora.Model.Models;
using uStora.Service;
using uStora.Web.Models;

namespace uStora.Web.Controllers
{
    public class CommonController : Controller
    {
        private IProductService _productService;

        public CommonController(IProductService productService)
        {
            _productService = productService;
        }
        
        public IList<ProductViewModel> ProductListAjax(int? page, string searchString)
        {
            IList<Product> listProducts = _productService.GetAllPagingAjax(searchString);

            var listProductsVm = Mapper.Map<IList<Product>, IList<ProductViewModel>>(listProducts);
            return listProductsVm;
        }
    }
}