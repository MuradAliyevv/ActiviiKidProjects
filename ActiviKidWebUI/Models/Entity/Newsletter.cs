using System.ComponentModel.DataAnnotations;

namespace ActiviKidWebUI.Models.Entity
{
    public class Newsletter:BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
