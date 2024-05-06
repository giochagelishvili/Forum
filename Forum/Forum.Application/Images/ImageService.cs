using Forum.Application.Exceptions;
using Forum.Application.Images.Interfaces;
using Forum.Application.Users.Interfaces.Services;
using Forum.Domain.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Images
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _config;

        public ImageService(IImageRepository imageRepository, IConfiguration config)
        {
            _imageRepository = imageRepository;
            _config = config;
        }

        public async Task<string> GetAsync(int userId, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetAsync(userId, cancellationToken);

            if (image == null)
                throw new ImageNotFoundException();

            return image.Url;
        }

        public async Task UploadAsync(IFormFile image, int userId, CancellationToken cancellationToken)
        {
            var url = await SaveImage(image, cancellationToken);

            var entity = new Image
            {
                UserId = userId,
                Url = url
            };

            if (await _imageRepository.ExistsAsync(userId, cancellationToken))
                await _imageRepository.UpdateAsync(entity, cancellationToken);
            else
                await _imageRepository.CreateAsync(entity, cancellationToken);
        }

        private async Task<string> SaveImage(IFormFile image, CancellationToken cancellationToken)
        {
            var uploadPath = _config.GetValue<string>("Constants:UploadPath");
            var requestPath = _config.GetValue<string>("Constants:RequestPath");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var ext = Path.GetExtension(image.FileName).ToLower();
            var validExtensions = new string[] { ".jpg", ".png", ".jpeg", ".webp", ".ico" };

            if (!validExtensions.Contains(ext))
                throw new InvalidExtensionException();

            var guid = Guid.NewGuid().ToString();
            var imageName = guid + ext;
            var imagePath = Path.Combine(uploadPath, imageName);

            using var stream = new FileStream(imagePath, FileMode.Create);

            await image.CopyToAsync(stream, cancellationToken);
            await stream.FlushAsync(cancellationToken);

            return $"{requestPath}/{imageName}";
        }

        public async Task DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetAsync(userId, cancellationToken);

            var uploadPath = _config.GetValue<string>("Constants:UploadPath");
            var requestPath = _config.GetValue<string>("Constants:RequestPath");

            var imageName = image.Url.Substring(requestPath.Length);
            var imagePath = uploadPath + imageName;

            if (File.Exists(imagePath))
                File.Delete(imagePath);
            else
                throw new ImageNotFoundException();

            await _imageRepository.DeleteAsync(userId, cancellationToken);
        }
    }
}
