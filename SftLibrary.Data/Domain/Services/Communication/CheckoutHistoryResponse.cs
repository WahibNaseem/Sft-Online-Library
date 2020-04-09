using SftLibrary.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SftLibrary.Data.Domain.Services.Communication
{
    public class CheckoutHistoryResponse : BaseResponse
    {
        public CheckoutHistory CheckoutHistory { get; private set; }
        public CheckoutHistoryResponse(bool success, string message, CheckoutHistory checkoutHistory) : base(success, message) => CheckoutHistory = checkoutHistory;
        /// <summary>
        /// create a success response
        /// </summary>
        /// <param name="book">Save checkoutHistory</param>
        /// /// <returns>Response.</returns>
        public CheckoutHistoryResponse(CheckoutHistory checkoutHistory) : this(true, string.Empty, checkoutHistory)
        {

        }

        /// <summary>
        /// create a error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// /// <returns>Response.</returns>
        public CheckoutHistoryResponse(string message) : this(false, message, null)
        {

        }

    }
}
