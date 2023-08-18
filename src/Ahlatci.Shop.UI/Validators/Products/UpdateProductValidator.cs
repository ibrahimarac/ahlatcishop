using Ahlatci.Shop.UI.Models.RequestModels.Products;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.Products
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductVM>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Ürüne kimlik bilgisi boş olamaz.")
                .GreaterThan(0).WithMessage("Ürüne kimlik bilgisi sıfırdan büyük bir sayı olmalıdır.");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Ürüne ait kategori bilgisi boş olamaz.")
                .GreaterThan(0).WithMessage("Kategori bilgisi sıfırdan büyük bir sayı olmalıdır.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Ürüne adı boş olamaz.")
                .MaximumLength(255).WithMessage("Ürün adı en fazla 255 karakter olabilir.");

            RuleFor(x => x.Detail)
                .NotNull().WithMessage("Ürüne detay bilgisi boş olamaz.");

            RuleFor(x => x.UnitInStock)
                .NotNull().WithMessage("Ürün stok adedi boş olamaz.");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("Ürün fiyatı boş olamaz.");
        }
    }
}

