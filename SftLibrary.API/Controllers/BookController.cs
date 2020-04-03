using Microsoft.AspNetCore.Mvc;
using SftLibrary.Data.Domain.Services;
using System.Threading.Tasks;

namespace SftLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var list = await _bookService.ListAsync();

            return Ok(list);
        }
    }
}
