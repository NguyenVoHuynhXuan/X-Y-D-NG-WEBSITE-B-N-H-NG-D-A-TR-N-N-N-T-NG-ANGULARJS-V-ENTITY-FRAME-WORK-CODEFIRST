namespace uStora.Web.Models
{
    public class ProductTagViewModel
    {
        public long ProductID { get; set; }

        public string TagID { get; set; }

        public ProductViewModel Product { get; set; }
        public virtual TagViewModel Tag { set; get; }
    }
}