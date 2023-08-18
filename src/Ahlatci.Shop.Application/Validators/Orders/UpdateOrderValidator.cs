using Ahlatci.Shop.Application.Models.RequestModels.Orders;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.Orders
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderVM>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Sipariş numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Sipariş numarası sıfırdan büyük bir sayı olmalıdır.");

            RuleFor(x => x.AddressId)
                .NotEmpty().WithMessage("Adres kimlik numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Adres kimlik numarası sıfırdan büyük bir sayı olmalıdır.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("Sipariş durumu boş olamaz.")
                .IsInEnum().WithMessage("Sipariş durumu geçerli bir değer değildir.");
        }
    }
}
