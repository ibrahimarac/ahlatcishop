using Ahlatci.Shop.Domain.Entities;

namespace Ahlatci.Shop.Application.Models.Dtos.Accounts
{
    public class TokenDto
    {
        public Roles Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
