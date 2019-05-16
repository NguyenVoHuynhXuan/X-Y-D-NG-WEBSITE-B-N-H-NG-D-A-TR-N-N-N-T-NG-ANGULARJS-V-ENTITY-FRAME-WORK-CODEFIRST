using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace uStora.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        [Required]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        [MaxLength(20)]
        public string Gender { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Image { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName ="varchar")]
        [MaxLength(128)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(128)]
        public string CreatedBy { get; set; }

        public bool IsViewed { get; set; }

        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<TrackOrder> TrackOrders { get; set; }
        public virtual IEnumerable<Wishlist> Wishlists { get; set; }
    }
}