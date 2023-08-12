using Ahlatci.Shop.Application.Models.RequestModels.Products;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.Products
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

