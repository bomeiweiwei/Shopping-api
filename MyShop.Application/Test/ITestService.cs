using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.Test
{
    public interface ITestService
    {
        Task<bool> GetConnectResult();
    }
}
