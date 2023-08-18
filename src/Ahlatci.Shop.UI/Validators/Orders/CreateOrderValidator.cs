using Ahlatci.Shop.UI.Models.RequestModels.Orders;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderVM>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Müşteri numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Müşteri numarası sıfırdan büyük bir sayı olmalıdır.");

            RuleFor(x => x.AddressId)
                .NotEmpty().WithMessage("Adres kimlik numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Adres kimlik numarası sıfırdan büyük bir sayı olmalıdır.");
        }
    }
}
