using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActiviKidWebUI.Models.Entity
{
    public class Contact :BaseEntity
    {


        [Required]
        public string FullName { get; set; }


        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }


        [Required]
        public bool Isanswerd { get; set; }

    }
}
