using Ahlatci.Shop.Application.Models.RequestModels.Cities;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.Cities
{
    public class CreateCityValidator : AbstractValidator<CreateCityVM>
    {
        public CreateCityValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Şehir adı boş olamaz.")
                .MaximumLength(20).WithMessage("Şehir adı 20 karakterden büyük olamaz.");

        }
    }
}
