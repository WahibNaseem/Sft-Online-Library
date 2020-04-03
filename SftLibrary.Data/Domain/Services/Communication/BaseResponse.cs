using System;
using System.Collections.Generic;
using System.Text;

namespace SftLibrary.Data.Domain.Services.Communication
{
    public class BaseResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; set; }

        public BaseResponse(bool success , string message)
        {
            Success = success;
            Message = message;
        }
    }
}
