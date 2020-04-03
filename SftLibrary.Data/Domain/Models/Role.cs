
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SftLibrary.Data.Domain.Models
{
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> Users { get; set; }
    }
}