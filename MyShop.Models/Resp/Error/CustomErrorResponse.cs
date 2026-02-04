using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Resp.Error
{
    public class CustomErrorResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string? TraceId { get; set; }

        public CustomErrorResponse(string message, int statusCode = StatusCodes.Status401Unauthorized)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
