using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    public class BrandViewModel : Auditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Website { get; set; }

        public bool? HotFlag { get; set; }

        public bool IsDeleted { get; set; }
    }
}