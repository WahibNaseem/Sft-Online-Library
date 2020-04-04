using Microsoft.AspNetCore.Identity;
using SftLib.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SftLibrary.Data.Persistance.Contexts
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<User> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //public void SeedUsers()
        //{
        //    if(!_userManager.Users.Any())
        //    {
        //        var roles = new List<Role>{
        //        };
        //    }
        //}
    }
}
