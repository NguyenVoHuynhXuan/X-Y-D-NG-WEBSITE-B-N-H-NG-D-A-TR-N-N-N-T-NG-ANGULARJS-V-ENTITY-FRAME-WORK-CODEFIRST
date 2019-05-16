using System.ComponentModel.DataAnnotations;

namespace uStora.Web.Models
{
    public class OrderDetailViewModel
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public long ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual OrderViewModel Order { get; set; }

        public virtual ProductViewModel Product { get; set; }
    }
}