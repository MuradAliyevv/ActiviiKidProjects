using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActiviKidWebUI.Models.Entity
{
	public class Category:BaseEntity
	{

        [Required(ErrorMessage = "The name must be written")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
	}
}
