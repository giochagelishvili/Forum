using Forum.API.Controllers.V1.Admin;
using Forum.Application.Images.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class ImageController : CustomControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _imageService.GetAsync(userId, cancellationToken);
        }

        [HttpPost]
        public async Task Upload(IFormFile image, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _imageService.UploadAsync(image, userId, cancellationToken);
        }

        [HttpDelete]
        public async Task Delete(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _imageService.DeleteAsync(userId, cancellationToken);
        }
    }
}
