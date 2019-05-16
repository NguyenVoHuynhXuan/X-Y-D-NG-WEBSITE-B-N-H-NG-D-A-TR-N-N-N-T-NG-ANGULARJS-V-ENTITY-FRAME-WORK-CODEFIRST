using uStora.Model.Abstracts;
using System.Collections.Generic;

namespace uStora.Web.Models
{
    public class TagViewModel : Auditable
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<PostTagViewModel> PostTags { get; set; }
    }
}