using SftLibrary.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SftLibrary.Data.Domain.Services.Communication
{
    public class CheckoutResponse : BaseResponse
    {
        public Checkout Checkout { get; protected set; }

        public CheckoutResponse(bool success, string message, Checkout checkout) : base(success, message) => Checkout = checkout;

        /// <summary>
        /// create a success response
        /// </summary>
        /// <param name="checkout">Save checkout</param>
        /// /// /// <returns>Response.</returns>
        public CheckoutResponse(Checkout checkout) : this(true, "", checkout)
        {
            
        }
        /// <summary>
        /// create a error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// /// /// <returns>Response.</returns>
        public CheckoutResponse(string message) : this(false, message, null)
        {

        }
    }
}
