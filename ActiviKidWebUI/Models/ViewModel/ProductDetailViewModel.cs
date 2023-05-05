using ActiviKidWebUI.Models.Entity;


namespace ActiviKidWebUI.Models.ViewModel
{
    public class ProductDetailViewModel
    {
        public dynamic Product { get; set; }
        public IEnumerable<dynamic> ProductColorSizeCount { get; set; }

        public int ProducId { get; set; }

        public int ColorId { get; set; }
        public int Count { get; set; }

        public IEnumerable<Orders> Orderss { get; set; }

        public List<dynamic> Color { get; set; }
        public IEnumerable<Orders> Orders { get; set; }

        public Order Order { get; set; }

        public Basket Basket { get; set; }

    }
}
