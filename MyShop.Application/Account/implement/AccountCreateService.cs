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
        private readonly IAccountCreateRepository _accountCreateRepository;
        private readonly IAdminAccountCreateRepository _adminAccountCreateRepository;
        private readonly IPasswordHasher<AccountDto> _hasher;
        private readonly ILogger<AccountCreateService> _logger;
        private readonly IExecutionStrategyFactory _strategyFactory;
        public AccountCreateService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser, IPasswordHasher<AccountDto> hasher, ILogger<AccountCreateService> logger, IExecutionStrategyFactory strategyFactory, IAccountCreateRepository accountCreateRepository, IAdminAccountCreateRepository adminAccountCreateRepository) : base(factory, currentUser)
        {
            _hasher = hasher;
            _logger = logger;
            _strategyFactory = strategyFactory;
            _accountCreateRepository = accountCreateRepository;
            _adminAccountCreateRepository = adminAccountCreateRepository;
        }
        /// <summary>
        /// 建立會員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<MemberRegisterResp>> CreateMemberAccountWithProfileAsync(AccountRegisterReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<MemberRegisterResp>();
            MemberRegisterDto dto = new MemberRegisterDto();
            try
            {
                dto = new MemberRegisterDto()
                {
                    Username = req.Username,
                    Password = req.Password,
                    Email = req.Email,
                    Phone = req.Phone,
                    CreatedAt = DateTime.UtcNow // DateTime.UtcNow.ToTaipeiTime();
                };

                dto.PasswordHash = _hasher.HashPassword(null!, req.Password);

                result.Data = await _accountCreateRepository.CreateMemberAccountWithProfileAsync(dto, ct);
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
            finally
            {
                req.Password = string.Empty;
                dto.PasswordHash = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// 建立廠商帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<VendorRegisterResp>> CreateVendorAccountWithProfileAsync(AccountRegisterReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<VendorRegisterResp>();
            VendorRegisterDto dto = new VendorRegisterDto();
            try
            {
                dto = new VendorRegisterDto()
                {
                    Username = req.Username,
                    Password = req.Password,
                    Email = req.Email,
                    Phone = req.Phone,
                    CreatedAt = DateTime.UtcNow
                };

                dto.PasswordHash = _hasher.HashPassword(null!, req.Password);

                result.Data = await _accountCreateRepository.CreateVendorAccountWithProfileAsync(dto, ct);
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
            finally
            {
                req.Password = string.Empty;
                dto.PasswordHash = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// 建立後台管理員帳號
        /// </summary>
        /// <param name="req"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<ApiResponseBase<AdminCreateResp>> CreateAdminAccountAsync(AdminAccountRegisterReq req, CancellationToken ct = default)
        {
            var result = new ApiResponseBase<AdminCreateResp>();
            AdminCreateDto dto = new AdminCreateDto();
            try
            {
                dto = new AdminCreateDto()
                {
                    Username = req.Username,
                    Password = req.Password,
                    Email = req.Email,
                    Phone = req.Phone,
                    RoleIds = req.RoleIds,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = CurrentUser.AccountId
                };

                dto.PasswordHash = _hasher.HashPassword(null!, req.Password);

                using var ctx = MainDB(ConnectionMode.Master);
                var strategy = _strategyFactory.Create(ctx);

                await strategy.ExecuteAsync(async () =>
                {
                    await using var tx = await ctx.BeginTransactionAsync(ct);

                    try
                    {
                        // 先產生帳號基本資料
                        long accountId = await _accountCreateRepository.StepCreateBasicAdminAccountAsync(ctx, dto, ct);
                        // 再產生後台管理員專屬資料
                        result.Data = await _adminAccountCreateRepository.StepCreateAdminAccountAsync(ctx, dto, accountId, ct);

                        await tx.CommitAsync(ct);
                    }
                    catch
                    {
                        await tx.RollbackAsync(ct);
                        throw;
                    }
                }, ct);
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
            finally
            {
                req.Password = string.Empty;
                dto.PasswordHash = string.Empty;
            }

            return result;
        }
    }
}
