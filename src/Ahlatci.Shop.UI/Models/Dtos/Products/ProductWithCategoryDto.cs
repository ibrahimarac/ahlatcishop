using System.ComponentModel.DataAnnotations;

namespace Ahlatci.Shop.UI.Models.Dtos.Products
{
    public class ProductWithCategoryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Display(Name="Ürün adı")]
        public string Name { get; set; }
        public string Detail { get; set; }

        [Display(Name="Stok adedi")]
        public int UnitInStock { get; set; }

        [Display(Name="Birim fiyatı")]
        public decimal UnitPrice { get; set; }

        [Display(Name="Kategorisi")]
        public CategoryDto Category { get; set; }
    }
}
