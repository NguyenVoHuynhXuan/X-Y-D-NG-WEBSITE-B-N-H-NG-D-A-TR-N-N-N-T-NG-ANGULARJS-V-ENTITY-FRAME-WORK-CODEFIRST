using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    [Table("ApplicationRoleGroups")]
    public class ApplicationRoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [StringLength(128)]
        [Key]
        [Column(Order = 2)]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRoles { get; set; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroups { get; set; }
    }
}