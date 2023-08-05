using Ahlatci.Shop.Application.Models.RequestModels.Accounts;
using FluentValidation;

namespace Ahlatci.Shop.Application.Validators.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
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

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Eposta bilgisi boş olamaz.")
                .MaximumLength(150).WithMessage("Eposta bilgisi 150 karakterden büyük olamaz.")
                .EmailAddress().WithMessage("Geçerli bir eposta adresi girmediniz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon no bilgisi boş olamaz.")
                .MaximumLength(13).WithMessage("Telefon no bilgisi 13 karakterden büyük olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Eposta bilgisi boş olamaz.")
                .MaximumLength(150).WithMessage("Eposta bilgisi 150 karakterden büyük olamaz.")
                .EmailAddress().WithMessage("Geçerli bir eposta adresi girmediniz.");

            RuleFor(x => x.Birtdate)
                .NotEmpty().WithMessage("Doğum tarihi bilgisi boş olamaz.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet bilgisi boş olamaz.")
                .IsInEnum().WithMessage("Cinsiyet bilgisi geçerli değil. (1 veya 2 olabilir.)");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(10).WithMessage("Kullanıcı adı en fazla 10 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MaximumLength(10).WithMessage("Parola en fazla 10 karakter olabilir.");

            RuleFor(x => x.PasswordAgain)
                .NotEmpty().WithMessage("Parola tekrar bilgisi boş olamaz.")
                .MaximumLength(10).WithMessage("Parola tekrar bilgisi 10 karakter olabilir.");

            RuleFor(x => x.Password)
                .Matches(x => x.PasswordAgain).WithMessage("Parola ve parola tekrar bilgisi eşleşmiyor.");

        }
    }
}

