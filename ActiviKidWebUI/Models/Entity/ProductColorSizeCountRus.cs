using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class ProductColorSizeCountRus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int ProductId { get; set; }
        public ProductRus Product { get; set; }
        [System.ComponentModel.DataAnnotations.Required]


        public int ColorId { get; set; }
        public ColorRus Color { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int Count { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string ImagePath { get; set; }
    }
}
