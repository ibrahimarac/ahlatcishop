using Ahlatci.Shop.UI.Models.RequestModels.ProductImages;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.ProductImages
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
