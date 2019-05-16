using MvcPaging;
using System;
using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    [Serializable]
    public class ProductViewModel : Auditable
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int CategoryID { get; set; }

        public int BrandID { get; set; }

        public string Image { get; set; }

        public string MoreImages { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Warranty { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public long? ViewCount { get; set; }

        public int? Quantity { get; set; }

        public string Tags { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { get; set; }
        public IPagedList<ProductViewModel> ListProducts { get; set; }
        public virtual BrandViewModel Brand { get; set; }
    }
}