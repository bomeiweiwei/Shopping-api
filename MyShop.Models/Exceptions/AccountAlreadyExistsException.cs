using Microsoft.AspNetCore.Http;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Models.Exceptions
{
    public sealed class AccountAlreadyExistsException : BusinessException
    {
        public string Username { get; }

        public AccountAlreadyExistsException(string username)
            : base(
                StatusCodes.Status400BadRequest,
                ReturnCode.AccountAlreadyExists,
                $"Account '{username}' already exists.")
        {
            Username = username;
        }
    }

}
