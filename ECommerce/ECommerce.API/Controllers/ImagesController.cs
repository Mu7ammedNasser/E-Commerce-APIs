using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImagesController(IImageManager imageManager, IWebHostEnvironment webHostEnvironment)
        {
            _imageManager = imageManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDto>>> UploadImageAsync([FromForm] ImageUploadDto imageUploadDto)
        {
            var basePath = _webHostEnvironment.WebRootPath ?? _webHostEnvironment.ContentRootPath;
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var result = await _imageManager.UploadImage(imageUploadDto, basePath, schema, host);
            if (!result.IsSuccess)
            {
                return GeneralResult<ImageUploadResultDto>.Failure(result.Message);
            }
            return GeneralResult<ImageUploadResultDto>.Success(result.Data!, result.Message);
        }
    }
}
