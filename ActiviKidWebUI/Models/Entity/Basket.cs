namespace ActiviKidWebUI.Models.Entity
{
    public class Basket:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? Count { get; set; }
        public int? ColorId { get; set; }
        public Color? Color { get; set; }


        public string UserId { get; set; }
        public bool approved { get; set; }

        public decimal? Price { get; set; }
        public decimal? DisCountPrice { get; set; }
        public decimal? Total { get; set; }
    }
}
