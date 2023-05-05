using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ActiviKidWebUI.Models.Entity.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? DeletedDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }

    }
}
