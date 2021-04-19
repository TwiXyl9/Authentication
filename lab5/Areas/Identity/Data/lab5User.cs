using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace lab5.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the lab5User class
    public class lab5User : IdentityUser
    {
        public string Registration_date { get; set; }
        public string LastLogin_date { get; set; }
        public bool IsBlocked { get; set; }
    }
}
