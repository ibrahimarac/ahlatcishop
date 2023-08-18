namespace Ahlatci.Shop.UI.Models.RequestModels.Accounts
{
    public class UpdateUserVM
    {
        public int? Id { get; set; }
        public int CityId { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime Birtdate { get; set; }
        public Gender Gender { get; set; }
    }
}
