using System.ComponentModel.DataAnnotations;

namespace Ahlatci.Shop.UI.Models.RequestModels.Products
{
    public class CreateProductVM
    {
        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }

        [Display(Name="Ürün adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Detail { get; set; }

        [Display(Name = "Stok adedi")]
        public int? UnitInStock { get; set; }

        [Display(Name = "Birim fiyatı")]
        public decimal? UnitPrice { get; set; }
    }
}


