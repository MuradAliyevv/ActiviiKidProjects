using System.ComponentModel.DataAnnotations;

namespace ActiviKidWebUI.Models.Entity
{
    public class News: BaseEntity
    {
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "The name must be written")]
        [MinLength(3, ErrorMessage = "Blog name at least must contain 3 letters.")]
        public string Name { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Blog description at least must contain 20 letters.")]
        public string Description { get; set; }
    }
}
