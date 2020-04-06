using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SftLib.Data.Domain.Models;
using SftLib.Data.Persistance.Contexts;
using SftLibrary.API.Resources;
using SftLibrary.Data.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
       
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(AppDbContext context, UserManager<User> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {         


            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles 
                                               on userRole.RoleId equals role.Id                                                
                                               select role.Name).ToList()
                                  }
                                 ).ToListAsync();

            return Ok(userList);
        }

        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, UserRoleUpdateResource roleUpdateResource)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("Username did not provided");

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return BadRequest("User not found to update roles");
                

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleUpdateResource.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }
    }
}
