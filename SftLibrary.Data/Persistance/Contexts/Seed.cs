using Microsoft.AspNetCore.Identity;
using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SftLibrary.Data.Persistance.Contexts
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin"},
                    new Role { Name = "Member"},
                    new Role { Name = "Moderator"},
                };

                foreach (var role in roles)
                    _roleManager.CreateAsync(role).Wait();

                var adminUser = new User
                {
                    UserName = "Admin",
                    FirstName = "Wahib",
                    LastName = "Naseem",
                    Gender = "Male"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "root").Result;

                if(result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRoleAsync(admin, "Admin");
                }

            }
        }
    }
}
