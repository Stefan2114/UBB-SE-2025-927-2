-- Insert Users
INSERT INTO [dbo].[Users] ([UserName], [Email], [PasswordHash])
VALUES 
('User1', 'user1@example.com', 'hashedpassword1'),
('User2', 'user2@example.com', 'hashedpassword2'),
('User3', 'user3@example.com', 'hashedpassword3');

-- Insert Groups
INSERT INTO [dbo].[Groups] ([Name], [AdminId])
VALUES 
('Group1', 1),
('Group2', 2),
('Group3', 3);

-- Insert Posts
INSERT INTO [dbo].[Posts] ([Title], [Description], [CreatedDate], [UserId], [PostVisibility], [GroupId], [PostTag])
VALUES 
('Post1', 'Description for Post1', GETDATE(), 1, 1, 1, 101),
('Post2', 'Description for Post2', GETDATE(), 2, 2, 2, 102),
('Post3', 'Description for Post3', GETDATE(), 3, 1, 3, 103);

-- Insert Reactions
INSERT INTO [dbo].[Reactions] ([UserId], [PostId], [ReactionType])
VALUES 
(1, 1, 1),
(2, 2, 2),
(3, 3, 1);

-- Insert Comments
INSERT INTO [dbo].[Comments] ([UserId], [PostId], [Content], [CreatedDate])
VALUES 
(1, 1, 'Nice post!', GETDATE()),
(2, 2, 'Interesting thoughts.', GETDATE()),
(3, 3, 'I completely agree!', GETDATE());

-- Insert GroupUsers
INSERT INTO [dbo].[GroupUsers] ([UserId], [GroupId])
VALUES 
(1, 1),
(2, 2),
(3, 3);

-- Insert UserFollowers
INSERT INTO [dbo].[UserFollowers] ([UserId], [FollowerId])
VALUES 
(1, 2),
(2, 3),
(3, 1);