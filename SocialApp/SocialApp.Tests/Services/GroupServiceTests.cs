namespace SocialApp.Tests
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using SocialApp.Entities;
    using SocialApp.Repository;
    using SocialApp.Services;

    /// <summary>
    /// Contains unit tests for the GroupService class.
    /// </summary>
    public class GroupServiceTests
    {
        private IGroupRepository groupRepository;
        private IUserRepository userRepository;
        private GroupService groupService;

        /// <summary>
        /// Sets up the test environment.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.groupRepository = Substitute.For<IGroupRepository>();
            this.userRepository = Substitute.For<IUserRepository>();
            this.groupService = new GroupService(this.groupRepository, this.userRepository);
        }

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the group does not exist.
        /// </summary>
        [Test]
        public void ValidateUpdate_GroupDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            long groupId = 1;
            string name = "GroupName";
            string desc = "Description";
            string image = "Image";
            long adminId = 1;

            this.groupRepository.GetById(groupId).Returns((Group)null);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => this.groupService.ValidateUpdate(groupId, name, desc, image, adminId));
            Assert.That(ex.Message, Is.EqualTo("Group with ID 1 does not exist (Parameter 'groupId')"));
        }

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the user does not exist.
        /// </summary>
        [Test]
        public void ValidateUpdate_UserDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            long groupId = 1;
            string name = "GroupName";
            string desc = "Description";
            string image = "Image";
            long adminId = 1;

            this.groupRepository.GetById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
            this.userRepository.GetById(adminId).Returns((User)null);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => this.groupService.ValidateUpdate(groupId, name, desc, image, adminId));
            Assert.That(ex.Message, Is.EqualTo("User with ID 1 does not exist (Parameter 'adminUserId')"));
        }

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the group name is empty.
        /// </summary>
        [Test]
        public void ValidateUpdate_GroupNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            long groupId = 1;
            string name = string.Empty;
            string desc = "Description";
            string image = "Image";
            long adminId = 1;

            this.groupRepository.GetById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
            this.userRepository.GetById(adminId).Returns(new User { Username = "Username", Email = "Email", PasswordHash = "PasswordHash", Image = "Image" });

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => this.groupService.ValidateUpdate(groupId, name, desc, image, adminId));
            Assert.That(ex.Message, Is.EqualTo("Group name cannot be empty or whitespace (Parameter 'groupName')"));
        }

        /// <summary>
        /// Validates that the ValidateUpdate method updates the group when provided with valid arguments.
        /// </summary>
        [Test]
        public void ValidateUpdate_ValidArguments_UpdatesGroup()
        {
            // Arrange
            long groupId = 1;
            string name = "GroupName";
            string desc = "Description";
            string image = "Image";
            long adminId = 1;

            this.groupRepository.GetById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
            this.userRepository.GetById(adminId).Returns(new User { Username = "Username", Email = "Email", PasswordHash = "PasswordHash", Image = "Image" });

            // Act
            this.groupService.ValidateUpdate(groupId, name, desc, image, adminId);

            // Assert
            this.groupRepository.Received(1).UpdateById(groupId, name, image, desc, adminId);
        }

        /// <summary>
        /// Validates that the GetAll method returns all groups.
        /// </summary>
        [Test]
        public void GetAll_ReturnsAllGroups()
        {
            // Arrange
            var groups = new List<Group> { new Group { Name = "Group1", Image = "Image1", Description = "Description1", AdminId = 1 }, new Group { Name = "Group2", Image = "Image2", Description = "Description2", AdminId = 2 } };
            this.groupRepository.GetAll().Returns(groups);

            // Act
            var result = this.groupService.GetAll();

            // Assert
            Assert.That(result, Is.EqualTo(groups));
        }

        /// <summary>
        /// Validates that the GetById method returns the correct group.
        /// </summary>
        [Test]
        public void GetById_ReturnsCorrectGroup()
        {
            // Arrange
            long groupId = 1;
            var group = new Group { Id = groupId, Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 };
            this.groupRepository.GetById(groupId).Returns(group);

            // Act
            var result = this.groupService.GetById(groupId);

            // Assert
            Assert.That(result, Is.EqualTo(group));
        }

        /// <summary>
        /// Validates that the GetUsersFromGroup method returns the correct users.
        /// </summary>
        [Test]
        public void GetUsersFromGroup_ReturnsCorrectUsers()
        {
            // Arrange
            long groupId = 1;
            var users = new List<User>
            {
                new User { Username = "User1", Email = "Email1", PasswordHash = "PasswordHash1", Image = "Image1" },
                new User { Username = "User2", Email = "Email2", PasswordHash = "PasswordHash2", Image = "Image2" }
            };
            this.groupRepository.GetById(groupId).Returns(new Group { Id = groupId });
            this.groupRepository.GetUsersFromGroup(groupId).Returns(users);

            // Act
            var result = this.groupService.GetUsersFromGroup(groupId);

            // Assert
            Assert.That(result, Is.EqualTo(users));
        }

        /// <summary>
        /// Validates that the GetGroupsForUser method returns the correct groups.
        /// </summary>
        [Test]
        public void GetGroupsForUser_ReturnsCorrectGroups()
        {
            // Arrange
            long userId = 1;
            var groups = new List<Group>
            {
                new Group { Name = "Group1", Image = "Image1", Description = "Description1", AdminId = 1 },
                new Group { Name = "Group2", Image = "Image2", Description = "Description2", AdminId = 2 }
            };
            this.userRepository.GetById(userId).Returns(new User { Id = userId });
            this.groupRepository.GetGroupsForUser(userId).Returns(groups);

            // Act
            var result = this.groupService.GetGroupsForUser(userId);

            // Assert
            Assert.That(result, Is.EqualTo(groups));
        }
    }
}
