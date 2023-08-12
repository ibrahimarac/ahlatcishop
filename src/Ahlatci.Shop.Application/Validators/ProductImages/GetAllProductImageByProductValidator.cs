using Ahlatci.Shop.Application.Models.RequestModels.ProductImages;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.ProductImages
{
    public class GetAllProductImageByProductValidator : AbstractValidator<GetAllProductImageByProductVM>
    {
        public GetAllProductImageByProductValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("Ürüne ait kimlik bilgisi boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Ürüne ait kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
