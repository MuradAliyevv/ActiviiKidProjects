using ActiviKidWebUI.Models.Entity;

namespace ActiviKidWebUI.Models.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<ActiviKidWebUI.Models.Entity.Basket> Baskets { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ActiviKidWebUI.Models.Entity.Color> Color { get; set; }
        public ActiviKidWebUI.Models.Entity.Orders Orders { get; set; }

        public List<int> ProductId { get; set; }
        public string Password { get; set; }

        public Order Order { get; set; }
    }
}
