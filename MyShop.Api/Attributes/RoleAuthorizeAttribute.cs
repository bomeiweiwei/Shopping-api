using MyShop.Shared.Enums;

namespace MyShop.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RoleAuthorizeAttribute : Attribute
    {
        public HashSet<int> AllowedRoles { get; }

        public RoleAuthorizeAttribute(params UserRole[] roles)
        {
            AllowedRoles = roles.Select(r => (int)r).ToHashSet();
        }
    }
}
