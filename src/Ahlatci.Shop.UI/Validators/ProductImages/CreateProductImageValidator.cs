using Ahlatci.Shop.UI.Models.RequestModels.ProductImages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.UI.Validators.ProductImages
{
    public class CreateProductImageValidator : AbstractValidator<CreateProductImageVM>
    {
        public CreateProductImageValidator()
        {
            var allowedContentTypes = new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif", "image/tiff" };

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün kimlik bilgisi boş olamaz.");

            RuleFor(x => x.UploadedImage)
                .NotNull().WithMessage("Resim dosyası seçilmelidir.")
                .Must(x => x.Length < 1 * 1024 * 1024).WithMessage("Dosya boyutu 1 MB'dan büyük olamaz.")
                .Must(x => allowedContentTypes.Contains(x.ContentType)).WithMessage("Sadece resim dosyası seçilebilir.");
        }
    }
}
