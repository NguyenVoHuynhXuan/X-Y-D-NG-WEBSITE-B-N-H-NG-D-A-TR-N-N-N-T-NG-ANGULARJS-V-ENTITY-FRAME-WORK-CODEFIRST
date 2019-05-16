using System.ComponentModel.DataAnnotations;
using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    public class ProductCategoryViewModel : Auditable
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public bool IsDeleted { get; set; }
        
    }
}