using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActiviKidWebUI.Models.Entity
{
    public class Orders :BaseEntity
    {

        public bool approved { get; set; }
        public string UserId { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int? Color { get; set; }
        public int ProducCreatedByUserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderNumber { get; set; }
 
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal? Price { get; set; }
        public decimal? DisCountPrice { get; set; }
   
    }
}
