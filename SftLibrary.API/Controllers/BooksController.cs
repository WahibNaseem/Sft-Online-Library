using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SftLib.Data.Domain.Models;
using SftLibrary.API.Extensions;
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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICheckoutService _checkoutService;
        private readonly ICheckoutHistoryService _checkoutHistoryService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, ICheckoutService checkoutService, ICheckoutHistoryService checkoutHistoryService, IMapper mapper)
        {
            _bookService = bookService;
            _checkoutService = checkoutService;
            _checkoutHistoryService = checkoutHistoryService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("books")]
        public async Task<IActionResult> GetBooks([FromQuery]string search)
        {

            var books = await _bookService.ListAsync(search);
            var resources = _mapper.Map<IEnumerable<BookCheckoutResource>>(books);

            return Ok(resources);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            var result = await _bookService.FindByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);


            var bookResource = _mapper.Map<BookCheckoutResource>(result.Book);

            var history = await _checkoutHistoryService.FindByBookId(id);
            
             var historyResource = _mapper.Map<IEnumerable<CheckoutHistoryResource>>(history);
             bookResource.CheckoutHistories = historyResource;
            

            return Ok(bookResource);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SaveBookResource saveBookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var book = _mapper.Map<Book>(saveBookResource);
            var result = await _bookService.SaveAsync(book);

            if (!result.Success)
                return BadRequest();

            var bookResource = _mapper.Map<BookCheckoutResource>(result.Book);

            return Ok(bookResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var bookResource = _mapper.Map<BookCheckoutResource>(result.Book);
            return Ok(bookResource);
        }

        [HttpPost("{id}/checkout/{bookId}")]
        public async Task<IActionResult> PlaceCheckout(int id, int bookId)
        {
            var result = await _checkoutService.CheckOutItem(id, bookId);
            if (!result.Success)
                return BadRequest(result.Message);

            var resource = _mapper.Map<BookCheckoutResource>(result.Book);
            return Ok(resource);
        }

        [HttpPost("checkinItem/{bookId}")]
        public async Task<IActionResult> CheckIn(int bookId)
        {
            var result = await _checkoutService.CheckInItem(bookId);
            if (!result.Success)
                return BadRequest(result.Message);

            var resource = _mapper.Map<BookCheckoutResource>(result.Book);
            return Ok(resource);
        }
    }
}
