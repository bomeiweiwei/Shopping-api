using MyShop.Application.Identity;
using MyShop.Domain;
using MyShop.Models.Dto.Identity;
using MyShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application
{
    public class BaseService
    {
        private readonly IMyShopDbContextFactory _factory;
        protected readonly ICurrentUserAccessor _currentUser;
        protected BaseService(IMyShopDbContextFactory factory, ICurrentUserAccessor currentUser)
        {
            _factory = factory;
            _currentUser = currentUser;
        }
        protected IMyShopDbContext MainDB(ConnectionMode mode = ConnectionMode.Master) => _factory.Create(mode);

        protected JwtUserInfo CurrentUser => _currentUser.Current;
    }
}
