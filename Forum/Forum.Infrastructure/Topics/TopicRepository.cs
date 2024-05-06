using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Domain;
using Forum.Domain.Topics;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Topics
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(ForumContext context) : base(context)
        {
        }

        public async Task<List<TopicCommentsCount>> GetAllTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show)
                .OrderByDescending(topic => topic.CreatedAt)
                .Skip(itemsToSkip).Take(itemsToTake)
                .Include(topic => topic.Author)
                .Select(topic => new TopicCommentsCount
                {
                    Topic = topic,
                    CommentCount = topic.Comments.Count()
                }).ToListAsync(cancellationToken);
        }

        public async Task<List<TopicCommentsCount>> GetAllArchivedTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show && topic.Status == Status.Inactive)
                .OrderByDescending(topic => topic.CreatedAt)
                .Skip(itemsToSkip).Take(itemsToTake)
                .Include(topic => topic.Author)
                .Select(topic => new TopicCommentsCount
                {
                    Topic = topic,
                    CommentCount = topic.Comments.Count()
                }).ToListAsync(cancellationToken);
        }

        public async Task<List<TopicCommentsCount>> GetAllUserTopicsAsync(int userId, int itemsToSkip, int itemsToTake, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show && topic.AuthorId == userId)
                .OrderByDescending(topic => topic.CreatedAt)
                .Skip(itemsToSkip).Take(itemsToTake)
                .Include(topic => topic.Author)
                .Select(topic => new TopicCommentsCount
                {
                    Topic = topic,
                    CommentCount = topic.Comments.Count()
                }).ToListAsync(cancellationToken);
        }

        public async Task<List<TopicWithLatestComment>> GetAllArchiveWorkerTopicsAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Where(topic => topic.State != State.Pending && topic.Status == Status.Active)
                .Select(topic => new TopicWithLatestComment
                {
                    TopicId = topic.Id,
                    ModifiedAt = topic.ModifiedAt,
                    LatestComment = topic.Comments.OrderByDescending(comment => comment.CreatedAt).FirstOrDefault()
                }).ToListAsync(cancellationToken);
        }
        public async Task<Topic?> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(topic => topic.Author)
                .Include(topic => topic.Comments)
                .ThenInclude(comment => comment.Author)
                .FirstOrDefaultAsync(topic => topic.Id == topicId && topic.State == State.Show, cancellationToken);
        }

        public async Task<int> GetTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show)
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show && topic.Status == Status.Inactive)
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.State == State.Show && topic.AuthorId == userId)
                .CountAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken)
        {
            return await AnyAsync(topic => topic.Id == topicId && topic.State == State.Show, cancellationToken);
        }

        public async Task<bool> IsActiveAsync(int topicId, CancellationToken cancellationToken)
        {
            return await AnyAsync(topic => topic.Id == topicId && topic.State == State.Show && topic.Status == Status.Active, cancellationToken);
        }
    }
}
