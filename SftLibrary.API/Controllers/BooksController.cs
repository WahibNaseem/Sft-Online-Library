using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SftLib.Data.Domain.Models;
using SftLibrary.API.Extensions;
using SftLibrary.API.Resources;
using SftLibrary.Data.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SftLibrary.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.ListAsync();
            var resources = _mapper.Map<IEnumerable<BookResource>>(books);

            return Ok(resources);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            var result = await _bookService.FindByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var bookResource = _mapper.Map<BookResource>(result.Book);

            return Ok(bookResource);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> PostAsync(int id, [FromBody]SaveBookResource saveBookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var book = _mapper.Map<Book>(saveBookResource);
            var result = await _bookService.SaveAsync(book);

            if (!result.Success)
                return BadRequest();

            var bookResource = _mapper.Map<BookResource>(result.Book);

            return Ok(bookResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var bookResource = _mapper.Map<BookResource>(result.Book);
            return Ok(bookResource);
        }
    }
}
