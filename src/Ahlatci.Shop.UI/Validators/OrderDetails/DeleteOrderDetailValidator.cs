using Ahlatci.Shop.UI.Models.RequestModels.OrderDetails;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.OrderDetails
{
    public class DeleteOrderDetailValidator : AbstractValidator<DeleteOrderDetailVM>
    {
        public DeleteOrderDetailValidator()
        {
            RuleFor(x => x.OrderDetailId)
                .NotEmpty().WithMessage("Sipariş detay numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Sipariş detay numarası sıfırdan büyük olmalıdır.");
        }
    }
}
