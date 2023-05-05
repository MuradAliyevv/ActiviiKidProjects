using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActiviKidWebUI.Models.Entity.Identity
{
    public class AppUserRole : IdentityUserRole<int>
    {
    }
}
