using Forum.Application.Users.Responses;

namespace Forum.Application.Comments.Responses
{
    public class CommentResponseModel
    {
        public int Id { get; set; }
        public string Body { get; set; } = default!;
        public UserResponseModel Author { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
