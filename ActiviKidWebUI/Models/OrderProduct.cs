using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.Entity.Identity;

namespace ActiviKidWebUI.Models
{
    public class OrderProduct:BaseEntity  
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string OrderId { get; set; }


    }
}
