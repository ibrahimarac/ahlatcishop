using System.ComponentModel.DataAnnotations;

namespace Ahlatci.Shop.UI.Models.RequestModels
{
    public class CreateCategoryVM
    {
        [Display(Name = "Kategori adı")]
        public string CategoryName { get; set; }
    }
}
