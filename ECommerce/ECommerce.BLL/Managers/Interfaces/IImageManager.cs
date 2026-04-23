using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface IImageManager
    {
        Task<GeneralResult<ImageUploadResultDto>> UploadImage(ImageUploadDto imageUploadDto, string basePath, string? schema, string? host);
    }
}
