using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    public class ApplicationUserGroup
    {
        [StringLength(128)]
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroups { get; set; }
    }
}