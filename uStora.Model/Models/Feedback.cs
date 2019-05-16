using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Column(TypeName ="varchar")]
        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}