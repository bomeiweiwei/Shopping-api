using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Exceptions
{
    public abstract class BusinessException : HttpStatusException
    {
        protected BusinessException(
            int httpStatusCode,
            ReturnCode appStatusCode,
            string message)
            : base(httpStatusCode, appStatusCode, message)
        {
        }
    }
}
