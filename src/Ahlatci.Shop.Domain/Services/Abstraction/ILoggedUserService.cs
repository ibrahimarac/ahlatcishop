using Ahlatci.Shop.Domain.Entities;

namespace Ahlatci.Shop.Domain.Services.Abstraction
{
    public interface ILoggedUserService
    {
        int? UserId { get; }
        Roles? Role { get; }
        string Username { get; }
        string Email { get; }
    }
}
