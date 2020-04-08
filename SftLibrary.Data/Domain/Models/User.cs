using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SftLibrary.Data.Domain.Models;

namespace SftLib.Data.Domain.Models
{
    public class User:IdentityUser<int>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public ICollection<UserRole>  UserRoles { get; set; }
        public ICollection<Checkout> CheckOuts { get; set; }
    }
}
