using System.ComponentModel.DataAnnotations;

namespace Ahlatci.Shop.UI.Models.RequestModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }

        [Display(Name="Kategori adı")]
        public string CategoryName { get; set; }
    }
}
