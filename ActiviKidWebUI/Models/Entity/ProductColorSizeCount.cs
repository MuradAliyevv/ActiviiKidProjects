namespace ActiviKidWebUI.Models.Entity
{
    public class ProductColorSizeCount:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int Count { get; set; }

        public string ImagePath { get; set; }
    }
}
