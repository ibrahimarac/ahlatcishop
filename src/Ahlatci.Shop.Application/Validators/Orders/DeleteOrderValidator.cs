using Ahlatci.Shop.Application.Models.RequestModels.Orders;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.Orders
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderVM>
    {
        public DeleteOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Sipariş numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Sipariş numarası sıfırdan büyük bir sayı olmalıdır.");
        }
    }
}
