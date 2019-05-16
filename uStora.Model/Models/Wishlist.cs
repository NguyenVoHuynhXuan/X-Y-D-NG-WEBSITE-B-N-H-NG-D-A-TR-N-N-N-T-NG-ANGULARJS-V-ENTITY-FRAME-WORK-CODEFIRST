using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uStora.Model.Abstracts;

namespace uStora.Model.Models
{
    [Table("Wishlists")]
    public class Wishlist : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        public string UserId { get; set; }

        public long ProductId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}