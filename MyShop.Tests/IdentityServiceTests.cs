using Microsoft.AspNetCore.Identity;
using Moq;
using MyShop.Application.Account;
using MyShop.Application.Identity;
using MyShop.Application.Identity.implement;
using MyShop.Application.Redis;
using MyShop.Domain;
using MyShop.Models.Dto.Account;
using MyShop.Models.Dto.Identity;
using MyShop.Models.Req.Identity;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyShop.Tests
{
    public class IdentityServiceTests
    {
        [Fact]
        public async Task VerifyLoginData_ReturnsFalse_WhenAccountNotFound()
        {
            var factoryMock = new Mock<IMyShopDbContextFactory>();
            var currentUserMock = new Mock<ICurrentUserAccessor>();
            var hasherMock = new Mock<IPasswordHasher<AccountDto>>();
            var accountReadMock = new Mock<IAccountReadService>();
            var redisMock = new Mock<IRedisService>();

            accountReadMock
                .Setup(x => x.GetLoginAccountData(It.IsAny<Models.Req.Account.GetAccountReq>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AccountDto?)null);

            var svc = new IdentityService(factoryMock.Object, currentUserMock.Object, hasherMock.Object, accountReadMock.Object, redisMock.Object);

            var req = new LoginReq { UserName = "noexist", Password = "pwd" };
            var result = await svc.VerifyLoginData(req, CancellationToken.None);

            Assert.False(result.IsVerifySuccess);
            Assert.Null(result.Data);
            Assert.Equal("pwd", req.Password); // 未發生清空
        }

        [Fact]
        public async Task VerifyLoginData_ReturnsFalse_WhenPasswordInvalid()
        {
            var factoryMock = new Mock<IMyShopDbContextFactory>();
            var currentUserMock = new Mock<ICurrentUserAccessor>();
            var hasherMock = new Mock<IPasswordHasher<AccountDto>>();
            var accountReadMock = new Mock<IAccountReadService>();
            var redisMock = new Mock<IRedisService>();

            var account = new AccountDto { AccountId = 1, UserName = "user1", PasswordHash = "hashed" };
            accountReadMock
                .Setup(x => x.GetLoginAccountData(It.IsAny<Models.Req.Account.GetAccountReq>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            hasherMock
                .Setup(h => h.VerifyHashedPassword(It.IsAny<AccountDto>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            var svc = new IdentityService(factoryMock.Object, currentUserMock.Object, hasherMock.Object, accountReadMock.Object, redisMock.Object);

            var req = new LoginReq { UserName = "user1", Password = "wrongpwd" };
            var result = await svc.VerifyLoginData(req, CancellationToken.None);

            Assert.False(result.IsVerifySuccess);
            Assert.Null(result.Data);
            Assert.Equal("wrongpwd", req.Password); // 驗證失敗仍不會清空
        }

        [Fact]
        public async Task VerifyLoginData_ReturnsTrue_And_ClearsPasswords_WhenSuccess()
        {
            var factoryMock = new Mock<IMyShopDbContextFactory>();
            var currentUserMock = new Mock<ICurrentUserAccessor>();
            var hasherMock = new Mock<IPasswordHasher<AccountDto>>();
            var accountReadMock = new Mock<IAccountReadService>();
            var redisMock = new Mock<IRedisService>();

            var account = new AccountDto { AccountId = 42, UserName = "gooduser", PasswordHash = "hashedValue" };
            accountReadMock
                .Setup(x => x.GetLoginAccountData(It.IsAny<Models.Req.Account.GetAccountReq>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            hasherMock
                .Setup(h => h.VerifyHashedPassword(It.IsAny<AccountDto>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            var svc = new IdentityService(factoryMock.Object, currentUserMock.Object, hasherMock.Object, accountReadMock.Object, redisMock.Object);

            var req = new LoginReq { UserName = "gooduser", Password = "correctpwd" };
            var result = await svc.VerifyLoginData(req, CancellationToken.None);

            Assert.True(result.IsVerifySuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(42, result.Data.AccountId);
            Assert.Equal("gooduser", result.Data.UserName);

            // 方法會清空輸入物與回傳物的密碼/雜湊
            Assert.Equal(string.Empty, req.Password);
            Assert.Equal(string.Empty, result.Data.PasswordHash);
        }
    }
}