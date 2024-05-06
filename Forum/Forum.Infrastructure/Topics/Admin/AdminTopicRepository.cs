using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Application.Topics.Requests;
using Forum.Domain;
using Forum.Domain.Topics;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Topics.Admin
{
    public class AdminTopicRepository : BaseRepository<Topic>, IAdminTopicRepository
    {
        public AdminTopicRepository(ForumContext context) : base(context)
        {
        }

        public async Task<List<TopicCommentsCount>> GetAllTopicsAsync(int itemsToSkip, int itemsToTake, CancellationToken cancellationToken)
        {
            return await _dbSet.OrderByDescending(topic => topic.CreatedAt)
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
            return await _dbSet.Where(topic => topic.Status == Status.Inactive)
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
            return await _dbSet.Where(topic => topic.AuthorId == userId)
                .OrderByDescending(topic => topic.CreatedAt)
                .Skip(itemsToSkip).Take(itemsToTake)
                .Include(topic => topic.Author)
                .Select(topic => new TopicCommentsCount
                {
                    Topic = topic,
                    CommentCount = topic.Comments.Count()
                }).ToListAsync(cancellationToken);
        }

        public async Task<Topic?> GetTopicAsync(int topicId, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(topic => topic.Author)
                .Include(topic => topic.Comments)
                .ThenInclude(comment => comment.Author)
                .FirstOrDefaultAsync(topic => topic.Id == topicId, cancellationToken);
        }

        public async Task CreateTopicAsync(Topic topic, CancellationToken cancellationToken)
        {
            topic.State = State.Show;

            await _dbSet.AddAsync(topic, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStateAsync(TopicStatePutModel putModel, CancellationToken cancellationToken)
        {
            var topic = await _dbSet.FirstOrDefaultAsync(topic => topic.Id == putModel.Id, cancellationToken);

            topic.State = putModel.State;

            _dbSet.Update(topic);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStatusAsync(TopicStatusPutModel putModel, CancellationToken cancellationToken)
        {
            var topic = await _dbSet.FirstOrDefaultAsync(topic => topic.Id == putModel.Id, cancellationToken);

            topic.Status = putModel.Status;

            _dbSet.Update(topic);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int topicId, CancellationToken cancellationToken)
        {
            return await AnyAsync(topic => topic.Id == topicId, cancellationToken);
        }

        public async Task<int> GetTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.CountAsync(cancellationToken);
        }

        public async Task<int> GetArchivedTopicsCountAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.Status == Status.Inactive).CountAsync(cancellationToken);
        }

        public async Task<int> GetUserTopicsCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(topic => topic.AuthorId == userId).CountAsync(cancellationToken);
        }
    }
}
