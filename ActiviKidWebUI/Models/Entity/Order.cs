using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActiviKidWebUI.Models.Entity
{
    public class Order :BaseEntity
    {
        [Required(ErrorMessage = "Please Enter FirstName")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter LastName")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Country")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Enter Adress")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]

        public string Region { get; set; }
        [Required(ErrorMessage = "Please Enter City")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]
        public string City { get; set; }

        public string? State { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        [Required(ErrorMessage = "Please Enter PostalCode")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 1)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 5)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Card Type")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Please Enter Card Number")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 16)]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Card CVV")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And uppercase and lowercase letters should be used", MinimumLength = 3)]
        public string CardCVV { get; set; }
        [Required(ErrorMessage = "Please Enter Expiration Month")]
        public string ExpirationMonth { get; set; }
        [Required(ErrorMessage = "Please Enter Expiration Year")]
        public string ExpirationYear { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
