﻿using Forum.Application.Comments.Responses;
using Forum.Application.Users.Responses;
using Forum.Domain;

namespace Forum.Application.Topics.Responses.Default
{
    public class TopicResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public UserResponseModel Author { get; set; } = default!;
        public Status Status { get; set; }
        public List<CommentResponseModel> Comments { get; set; } = default!;
    }
}