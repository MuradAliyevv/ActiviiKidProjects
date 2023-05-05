using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.ViewModel
{
    public class ProductViewModel
    {
        public IEnumerable<dynamic> Products { get; set; }
        public dynamic Product { get; set; }
        public IEnumerable<dynamic> ProductColorSizeCounts{ get; set; }

        public Basket? Basket { get; set; }
    }
}
