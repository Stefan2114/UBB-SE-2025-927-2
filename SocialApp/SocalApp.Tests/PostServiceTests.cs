using SocialApp;
using NSubstitute;
using SocialApp.Services;
using AppCommonClasses.Models;
using AppCommonClasses.Enums;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Services;



namespace SocialApp.Tests
{
    /// <summary>
    /// Contains unit tests for the PostService class.
    /// </summary>
    public class PostServiceTests
    {
        /// <summary>
        /// Validates that the AddPost method returns a Post object when provided with valid arguments.
        /// </summary>
        [Test]
        public void AddPost_WithValidArguments_ReturnsPost()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            var title = "title";
            string? content = "Test Content";
            long userId = 1;
            long groupId = 1;
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            User user = new User { Id = userId, Username = "username", Email = "email", Password = "passwordHash", Image = "image" };
            Group group = new Group { Id = groupId, Name = "groupName", Image = "groupImage", Description = "description", AdminId = 1 };
            Post post = new Post { Title = title, Content = content, UserId = userId, GroupId = groupId, Visibility = postVisibility, Tag = postTag, CreatedDate = DateTime.Now };

            userRepository.GetById(userId).Returns(user);
            groupRepository.GetGroupById(groupId).Returns(group);

            // Act
            postService.AddPost(title, content, userId, groupId, postVisibility, postTag);

            // Assert
            userRepository.Received(1).GetById(userId);
            groupRepository.Received(1).GetGroupById(groupId);
            postRepository.Received(1).SavePost(Arg.Any<Post>());
        }

        /// <summary>
        /// Validates that the AddPost method throws an exception when provided with an invalid title.
        /// </summary>
        [Test]
        public void AddPost_WithInvalidTitle_ThrowsException()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            var title = "";
            string? content = "Test Content";
            long userId = 1;
            long groupId = 1;
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            // Act & Assert
            Assert.Throws<Exception>(() => postService.AddPost(title, content, userId, groupId, postVisibility, postTag), "Post title cannot be empty");
        }

        /// <summary>
        /// Validates that the AddPost method throws an exception when provided with an invalid user ID.
        /// </summary>
        [Test]
        public void AddPost_WithInvalidUserId_ThrowsException()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            var title = "title";
            string? content = "Test Content";
            long userId = 1;
            long groupId = 1;
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            userRepository.GetById(userId).Returns((User)null); // Simulate user not found

            // Act & Assert
            Assert.Throws<Exception>(() => postService.AddPost(title, content, userId, groupId, postVisibility, postTag), "User does not exist");
            userRepository.Received(1).GetById(userId);
        }

        /// <summary>
        /// Validates that the AddPost method throws an exception when provided with an invalid group ID.
        /// </summary>
        [Test]
        public void AddPost_WithInvalidGroupId_ThrowsException()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            var title = "title";
            string? content = "Test Content";
            long userId = 1;
            long groupId = 1;
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            User user = new User { Id = userId, Username = "username", Email = "email", Password = "passwordHash", Image = "image" };

            userRepository.GetById(userId).Returns(user); // Simulate user found
            groupRepository.GetGroupById(userId).Returns((Group)null); // Simulate group not found

            // Act & Assert
            Assert.Throws<Exception>(() => postService.AddPost(title, content, userId, groupId, postVisibility, postTag), "Group does not exist");
            userRepository.Received(1).GetById(userId);
            groupRepository.Received(1).GetGroupById(groupId);
        }

        /// <summary>
        /// Validates that the DeletePost method throws an exception when provided with an invalid post ID.
        /// </summary>
        [Test]
        public void DeletePost_WithInvalidPostId_ThrowsException()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            long postId = 1;

            postRepository.GetPostById(postId).Returns((Post)null); // Simulate post not found

            // Act & Assert
            Assert.Throws<Exception>(() => postService.DeletePost(postId), "Post does not exist");
            postRepository.Received(1).GetPostById(postId);
        }

        /// <summary>
        /// Validates that the DeletePost method successfully deletes a post when provided with a valid post ID.
        /// </summary>
        [Test]
        public void DeletePost_WithValidPostId()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            long postId = 1;

            Post post = new Post { Id = postId, Title = "title", Content = "content", UserId = 1, GroupId = 1, Visibility = PostVisibility.Public, Tag = PostTag.Food, CreatedDate = DateTime.Now };

            postRepository.GetPostById(postId).Returns(post);

            // Act
            postService.DeletePost(postId);

            // Assert
            postRepository.Received(1).GetPostById(postId);
            postRepository.Received(1).DeletePostById(postId);
        }

        /// <summary>
        /// Validates that the UpdatePost method throws an exception when provided with an invalid post ID.
        /// </summary>
        [Test]
        public void UpdatePost_WithInvalidPostId_ThrowsException()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            long postId = 1;
            var title = "title";
            string? content = "Test Content";
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            postRepository.GetPostById(postId).Returns((Post)null); // Simulate post not found

            // Act & Assert
            Assert.Throws<Exception>(() => postService.UpdatePost(postId, title, content, postVisibility, postTag), "Post does not exist");
            postRepository.Received(1).GetPostById(postId);
        }

        /// <summary>
        /// Validates that the UpdatePost method successfully updates a post when provided with a valid post ID.
        /// </summary>
        [Test]
        public void UpdatePost_WithValidPostId()
        {
            // Arange
            var postRepository = Substitute.For<IPostRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var groupRepository = Substitute.For<IGroupRepository>();

            PostService postService = new PostService(postRepository, userRepository, groupRepository);

            long postId = 1;
            var title = "title";
            string? content = "Test Content";
            PostVisibility postVisibility = PostVisibility.Public;
            PostTag postTag = PostTag.Food;

            Post post = new Post { Id = postId, Title = title, Content = content, UserId = 1, GroupId = 1, Visibility = postVisibility, Tag = postTag, CreatedDate = DateTime.Now };

            postRepository.GetPostById(postId).Returns(post);

            // Act
            postService.UpdatePost(postId, title, content, postVisibility, postTag);

            // Assert
            postRepository.Received(1).GetPostById(postId);
            postRepository.Received(1).UpdatePostById(postId, title, content, postVisibility, postTag);
        }
    }
}