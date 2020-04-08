using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SftLibrary.API.Resources;
using SftLibrary.Data.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SftLibrary.API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(Name= "Users")]   
        public async Task<IActionResult> GetUsers()
        {
            var userList = await _userService.ListAsync();
            var resources = _mapper.Map<IEnumerable<UserForListResource>>(userList);

            return Ok(resources);
        }

        [HttpGet("{id}",Name ="User")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.FindByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<UserForCheckoutResource>(result.User);

            return Ok(userResource);
        }
        

    }
}
