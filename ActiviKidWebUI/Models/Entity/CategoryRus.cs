using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class CategoryRus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name must be written")]
        public string Name { get; set; }
        public ICollection<ProductRus> Products { get; set; }
    }
}
