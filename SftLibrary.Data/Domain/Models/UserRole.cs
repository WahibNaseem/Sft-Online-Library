using Microsoft.AspNetCore.Identity;
using SftLib.Data.Domain.Models;

namespace SftLibrary.Data.Domain.Models
{
    public class UserRole: IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
        
    }
}