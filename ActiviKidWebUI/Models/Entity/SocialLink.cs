using System.ComponentModel.DataAnnotations;

namespace ActiviKidWebUI.Models.Entity
{
    public class SocialLink:BaseEntity
    {
        [Required]
        public string Icon { get; set; }

        [Required(ErrorMessage = "The name must be written")]
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
