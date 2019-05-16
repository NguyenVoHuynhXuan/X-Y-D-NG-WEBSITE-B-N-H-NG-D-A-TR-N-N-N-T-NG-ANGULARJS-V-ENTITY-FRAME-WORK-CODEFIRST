using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uStora.Model.Models
{
    [Table("VistorStatistics")]
    public class VisitorStatistic
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        public DateTime VisitedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string IPAddress { get; set; }
    }
}