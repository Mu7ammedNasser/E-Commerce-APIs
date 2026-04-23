using FluentValidation;

namespace ECommerce.BLL
{
    public class ImageUploadDtoValidators : AbstractValidator<ImageUploadDto>
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        public ImageUploadDtoValidators()
        {
            RuleFor(i => i.File)
                .NotNull()
                .WithMessage("Image file is required.")
                .WithErrorCode("Err-001");

            When(i => i.File != null, () =>
            {
                RuleFor(i => i.File.Length)
                    .LessThanOrEqualTo(5 * 1024 * 1024) // 5MB
                    .WithMessage("Image file size must be less than or equal to 5MB.")
                    .WithErrorCode("Err-002")
                    .WithName("FileSize");

                RuleFor(i => i.File.Length)
                    .GreaterThan(0) // 5MB
                    .WithMessage("Image file size must be greater than 0.")
                    .WithErrorCode("Err-003")
                    .WithName("FileSize");

                RuleFor(i => Path.GetExtension(i.File.FileName).ToLower())
                     .Must(ext => AllowedExtensions.Contains(ext))
                     .WithMessage("Unsupported file Extension")
                     .WithErrorCode("ERR-01")
                     .WithName("FileExtension");
            });

        }
    }
}
