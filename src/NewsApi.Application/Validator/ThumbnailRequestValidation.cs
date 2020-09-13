using System.IO;
using System.Linq;
using FluentValidation;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Validator
{
    public class ThumbnailRequestValidation : AbstractValidator<ThumbnailRequest>
    {
        public ThumbnailRequestValidation()
        {
            RuleFor(x => x).NotNull();

            RuleFor(x => x.NewsId)
                .SetValidator(new GuidValidator());

            RuleFor(x => x.FileLength)
                .NotEmpty().NotEmpty()
                .Must(IsTooLarge)
                .WithMessage("Thumbnail image Too Large (max is 5MB)");

            RuleFor(x => x.FileName)
                .NotNull().NotEmpty()
                .Must(CheckExtensionSupport)
                .WithMessage("Thumbnail image with unsupported extension, use '.jpg', '.png' or '.jpeg')");

            RuleFor(x => x.FileLocalPath)
                .NotEmpty().NotEmpty()
                .Must(File.Exists)
                .WithMessage("The local file was not found");

        }

        private bool IsTooLarge(long length) => length < 5 * 1024 * 1024;

        private bool CheckExtensionSupport(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            var extension = Path.GetExtension(fileName);
            return allowedExtensions.Contains(extension.ToLower());
        }
    }
}