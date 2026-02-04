using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Exceptions
{
    public abstract class HttpStatusException : Exception
    {
        public int HttpStatusCode { get; }

        public ReturnCode AppStatusCode { get; }

        protected HttpStatusException(int httpStatusCode, ReturnCode appStatusCode, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            AppStatusCode = appStatusCode;
        }
    }
}
