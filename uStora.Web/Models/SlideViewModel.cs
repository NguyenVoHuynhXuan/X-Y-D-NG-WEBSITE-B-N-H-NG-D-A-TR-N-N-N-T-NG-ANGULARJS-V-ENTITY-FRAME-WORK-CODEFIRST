using uStora.Model.Abstracts;

namespace uStora.Web.Models
{
    public class SlideViewModel : Auditable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public string URL { get; set; }

        public int? DisplayOrder { get; set; }
    }
}