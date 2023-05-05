using ActiviKidWebUI.Models.Entity.Identity;
using System;
using System.Collections.Generic;

namespace ActiviKidWebUI.Models.Entity
{
    public partial class Customer: BaseEntity
    {


        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public string? Description { get; set; } = null!;

    }
}
