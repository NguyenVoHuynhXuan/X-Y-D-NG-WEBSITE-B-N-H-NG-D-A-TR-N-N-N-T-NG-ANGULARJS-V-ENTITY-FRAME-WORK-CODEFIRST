using System.Collections.Generic;

namespace uStora.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<BrandViewModel> Brands { get; set; }
        public IEnumerable<ProductViewModel> TopViews { get; set; }
        public IEnumerable<ProductViewModel> LatestProducts { get; set; }
        public IEnumerable<ProductViewModel> TopSaleProducts { get; set; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }

    }
}