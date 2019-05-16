using MvcPaging;
using System;
using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    [Serializable]
    public class WishlistViewModel: Auditable
    {
        public int ID { get; set; }
        public string UserId { get; set; }

        public long ProductId { get; set; }

        public virtual ApplicationUserViewModel ApplicationUser { get; set; }

        public virtual ProductViewModel Product { get; set; }

        public IPagedList<ProductViewModel> ListProducts { get; set; }
    }
}