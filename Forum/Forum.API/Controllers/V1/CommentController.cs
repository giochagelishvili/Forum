using Asp.Versioning;
using Forum.API.Controllers.V1.Admin;
using Forum.API.Infrastructure.Models.Comments;
using Forum.Application.Comments.Interfaces;
using Forum.Application.Comments.Requests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CommentController : CustomControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task Create(CommentRequestPostApiModel comment, CancellationToken cancellationToken)
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = comment.Adapt<CommentRequestPostModel>();

            model.AuthorId = authorId;

            await _commentService.CreateAsync(model, cancellationToken);
        }

        [HttpDelete]
        public async Task Delete(int commentId, CancellationToken cancellationToken)
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _commentService.DeleteAsync(commentId, authorId, cancellationToken);
        }
    }
}
