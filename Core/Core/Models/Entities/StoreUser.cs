using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class StoreUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
    }
}
