using SftLib.Data.Domain.Models;

namespace SftLibrary.Data.Domain.Services.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User { get; private set; }

        public UserResponse(bool success, string message, User user) : base(success, message) => User = user;

        /// <summary>
        /// create a success response
        /// </summary>
        /// <param name="checkout">Save User</param>
        /// /// /// <returns>Response.</returns>

        public UserResponse(User user) : this(true, "", user)
        {

        }
        /// <summary>
        /// create a error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// /// /// <returns>Response.</returns>
        public UserResponse(string message) : this(false, message, null)
        {

        }
    }
}
