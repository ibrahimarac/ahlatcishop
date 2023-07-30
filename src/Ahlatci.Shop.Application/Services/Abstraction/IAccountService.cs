using Ahlatci.Shop.Application.Models.RequestModels.Accounts;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IAccountService
    {
        Task<bool> CreateUser(CreateUserVM createUserVM);
    }
}
