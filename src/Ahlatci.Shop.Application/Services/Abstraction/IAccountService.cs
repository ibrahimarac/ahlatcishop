using Ahlatci.Shop.Application.Models.Dtos.Accounts;
using Ahlatci.Shop.Application.Models.RequestModels.Accounts;
using Ahlatci.Shop.Application.Wrapper;

namespace Ahlatci.Shop.Application.Services.Abstraction
{
    public interface IAccountService
    {
        Task<Result<bool>> CreateUser(CreateUserVM createUserVM);

        Task<Result<TokenDto>> Login(LoginVM loginVM);
    }
}
