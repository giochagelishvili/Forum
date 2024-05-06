using Forum.Domain.Images;

namespace Forum.Application.Images.Interfaces
{
    public interface IImageRepository
    {
        Task<Image?> GetAsync(int userId, CancellationToken cancellationToken);
        Task CreateAsync(Image image, CancellationToken cancellationToken);
        Task UpdateAsync(Image image, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken);
        Task DeleteAsync(int userId, CancellationToken cancellationToken);
    }
}
