using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace uStora.Model.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}