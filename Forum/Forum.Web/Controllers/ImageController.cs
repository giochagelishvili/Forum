using Forum.Application.Images.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile image, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _imageService.UploadAsync(image, userId, cancellationToken);

            return RedirectToAction("Profile", "Profile", new { id = userId });
        }

        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _imageService.DeleteAsync(userId, cancellationToken);

            return RedirectToAction("Profile", "Profile", new { id = userId });
        }
    }
}
