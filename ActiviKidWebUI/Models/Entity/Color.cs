using System.Collections.Generic;

namespace ActiviKidWebUI.Models.Entity
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<ProductColorSizeCount> ProductColorCloths { get; set; }
    }
}
