//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using ActiviKidWebUI.Models.FormModel;
using System.Collections.Generic;

namespace ActiviKidWebUI.Models.Entity
{
    public class ProductRus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string? ImagePath { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public int GenderId { get; set; }
        public GendersRus Gender { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int CategoryId { get; set; }
        public CategoryRus Category { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }
        public bool MostLiked { get; set; }
        public bool BestSeller { get; set; }
        public int? Count { get; set; }
        public bool NewArrival { get; set; }
        public int? ConstantCount { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public bool Top { get; set; }

        public ICollection<ProductColorSizeCountRus> ProductColorSizeCount { get; set; }



    }
}
