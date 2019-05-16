using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerEmail { get; set; }
        
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        public string CustomerMessage { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Column(TypeName ="varchar")]
        [MaxLength(128)]
        public string CreatedBy { get; set; }

        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        [Required]
        public int PaymentStatus { get; set; }

        [Required]
        public bool Status { get; set; }

        [MaxLength(128)]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

        public virtual IEnumerable<TrackOrder> TrackOrders { get; set; }
    }
}