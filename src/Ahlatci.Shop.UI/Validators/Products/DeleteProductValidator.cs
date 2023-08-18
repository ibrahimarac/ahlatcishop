using Ahlatci.Shop.UI.Models.RequestModels.Products;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.Products
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductVM>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Silinecek ürünün kimlik bilgisi boş olamaz.")
                .GreaterThan(0).WithMessage("Silinecek ürünün kimlik bilgisi sıfırdan büyük bir sayı olmalıdır.");
        }
    }
}

