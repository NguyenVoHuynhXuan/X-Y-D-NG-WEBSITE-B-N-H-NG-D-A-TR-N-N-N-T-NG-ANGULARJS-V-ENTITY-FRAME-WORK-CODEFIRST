using System;
using System.ComponentModel.DataAnnotations;

namespace uStora.Model.Abstracts
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        [MaxLength(150)]
        public string MetaKeyword { get; set; }

        [MaxLength(150)]
        public string MetaDescription { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}