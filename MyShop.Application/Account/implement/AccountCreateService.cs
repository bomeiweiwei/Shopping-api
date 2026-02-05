using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyShop.Application.Identity;
using MyShop.Domain;
using MyShop.Domain.Account;
using MyShop.Models;
using MyShop.Models.Dto.Account;
using MyShop.Models.Exceptions;
using MyShop.Models.Req.Account;
using MyShop.Models.Resp.Account;
using MyShop.Shared.Enums;
using MyShop.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Account.implement
{
    public class AccountCreateService : BaseService, IAccountCreateService
    {
        private readonly IAccountCreateRepository _repo;
        private readonly IPasswordHasher<AccountDto> _hasher;
        private readonly ILogger<AccountCreateService> _logger;
        public AccountCreateService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser, IPasswordHasher<AccountDto> hasher, ILogger<AccountCreateService> logger, IAccountCreateRepository repo) : base(factory, currentUser)
        {
            _repo = repo;
            _hasher = hasher;
            _logger = logger;
        }

        public async Task<ApiResponseBase<MemberRegisterResp>> CreateMemberAccountWithProfileAsync(MemberRegisterReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<MemberRegisterResp>();
            try
            {
                MemberRegisterDto dto = new MemberRegisterDto()
                {
                    Username = req.Username,
                    Password = req.Password,
                    Email = req.Email,
                    Phone = req.Phone
                };

                dto.PasswordHash = _hasher.HashPassword(null!, req.Password);

                req.Password = string.Empty;

                result.Data = await _repo.CreateMemberAccountWithProfileAsync(dto, ct);
            }
            catch (AccountAlreadyExistsException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Account already exists. Username={Username}",
                    req.Username
                );
                result.StatusCode = (long)ReturnCode.AccountAlreadyExists;
                result.Message = ReturnCode.AccountAlreadyExists.GetDescription();
            }
            catch (Exception)
            {
                result.StatusCode = (long)ReturnCode.ExceptionError;
                result.Message = ReturnCode.ExceptionError.GetDescription();
                throw;
            }

            return result;
        }
    }
}
