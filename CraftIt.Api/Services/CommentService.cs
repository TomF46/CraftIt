using System;
using System.Collections.Generic;
using System.Linq;
using CraftIt.Api.Helpers;
using CraftIt.Api.Models;
using Microsoft.AspNetCore.Http;

namespace CraftIt.Api.Services
{
    /// <summary>Class <c>CommentService</c>Class used to implement methods in the ICommentService interface</summary>
    public class CommentService : ICommentService
    {
        private CraftItContext _context;

        public CommentService(CraftItContext context)
        {
            _context = context;
        }

        public Comment GetComment(int id){
            var comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if(comment == null) throw new AppException("Comment not found");

            return comment;
        }

        public void AddComment(CommentCreationDto comment, User user)
        {

            if(user == null) throw new AppException("User not found");

            var product = _context.Products.FirstOrDefault(x => x.Id == comment.ProductId);

            if(product == null) throw new AppException("Product not found");

            var commentToAdd = new Comment{
                User = user,
                Product = product,
                Message = comment.Message,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(commentToAdd);
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if(comment == null) throw new AppException("Comment not found");

            _context.Comments.Remove(comment);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateComment(CommentUpdateDto comment)
        {
            var commentToUpdate = _context.Comments.FirstOrDefault(x => x.Id == comment.CommentId);

            if(commentToUpdate == null) throw new AppException("Comment not found");

            commentToUpdate.Message = comment.Message;
            Save();
        }
    }
}