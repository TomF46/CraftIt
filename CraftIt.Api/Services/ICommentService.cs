using System.Collections.Generic;
using CraftIt.Api.Models;
namespace CraftIt.Api.Services
{
    public interface ICommentService
    {
        void AddComment(CommentCreationDto comment);
        void DeleteComment(int id);
        void UpdateComment(CommentUpdateDto comment);
        bool Save();
    }
}