using Ahlatci.Shop.UI.Models.RequestModels.Products;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.Products
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdVM>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Ürünün kimlik bilgisi boş olamaz.")
                .GreaterThan(0).WithMessage("Ürünün kimlik bilgisi sıfırdan büyük bir sayı olmalıdır.");
        }
    }
}

