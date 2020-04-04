using SftLib.Data.Domain.Models;

namespace SftLibrary.Data.Domain.Services.Communication
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; private set; }
        public BookResponse(bool success, string message, Book book) : base(success, message) => Book = book;

        /// <summary>
        /// create a success response
        /// </summary>
        /// <param name="book">Save book</param>
        /// /// <returns>Response.</returns>
        public BookResponse(Book book) : this(true, string.Empty, book)
        {

        }

        /// <summary>
        /// create a error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// /// <returns>Response.</returns>
        public BookResponse(string message) : this(false, message, null)
        {

        }

    }
}
