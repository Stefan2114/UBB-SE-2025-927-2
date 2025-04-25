

namespace SocialApp.Tests
{

    using System;
    using NSubstitute;
    using NUnit.Framework;
    using AppCommonClasses.Models;
    using SocialApp.Services;
    using AppCommonClasses.Repos;


    /// <summary>
    /// Contains unit tests for the CommentService class.
    /// </summary>
    public class CommentServiceTests
    {
        /// <summary>
        /// Validates that the ValidateAdd method successfully adds a comment when provided with valid arguments.
        /// </summary>
        [Test]
        public void AddComment_WithValidArguments_ReturnsComment()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            string content = "TestContent";
            long userId = 1;
            long postId = 2;

            var user = new User { Id = userId, Username = "TestUser",Email="TestEmail@test.com", PasswordHash = "TestPassword", Image = "TestImage" };
            var post = new Post { Id = postId, Title = "TestPost", Content = "TestContent", CreatedDate = DateTime.Now, UserId = userId, GroupId = 1, Visibility = AppCommonClasses.Enums.PostVisibility.Public, Tag = AppCommonClasses.Enums.PostTag.Misc };

            userRepository.GetById(userId).Returns(user);
            postRepository.GetPostById(postId).Returns(post);

            // Act
            var result = commentService.AddComment(content, userId, postId);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Content, Is.EqualTo(content));
            Assert.That(result.UserId, Is.EqualTo(userId));
            Assert.That(result.PostId, Is.EqualTo(postId));
            commentRepository.Received(1).SaveComment(Arg.Any<Comment>());
        }

        /// <summary>
        /// Validates that the ValidateAdd method throws an exception when the content is empty.
        /// </summary>
        [Test]
        public void AddComment_WithInvalidContent_ThrowsException()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            string emptyContent = "";
            long userId = 1;
            long postId = 2;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => commentService.AddComment(emptyContent, userId, postId));
            Assert.That(ex.Message, Is.EqualTo("Comment content cannot be empty or null. (Parameter 'content')"));
        }

        /// <summary>
        /// Validates that the ValidateAdd method throws an exception when the user does not exist.
        /// </summary>
        [Test]
        public void AddComment_WithInvalidUser_ThrowsException()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            string content = "Test Content";
            long invalidUserId = 999;
            long postId = 2;

            userRepository.GetById(invalidUserId).Returns((User)null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => commentService.AddComment(content, invalidUserId, postId));
            Assert.That(ex.Message, Is.EqualTo($"User with ID {invalidUserId} does not exist."));
            userRepository.Received(1).GetById(invalidUserId);
        }

        /// <summary>
        /// Validates that the ValidateAdd method throws an exception when the post does not exist.
        /// </summary>
        [Test]
        public void AddComment_WithInvalidPost_ThrowsException()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            string content = "Test Content";
            long userId = 1;
            long invalidPostId = 999;

            var user = new User { Id = userId, Username = "TestUser", Email = "testemail", PasswordHash = "TestPassword", Image = "TestImage" };


            userRepository.GetById(userId).Returns(user);
            postRepository.GetPostById(invalidPostId).Returns((Post)null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => commentService.AddComment(content, userId, invalidPostId));
            Assert.That(ex.Message, Is.EqualTo($"Post with ID {invalidPostId} does not exist."));
            postRepository.Received(1).GetPostById(invalidPostId);
        }

        /// <summary>
        /// Validates that the ValidateDelete method successfully deletes a comment when provided with a valid comment ID.
        /// </summary>
        [Test]
        public void DeleteComment_WithValidCommentId()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            long commentId = 1;
            var comment = new Comment { Id = commentId, Content = "Test Content", CreatedDate = DateTime.Now, UserId = 1, PostId = 2 };

            commentRepository.GetCommentById(commentId).Returns(comment);

            // Act
            commentService.DeleteComment(commentId);

            // Assert
            commentRepository.Received(1).DeleteCommentById(commentId);
        }

        /// <summary>
        /// Validates that the ValidateDelete method throws an exception when the comment does not exist.
        /// </summary>
        [Test]
        public void DeleteComment_WithInvalidCommentId_ThrowsException()
        {
            // Arrange
            var commentRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            var commentService = new CommentService(commentRepository, postRepository, userRepository);

            long invalidCommentId = 999;

            commentRepository.GetCommentById(invalidCommentId).Returns((Comment)null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => commentService.DeleteComment(invalidCommentId));
            Assert.That(ex.Message, Is.EqualTo($"Comment with ID {invalidCommentId} does not exist."));
            commentRepository.Received(1).GetCommentById(invalidCommentId);
        }
 
        /// Validates that the UpdateComment method functions correctly when provided with valid arguments.
        /// </summary>
        [Test]
        public void UpdateComment_WithValidArguments()
        {
            // Arange
            var commentsRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            CommentService commentService = new CommentService(commentsRepository,postRepository,userRepository);

            long commentId = 1;
            long userId = 1;
            long postId = 1;
            string? content = "Test Content";

            Comment testComment = new Comment { Id = commentId, UserId=userId, PostId = postId, Content = content, CreatedDate = DateTime.Now };

            commentsRepository.GetCommentById(commentId).Returns(testComment);

            // Act
            commentService.UpdateComment(commentId, content);

            // Assert
            commentsRepository.Received(1).GetCommentById(commentId);
            commentsRepository.Received(1).UpdateCommentContentById(commentId, content);
        }

        /// <summary>
        /// Validates that the UpdateComment function throws an exception when provided with an invalid comment Id.
        /// </summary>
        [Test]
        public void UpdateComment_WithInvalidCommentId_ThrowsException()
        {
            // Arange
            var commentsRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            CommentService commentService = new CommentService(commentsRepository, postRepository, userRepository);

            long commentId = 1;
            string? content = "Test Content";

            commentsRepository.GetCommentById(commentId).Returns((Comment)null);

            // Act & Assert
            Assert.Throws<Exception>(() => commentService.UpdateComment(commentId,content), "Comment does not exist");
            commentsRepository.Received(1).GetCommentById(commentId);
        }

        /// <summary>
        /// Validates that the UpdateComment function throws an exception when the provided content is empty.
        /// </summary>
        [Test]
        public void UpdateComment_WithInvalidContent_ThrowsException()
        {
            // Arange
            var commentsRepository = Substitute.For<ICommentRepository>();
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();

            CommentService commentService = new CommentService(commentsRepository, postRepository, userRepository);

            long commentId = 1;
            long userId = 1;
            long postId = 1;
            string? content = "Test Content";
            string? emptycontent = string.Empty;

            Comment testComment = new Comment { Id = commentId, UserId = userId, PostId = postId, Content = content, CreatedDate = DateTime.Now };

            commentsRepository.GetCommentById(commentId).Returns(testComment);

            // Act & Assert
            Assert.Throws<Exception>(() => commentService.UpdateComment(commentId, emptycontent), "Comment content cannot be empty");
            commentsRepository.Received(1).GetCommentById(commentId);
        }
    }
}
