using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uStora.Model.Abstracts;

namespace uStora.Model.Models
{
    [Table("Vehicles")]
    public class Vehicle : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        public string VehicleNumber { get; set; }
        

        [MaxLength(100)]
        public string DriverName { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ModelID { get; set; }

        [MaxLength(150)]
        public string Model { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public virtual IEnumerable<TrackOrder> TrackOrders { get; set; }
    }
}