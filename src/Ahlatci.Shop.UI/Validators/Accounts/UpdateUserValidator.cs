using Ahlatci.Shop.UI.Models.RequestModels.Accounts;
using FluentValidation;

namespace Ahlatci.Shop.UI.Validators.Accounts
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserVM>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek kullanıcı kimlik numarası gönderilmelidir.");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("Geçerli bir il bilgisi gönderilmelidir.")
                .LessThan(82).WithMessage("Geçersiz bir il numarası gönderildi.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Tc kimlik boş olamaz.")
                .MaximumLength(11).WithMessage("Tc kimlik numarası 11 karakterden büyük olamaz.")
                .MinimumLength(11).WithMessage("Tc kimlik numarası 11 karakterden küçük olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Ad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Soyad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon no bilgisi boş olamaz.")
                .MaximumLength(13).WithMessage("Telefon no bilgisi 13 karakterden büyük olamaz.");

            RuleFor(x => x.Birtdate)
                .NotEmpty().WithMessage("Doğum tarihi bilgisi boş olamaz.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet bilgisi boş olamaz.")
                .IsInEnum().WithMessage("Cinsiyet bilgisi geçerli değil. (1 veya 2 olabilir.)");

        }
    }
}

