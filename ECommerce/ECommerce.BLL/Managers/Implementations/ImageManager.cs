using ECommerce.Common;
using FluentValidation;

namespace ECommerce.BLL
{
    public class ImageManager : IImageManager
    {
        private readonly IValidator<ImageUploadDto> _validator;

        public ImageManager(IValidator<ImageUploadDto> validator)
        {
            _validator = validator;
        }
        public async Task<GeneralResult<ImageUploadResultDto>> UploadImage(
            ImageUploadDto imageUploadDto, 
            string basePath,
            string? schema,
            string? host)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                return GeneralResult<ImageUploadResultDto>.Failure("Base path is required to save the image.");
            }

            if(string.IsNullOrEmpty(schema) || string.IsNullOrEmpty(host))
            {
                return GeneralResult<ImageUploadResultDto>.Failure("Schema and host are required to generate the image URL.");
            }

            var result = await _validator.ValidateAsync(imageUploadDto);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return GeneralResult<ImageUploadResultDto>.Failure(string.Join(", ", errors));
            }

            var file = imageUploadDto.File;
            var extension = Path.GetExtension(file.FileName).ToLower();
            var cleanName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-");
            var newFileName = $"{cleanName}-{Guid.NewGuid()}{extension}";
            //var folderName = Path.Combine("wwwroot", "images", newFileName);
            var directoryPath = Path.Combine(basePath,"Files"); // updated later
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fullFilePath = Path.Combine(directoryPath, newFileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{schema}://{host}/Files/{newFileName}";

            var imageUploadResult = new ImageUploadResultDto(url);

            return GeneralResult<ImageUploadResultDto>.Success(imageUploadResult);

        }

    }
}
