using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    public class ContactDetailViewModel : Auditable
    {
        public int ID { get; set; }

        [MaxLength(250, ErrorMessage = "Chỉ nhập 250 ký tự.")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50, ErrorMessage = "Chỉ nhập 50 ký tự.")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(250, ErrorMessage = "Chỉ nhập 250 ký tự.")]
        public string Website { get; set; }

        [MaxLength(250, ErrorMessage = "Chỉ nhập 250 ký tự.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Vui lòng nhập Email của bạn.")]
        public string Email { get; set; }

        [MaxLength(250, ErrorMessage = "Chỉ nhập 250 ký tự.")]
        public string Address { get; set; }

        public string Description { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
    }
}