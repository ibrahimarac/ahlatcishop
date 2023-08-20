using Ahlatci.Shop.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ahlatci.Shop.UI.Authorization
{
    public class RoleAccessRequirement : IAuthorizationRequirement
    {
        public Roles[] Roles { get; set; }

        public RoleAccessRequirement(params Roles[] roles)
        {
            Roles = roles;
        }
    }
}
