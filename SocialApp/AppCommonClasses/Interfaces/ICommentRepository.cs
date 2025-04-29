using AppCommonClasses.Models;
using System.Collections.Generic;

namespace AppCommonClasses.Interfaces
{
    public interface ICommentRepository
    {
        void DeleteCommentById(long id);

        List<Comment> GetAllComments();

        Comment? GetCommentById(long id);

        List<Comment> GetCommentsByPostId(long postId);

        void SaveComment(Comment entity);

        void UpdateCommentContentById(long id, string content);
    }
}