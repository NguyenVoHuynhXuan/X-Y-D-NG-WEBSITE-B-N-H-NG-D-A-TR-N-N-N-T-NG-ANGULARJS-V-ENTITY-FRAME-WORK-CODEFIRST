using uStora.Model.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(250)]
        public string Website { get; set; }

        [MaxLength(250)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public string Description { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
    }
}