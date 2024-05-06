using Microsoft.Extensions.Configuration;
using Forum.Application.Topics;
using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Application.Users.Interfaces.Services;
using Moq;
using Forum.Application.Topics.Responses.Default;
using Forum.Domain.Topics;
using FluentAssertions;
using Forum.Application.Exceptions;
using Forum.Shared.Localizations;
using Forum.Domain.Comments;
using Forum.Application.Topics.Requests;

namespace Forum.Tests.Unit
{
    public class TopicServiceTests
    {
        private readonly TopicService _sut;
        private readonly Mock<ITopicRepository> _topicRepositoryMock = new();
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly Mock<IConfiguration> _configMock = new();

        public TopicServiceTests()
        {
            _sut = new TopicService(_topicRepositoryMock.Object, _userServiceMock.Object, _configMock.Object);
        }

        //GetAllTopicsAsync
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public async Task GetAllTopicsAsync_WhenPageIsLessThanOrEqualToZero_ShouldThrowPageNotFoundException(int page)
        {
            //Act
            var result = async () => await _sut.GetAllTopicsAsync(page, It.IsAny<int>(), It.IsAny<CancellationToken>());

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllTopicsAsync
        [Theory]
        [InlineData(2)]
        public async Task GetAllTopicsAsync_WhenPageIsGreaterThanOneAndResultIsZero_ShouldThrowPageNotFoundException(int page)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = async () => await _sut.GetAllTopicsAsync(page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllTopicsAsync
        [Theory]
        [InlineData(1)]
        public async Task GetAllTopicsAsync_WhenPageIsEqualToOneAndResultIsZero_ShouldNotThrowException(int page)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = await _sut.GetAllTopicsAsync(page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllTopicsAsync
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        public async Task GetAllTopicsAsync_WhenPageIsValidAndResultIsNotZero_ShouldHaveCountOfItemsPerPage(int page, int itemsPerPage)
        {
            // Stage
            var topics = new List<TopicCommentsCount>();

            for (int i = 1; i <= itemsPerPage; i++)
            {
                topics.Add(new TopicCommentsCount
                {
                    Topic = new Topic(),
                    CommentCount = i
                });
            }

            _topicRepositoryMock.Setup(x => x.GetAllTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(topics);

            //Act
            var result = await _sut.GetAllTopicsAsync(page, itemsPerPage, CancellationToken.None);

            //Assert
            Assert.True(result.Count() == itemsPerPage);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllArchivedTopicsAsync
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public async Task GetAllArchivedTopicsAsync_WhenPageIsLessThanOrEqualToZero_ShouldThrowPageNotFoundException(int page)
        {
            //Act
            var result = async () => await _sut.GetAllArchivedTopicsAsync(page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllArchivedTopicsAsync
        [Theory]
        [InlineData(2)]
        public async Task GetAllArchivedTopicsAsync_WhenPageIsGreaterThanOneAndResultIsZero_ShouldThrowPageNotFoundException(int page)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllArchivedTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = async () => await _sut.GetAllArchivedTopicsAsync(page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllArchivedTopicsAsync
        [Theory]
        [InlineData(1)]
        public async Task GetAllArchivedTopicsAsync_WhenPageIsEqualToOneAndResultIsZero_ShouldNotThrowException(int page)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllArchivedTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = await _sut.GetAllArchivedTopicsAsync(page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllArchivedTopicsAsync
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 2)]
        public async Task GetAllArchivedTopicsAsync_WhenPageIsValidAndResultIsNotZero_ShouldHaveCountOfItemsPerPage(int page, int itemsPerPage)
        {
            var topics = new List<TopicCommentsCount>();

            for (int i = 1; i <= itemsPerPage; i++)
            {
                topics.Add(new TopicCommentsCount
                {
                    Topic = new Topic(),
                    CommentCount = i
                });
            }

            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllArchivedTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(topics);

            //Act
            var result = await _sut.GetAllArchivedTopicsAsync(page, itemsPerPage, CancellationToken.None);

            //Assert
            Assert.True(result.Count() == itemsPerPage);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllUserTopicsAsync
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public async Task GetAllUserTopicsAsync_WhenPageIsLessThanOrEqualToZero_ShouldThrowPageNotFoundException(int page)
        {
            //Act
            var result = async () => await _sut.GetAllUserTopicsAsync(It.IsAny<int>(), page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllUserTopicsAsync
        [Theory]
        [InlineData(2)]
        public async Task GetAllUserTopicsAsync_WhenPageIsGreaterThanOneAndResultIsZero_ShouldThrowPageNotFoundException(int page)
        {
            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _topicRepositoryMock.Setup(x => x.GetAllUserTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = async () => await _sut.GetAllUserTopicsAsync(It.IsAny<int>(), page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllUserTopicsAsync
        [Theory]
        [InlineData(1)]
        public async Task GetAllUserTopicsAsync_WhenPageIsEqualToOneAndResultIsZero_ShouldNotThrowException(int page)
        {
            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _topicRepositoryMock.Setup(x => x.GetAllUserTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = await _sut.GetAllUserTopicsAsync(It.IsAny<int>(), page, It.IsAny<int>(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllUserTopicsAsync
        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 2)]
        public async Task GetAllUserTopicsAsync_WhenPageIsValidAndResultIsNotZero_ShouldHaveCountOfItemsPerPage(int page, int itemsPerPage)
        {
            var topics = new List<TopicCommentsCount>();

            for (int i = 1; i <= itemsPerPage; i++)
            {
                topics.Add(new TopicCommentsCount
                {
                    Topic = new Topic(),
                    CommentCount = i
                });
            }

            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _topicRepositoryMock.Setup(x => x.GetAllUserTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(topics);

            //Act
            var result = await _sut.GetAllUserTopicsAsync(It.IsAny<int>(), page, itemsPerPage, CancellationToken.None);

            //Assert
            Assert.True(result.Count() == itemsPerPage);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.IsType<List<TopicResponseNewsFeedModel>>(result);
        }

        //GetAllUserTopicsAsync
        [Fact]
        public async Task GetAllUserTopicsAsync_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
        {
            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            _topicRepositoryMock.Setup(x => x.GetAllUserTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicCommentsCount>());

            //Act
            var result = async () => await _sut.GetAllUserTopicsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None);

            //Assert
            await result.Should().ThrowAsync<PageNotFoundException>().WithMessage(ErrorMessages.PageNotFound);
        }

        //GetAllArchiveWorkerTopicsAsync
        [Fact]
        public async Task GetAllArchiveWorkerTopicsAsync_WhenRepositoryReturnsTopics_ShouldNotBeEmptyList()
        {
            // Stage

            var topics = new List<TopicWithLatestComment>();

            for (int i = 1; i <= 5; i++)
            {
                topics.Add(new TopicWithLatestComment
                {
                    TopicId = i,
                    ModifiedAt = DateTime.UtcNow.AddDays(i),
                    LatestComment = new Comment()
                });
            }

            _topicRepositoryMock.Setup(x => x.GetAllArchiveWorkerTopicsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(topics);

            //Act
            var result = await _sut.GetAllArchiveWorkerTopicsAsync(CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count() > 0);
        }

        //GetAllArchiveWorkerTopicsAsync
        [Fact]
        public async Task GetAllArchiveWorkerTopicsAsync_WhenRepositoryDoesNotReturnTopics_ShouldBeEmptyList()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetAllArchiveWorkerTopicsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<TopicWithLatestComment>());

            //Act
            var result = await _sut.GetAllArchiveWorkerTopicsAsync(CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        //GetAllArchiveWorkerTopicsAsync
        [Fact]
        public async Task GetAllArchiveWorkerTopicsAsync_WhenCancellationIsRequested_CancelsExecution()
        {
            // Stage
            var cancellationToken = new CancellationToken(true);

            _topicRepositoryMock.Setup(x => x.GetAllArchiveWorkerTopicsAsync(cancellationToken)).ThrowsAsync(new OperationCanceledException());

            // Act
            var result = async () => await _sut.GetAllArchiveWorkerTopicsAsync(cancellationToken);

            // Assert
            await result.Should().ThrowAsync<OperationCanceledException>();
        }

        //GetTopicAsync
        [Fact]
        public async Task GetTopicAsync_WhenTopicDoesNotExist_ThrowsTopicNotFoundException()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetTopicAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync((Topic)null);

            // Act
            var result = async () => await _sut.GetTopicAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            await result.Should().ThrowAsync<TopicNotFoundException>();
        }

        //GetTopicAsync
        [Fact]
        public async Task GetTopicAsync_WhenTopicExists_ResultShouldNotBeNull()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetTopicAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Topic());

            // Act
            var result = await _sut.GetTopicAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TopicResponseModel>(result);
        }

        //CreateTopicAsync
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CreateTopicAsync_WhenUserDoesNotHaveEnoughComments_ShouldThrowNotEnoughCommentsException(int commentCountResult)
        {
            // Stage
            _configMock.Setup(x => x["Constants:MinimumCommentsRequired"]).Returns("3");

            _userServiceMock.Setup(x => x.GetUserCommentCountAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(commentCountResult);

            var postModel = new TopicRequestPostModel { AuthorId = It.IsAny<int>() };

            // Act
            var result = async () => await _sut.CreateTopicAsync(postModel, It.IsAny<CancellationToken>());

            // Assert
            await result.Should().ThrowAsync<NotEnoughCommentsException>();
        }

        //CreateTopicAsync
        [Fact]
        public async Task CreateTopicAsync_WhenUserHasEnoughComments_ShouldNotThrowException()
        {
            // Stage
            _configMock.Setup(x => x["Constants:MinimumCommentsRequired"]).Returns("3");

            _userServiceMock.Setup(x => x.GetUserCommentCountAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);

            var postModel = new TopicRequestPostModel { AuthorId = It.IsAny<int>() };

            // Act
            var result = async () => await _sut.CreateTopicAsync(postModel, It.IsAny<CancellationToken>());

            // Assert
            await result.Should().NotThrowAsync<Exception>();
        }

        //GetTopicsCountAsync
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public async Task GetTopicsCountAsync_ShouldNotThrowException(int topicCount)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetTopicsCountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(topicCount);

            // Act
            var result = async () => await _sut.GetTopicsCountAsync(It.IsAny<CancellationToken>());

            // Assert
            await result.Should().NotThrowAsync<Exception>();
        }

        //GetArchivedTopicsCountAsync
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public async Task GetArchivedTopicsCountAsync_ShouldNotThrowException(int topicCount)
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.GetArchivedTopicsCountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(topicCount);

            // Act
            var result = async () => await _sut.GetArchivedTopicsCountAsync(It.IsAny<CancellationToken>());

            // Assert
            await result.Should().NotThrowAsync<Exception>();
        }

        //GetUserTopicsCountAsync
        [Fact]
        public async Task GetUserTopicsCountAsyncCountAsync_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
        {
            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = async () => await _sut.GetUserTopicsCountAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            await result.Should().ThrowAsync<UserNotFoundException>().WithMessage(ErrorMessages.UserNotFound);
        }

        //GetUserTopicsCountAsync
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public async Task GetUserTopicsCountAsyncCountAsync_WhenUserExists_ShouldNotThrowException(int topicCount)
        {
            // Stage
            _userServiceMock.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _topicRepositoryMock.Setup(x => x.GetUserTopicsCountAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(topicCount);

            // Act
            var result = async () => await _sut.GetUserTopicsCountAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            await result.Should().NotThrowAsync<Exception>();
        }

        //ExistsAsync
        [Fact]
        public async Task ExistsAsync_WhenTopicExists_ShouldReturnTrue()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _sut.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result);
        }

        //ExistsAsync
        [Fact]
        public async Task ExistsAsync_WhenTopicDoesNotExist_ShouldReturnFalse()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // Act
            var result = await _sut.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result);
        }

        //IsActiveAsync
        [Fact]
        public async Task ExistsAsync_WhenTopicIsActive_ShouldReturnTrue()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.IsActiveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _sut.IsActiveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result);
        }

        //IsActiveAsync
        [Fact]
        public async Task ExistsAsync_WhenTopicIsInactive_ShouldReturnFalse()
        {
            // Stage
            _topicRepositoryMock.Setup(x => x.IsActiveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // Act
            var result = await _sut.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result);
        }
    }
}
