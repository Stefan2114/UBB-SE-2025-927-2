-- deleting all data
USE [ISSDB]
GO

ALTER TABLE [dbo].[Comments] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Groups] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[GroupUsers] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Posts] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Reactions] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[UserFollowers] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Users] NOCHECK CONSTRAINT ALL

DELETE FROM [dbo].[Comments]
DELETE FROM [dbo].[Groups]
DELETE FROM [dbo].[GroupUsers]
DELETE FROM [dbo].[Posts]
DELETE FROM [dbo].[Reactions]
DELETE FROM [dbo].[UserFollowers]
DELETE FROM [dbo].[Users]

ALTER TABLE [dbo].[Comments] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Groups] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[GroupUsers] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Posts] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Reactions] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[UserFollowers] WITH CHECK CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT ALL

-- users

SET IDENTITY_INSERT [dbo].[Users] ON


INSERT INTO [dbo].[Users](Id,UserName, Email, PasswordHash) VALUES
(0,'admin','admin@gmail.com','admin'),
(1,'darius','darius@gmail.com','password123'),
(2,'marius','marius@gmail.com','password123'),
(3,'leo','leo@gmail.com','password123'),
(4,'sorin','sorin@gmail.com','password123'),
(5,'horia','horia@gmail.com','password123'),
(6,'calin','calin@gmail.com','password123')

SET IDENTITY_INSERT [dbo].[Users] OFF

-- posts (that aren't on groups)

SET IDENTITY_INSERT [dbo].[Posts] ON

INSERT INTO [dbo].[Posts](Id,Title,Content,CreatedDate,UserId,PostVisibility,PostTag) VALUES

(1, 'Post #1', 'Exploring the hidden gems of urban photography – how everyday scenes can transform into art with the right perspective.', '2025-03-20 19:23:45', 1, 1, 1),
(2, 'Post #2', 'The science behind productivity: why morning routines matter and how to optimize your day for success.', '2025-03-23 18:08:12', 1, 1, 2),
(3, 'Post #3', 'A beginner’s guide to sustainable living: small changes that make a big impact on the planet.', '2025-03-21 22:10:05', 1, 1, 3),
(4, 'Post #4', 'Why learning a musical instrument as an adult is easier than you think – and why it’s worth the effort.', '2025-03-22 13:55:29', 1, 1, 4),
(5, 'Post #5', 'The future of AI in creative industries: will machines replace human artists, or enhance them?', '2025-03-26 12:36:50', 1, 2, 1),
(6, 'Post #6', 'Minimalism vs. maximalism: which lifestyle philosophy aligns better with your personality?', '2025-03-24 11:22:31', 1, 2, 2),
(7, 'Post #7', 'The psychology of color in branding: how hues influence consumer decisions without them realizing.', '2025-03-21 22:10:05', 1, 2, 3),
(8, 'Post #8', 'How digital nomadism is reshaping work culture – and the challenges nobody talks about.', '2025-03-23 22:45:18', 1, 2, 4),
(9, 'Post #9', 'The rise of plant-based diets: debunking myths and uncovering the health benefits.', '2025-03-24 05:12:21', 1, 3, 1),
(10, 'Post #10', 'Why sleep is the ultimate productivity hack – and how to optimize your rest for peak performance.', '2025-03-21 21:10:59', 1, 3, 2),
(11, 'Post #11', 'The art of storytelling in business: how narratives shape customer loyalty and brand identity.', '2025-03-23 10:11:56', 1, 3, 3),
(12, 'Post #12', 'How blockchain is revolutionizing industries beyond cryptocurrency – from healthcare to real estate.', '2025-03-26 08:44:17', 1, 3, 4),
(13, 'Post #13', 'The hidden history of street art: how graffiti evolved from vandalism to high art.', '2025-03-21 21:10:59', 2, 1, 1),
(14, 'Post #14', 'Why emotional intelligence matters more than IQ in leadership and career success.', '2025-03-23 22:45:18', 2, 1, 2),
(15, 'Post #15', 'The surprising benefits of cold showers – from improved circulation to mental resilience.', '2025-03-23 13:37:24', 2, 1, 3),
(16, 'Post #16', 'How to build a personal brand in the age of social media – authenticity vs. curation.', '2025-03-22 18:40:21', 2, 1, 4),
(17, 'Post #17', 'The ethics of genetic engineering: where should we draw the line with CRISPR technology?', '2025-03-20 08:15:32', 2, 2, 1),
(18, 'Post #18', 'Why failure is the best teacher – and how to reframe setbacks as stepping stones.', '2025-03-25 09:03:44', 2, 2, 2),
(19, 'Post #19', 'The silent power of introverts: how quiet leaders are reshaping workplaces.', '2025-03-23 10:11:56', 2, 2, 3),
(20, 'Post #20', 'How micro-habits can lead to massive life changes – the 1% rule in action.', '2025-03-22 07:05:33', 2, 2, 4),
(21, 'Post #21', 'The future of space tourism: when will average people vacation on the Moon?', '2025-03-24 17:38:55', 2, 3, 1),
(22, 'Post #22', 'Why nostalgia marketing works – and how brands tap into our fondest memories.', '2025-03-24 17:38:55', 2, 3, 2),
(23, 'Post #23', 'The dark side of social media algorithms: how they shape our perceptions without us knowing.', '2025-03-26 08:44:17', 2, 3, 3),
(24, 'Post #24', 'How biophilic design is transforming urban spaces – bringing nature back into cities.', '2025-03-24 17:38:55', 2, 3, 4),
(25, 'Post #25', 'The psychology of conspiracy theories: why do people believe in them, and how to counter misinformation.', '2025-03-23 22:45:18', 3, 1, 1),
(26, 'Post #26', 'Why lifelong learning is the key to career resilience in an automated world.', '2025-03-25 20:05:39', 3, 1, 2),
(27, 'Post #27', 'The rise of virtual influencers: are digital personas the future of marketing?', '2025-03-22 10:59:58', 3, 1, 3),
(28, 'Post #28', 'How ancient philosophies like Stoicism are making a comeback in modern self-help.', '2025-03-22 15:29:48', 3, 1, 4),
(29, 'Post #29', 'The science of habit formation: why it takes more than 21 days to build a lasting change.', '2025-03-21 21:10:59', 3, 2, 1),
(30, 'Post #30', 'Why remote work isn’t for everyone – the overlooked downsides of the work-from-home revolution.', '2025-03-25 14:58:27', 3, 2, 2),
(31, 'Post #31', 'How AI-generated art is challenging traditional notions of creativity and authorship.', '2025-03-26 12:36:50', 3, 2, 3),
(32, 'Post #32', 'The surprising link between gut health and mental well-being – what you eat affects your mood.', '2025-03-22 10:59:58', 3, 2, 4),
(33, 'Post #33', 'Why we’re wired for storytelling – the evolutionary roots of narrative and its power today.', '2025-03-20 19:23:45', 3, 3, 1),
(34, 'Post #34', 'The hidden costs of fast fashion – environmental and ethical impacts you didn’t know about.', '2025-03-22 13:55:29', 3, 3, 2),
(35, 'Post #35', 'How to cultivate deep focus in an age of constant distraction – lessons from neuroscience.', '2025-03-21 21:10:59', 3, 3, 3),
(36, 'Post #36', 'The future of education: will traditional classrooms survive in a digital-first world?', '2025-03-21 12:55:41', 3, 3, 4),
(37, 'Post #37', 'How quantum computing could revolutionize industries – from medicine to cybersecurity.', '2025-03-26 15:29:37', 4, 1, 1),
(38, 'Post #38', 'Why silence is the ultimate luxury in our noisy, hyper-connected world.', '2025-03-22 18:40:21', 4, 1, 2),
(39, 'Post #39', 'The unexpected benefits of boredom – why your brain needs downtime to thrive.', '2025-03-21 12:55:41', 4, 1, 3),
(40, 'Post #40', 'How the metaverse is blurring the lines between reality and digital existence.', '2025-03-22 18:40:21', 4, 1, 4),
(41, 'Post #41', 'The rise of solopreneurship: why more people are choosing to go it alone in business.', '2025-03-22 13:55:29', 4, 2, 1),
(42, 'Post #42', 'Why cultural appropriation debates are reshaping art, fashion, and entertainment.', '2025-03-24 21:33:45', 4, 2, 2),
(43, 'Post #43', 'How psychedelics are being rediscovered as tools for mental health treatment.', '2025-03-25 14:58:27', 4, 2, 3),
(44, 'Post #44', 'The psychology of pricing: why we perceive $9.99 as significantly cheaper than $10.', '2025-03-24 11:22:31', 4, 2, 4),
(45, 'Post #45', 'Why ancient survival instincts explain our modern anxieties and how to overcome them.', '2025-03-20 08:15:32', 4, 3, 1),
(46, 'Post #46', 'How decentralized finance (DeFi) is challenging traditional banking systems.', '2025-03-26 08:44:17', 4, 3, 2),
(47, 'Post #47', 'The hidden power of micro-expressions – what your face reveals before you speak.', '2025-03-21 09:30:12', 4, 3, 3),
(48, 'Post #48', 'Why the "10,000-hour rule" is a myth – the real science behind mastering a skill.', '2025-03-20 08:15:32', 4, 3, 4),
(49, 'Post #49', 'How language shapes thought: why some concepts exist only in certain cultures.', '2025-03-22 10:59:58', 5, 1, 1),
(50, 'Post #50', 'The dark forest theory: a chilling answer to the Fermi paradox and alien silence.', '2025-03-21 12:55:41', 5, 1, 2),
(51, 'Post #51', 'Why the "5-second rule" for dropped food is both wrong and surprisingly insightful.', '2025-03-22 18:40:21', 5, 1, 3),
(52, 'Post #52', 'How impostor syndrome affects high achievers – and strategies to overcome self-doubt.', '2025-03-22 18:40:21', 5, 1, 4),
(53, 'Post #53', 'The rise of digital detox retreats – why people are paying to disconnect.', '2025-03-20 08:15:32', 5, 2, 1),
(54, 'Post #54', 'How synesthesia blends the senses – and what it reveals about human perception.', '2025-03-21 22:10:05', 5, 2, 2),
(55, 'Post #55', 'Why the placebo effect is getting stronger – and what it means for modern medicine.', '2025-03-23 10:11:56', 5, 2, 3),
(56, 'Post #56', 'The science of luck: why some people seem to attract good fortune (and how you can too).', '2025-03-23 18:08:12', 5, 2, 4),
(57, 'Post #57', 'How memes became the lingua franca of the internet – and why they matter.', '2025-03-22 13:55:29', 5, 3, 1),
(58, 'Post #58', 'Why time feels like it speeds up as we age – the neuroscience behind perceived time.', '2025-03-22 10:59:58', 5, 3, 2),
(59, 'Post #59', 'The hidden world of urban foraging – how cities are full of edible wild plants.', '2025-03-22 07:05:33', 5, 3, 3),
(60, 'Post #60', 'How "digital twins" are revolutionizing industries from manufacturing to healthcare.', '2025-03-20 16:20:14', 5, 3, 4),
(61, 'Post #61', 'Why laughter is contagious – the evolutionary roots of this social bonding tool.', '2025-03-24 11:22:31', 6, 1, 1),
(62, 'Post #62', 'The Mozart effect debunked – what music really does (and doesn’t do) for your brain.', '2025-03-23 10:11:56', 6, 1, 2),
(63, 'Post #63', 'How predictive policing algorithms risk reinforcing societal biases – the ethical dilemma.', '2025-03-20 16:20:14', 6, 1, 3),
(64, 'Post #64', 'Why some people are naturally early risers – the genetics behind chronotypes.', '2025-03-21 09:30:12', 6, 1, 4),
(65, 'Post #65', 'The uncanny valley: why humanoid robots creep us out at a certain level of realism.', '2025-03-21 12:55:41', 6, 2, 1),
(66, 'Post #66', 'How "forest bathing" became medicine – the science behind nature’s healing power.', '2025-03-22 18:40:21', 6, 2, 2),
(67, 'Post #67', 'Why we procrastinate on things we enjoy – the surprising psychology of self-sabotage.', '2025-03-20 16:20:14', 6, 2, 3),
(68, 'Post #68', 'The hidden history of emojis – how tiny images became a global language.', '2025-03-25 09:03:44', 6, 2, 4),
(69, 'Post #69', 'Why some cultures don’t have words for "blue" – how language shapes color perception.', '2025-03-22 07:05:33', 6, 3, 1),
(70, 'Post #70', 'How "digital minimalism" is helping people reclaim focus in a distracted world.', '2025-03-24 17:38:55', 6, 3, 2),
(71, 'Post #71', 'The rise of "slow TV" – why millions watch mundane events in real time.', '2025-03-23 18:08:12', 6, 3, 3),
(72, 'Post #72', 'Why the "Baader-Meinhof phenomenon" makes you notice things everywhere after learning them.', '2025-03-26 12:36:50', 6, 3, 4);

SET IDENTITY_INSERT [dbo].[Posts] OFF

-- reactions

INSERT INTO [dbo].[Reactions] ([UserId], [PostId], [ReactionType])  
VALUES  
    (1, 5, 2), (2, 12, 3), (3, 25, 1), (4, 40, 4), (5, 8, 2), (6, 55, 1),  
    (1, 30, 3), (2, 45, 2), (3, 10, 4), (4, 20, 1), (5, 35, 3), (6, 50, 2),  
    (1, 15, 4), (2, 22, 1), (3, 48, 2), (4, 59, 3), (5, 6, 1), (6, 33, 4),  
    (1, 41, 3), (2, 52, 2), (3, 13, 1), (4, 27, 4), (5, 58, 3), (6, 39, 2),  
    (1, 7, 1), (2, 19, 4), (3, 21, 3), (4, 32, 2), (5, 44, 1), (6, 53, 3),  
    (1, 11, 2), (2, 26, 4), (3, 36, 1), (4, 49, 3), (5, 57, 2), (6, 9, 4),  
    (1, 23, 1), (2, 34, 3), (3, 46, 2), (4, 60, 4);

-- comments

SET IDENTITY_INSERT [dbo].[Comments] ON

select * from UserFollowers;

SELECT * FROM Posts WHERE UserId IN ((SELECT UserId FROM UserFollowers WHERE FollowerId = @UserId AND (PostVisibility = 2 OR PostVisibility = 3)) OR UserId = @UserId OR PostVisibility = 3

INSERT INTO [dbo].[Comments] ([Id], [UserId], [PostId], [Content], [CreatedDate]) VALUES 
    (1, 1, 5, 'Great post!', GETDATE()),  
    (2, 2, 12, 'I totally agree.', GETDATE()),  
    (3, 3, 25, 'Nice perspective.', GETDATE()),  
    (4, 4, 40, 'Interesting read.', GETDATE()),  
    (5, 5, 8, 'Loved this!', GETDATE()),  
    (6, 6, 55, 'Very insightful.', GETDATE()),  
    (7, 1, 30, 'Thanks for sharing!', GETDATE()),  
    (8, 2, 45, 'Good points.', GETDATE()),  
    (9, 3, 10, 'Well written!', GETDATE()),  
    (10, 4, 20, 'I learned something new.', GETDATE()),  
    (11, 5, 35, 'Awesome content.', GETDATE()),  
    (12, 6, 50, 'Totally relatable.', GETDATE()),  
    (13, 1, 15, 'This is helpful.', GETDATE()),  
    (14, 2, 22, 'Fantastic post!', GETDATE()),  
    (15, 3, 48, 'Agreed!', GETDATE()),  
    (16, 4, 59, 'So true.', GETDATE()),  
    (17, 5, 6, 'Really enjoyed this.', GETDATE()),  
    (18, 6, 33, 'Very informative.', GETDATE()),  
    (19, 1, 41, 'Great insights.', GETDATE()),  
    (20, 2, 52, 'I like this take.', GETDATE()),  
    (21, 3, 13, 'Keep posting!', GETDATE()),  
    (22, 4, 27, 'Well said.', GETDATE()),  
    (23, 5, 58, 'Engaging read.', GETDATE()),  
    (24, 6, 39, 'Super interesting.', GETDATE()),  
    (25, 1, 7, 'This made me think.', GETDATE()),  
    (26, 2, 19, 'Great discussion.', GETDATE()),  
    (27, 3, 21, 'Nice job!', GETDATE()),  
    (28, 4, 32, 'Very well put.', GETDATE()),  
    (29, 5, 44, 'I appreciate this.', GETDATE()),  
    (30, 6, 53, 'Thanks for this.', GETDATE()),  
    (31, 1, 11, 'This is gold.', GETDATE()),  
    (32, 2, 26, 'Amazing!', GETDATE()),  
    (33, 3, 36, 'So insightful.', GETDATE()),  
    (34, 4, 49, 'Nice thoughts.', GETDATE()),  
    (35, 5, 57, 'I never thought of this.', GETDATE()),  
    (36, 6, 9, 'Really cool post.', GETDATE()),  
    (37, 1, 23, 'I appreciate this take.', GETDATE()),  
    (38, 2, 34, 'Brilliant!', GETDATE()),  
    (39, 3, 46, 'This is very helpful.', GETDATE()),  
    (40, 4, 60, 'I love this!', GETDATE()),  
    (41, 5, 3, 'Such a good read.', GETDATE()),  
    (42, 6, 17, 'Very well explained.', GETDATE()),  
    (43, 1, 29, 'Impressive!', GETDATE()),  
    (44, 2, 42, 'This is eye-opening.', GETDATE()),  
    (45, 3, 51, 'Very cool.', GETDATE()),  
    (46, 4, 14, 'Couldn�t agree more.', GETDATE()),  
    (47, 5, 31, 'A fresh perspective.', GETDATE()),  
    (48, 6, 47, 'Nice post!', GETDATE()),  
    (49, 1, 18, 'I enjoyed this.', GETDATE()),  
    (50, 2, 28, 'Thanks for writing this.', GETDATE());  


SET IDENTITY_INSERT [dbo].[Comments] OFF
SELECT * FROM Posts
select * from UserFollowers;
-- Followers

INSERT INTO [dbo].[UserFollowers] ([UserId],[FollowerId]) VALUES
	(1,2),(1,3),(2,1),(2,3),(2,4),(3,1),(3,5),(3,6),(4,1),(4,2),(4,3),(5,1),(5,2),(5,3),(5,4),(6,1),(6,2),(6,3);

-- Groups
SET IDENTITY_INSERT [dbo].[Groups] ON

INSERT INTO [dbo].[Groups] ([Id], [Name], [Image], [Description], [AdminId]) VALUES 
	(0, 'Admin Group', NULL, 'Admin Group', 0),
    (1, 'Morning Fitness Club', NULL, 'A group for early risers who love morning workouts.', 1),
    (2, 'Strength Training Enthusiasts', NULL, 'Focuses on weightlifting and strength-building exercises.', 2),
    (3, 'Yoga for Relaxation', NULL, 'A group for yoga lovers to practice and discuss techniques.', 3),
    (4, 'Weekend Hikers', NULL, 'Organizing weekend hiking trips and outdoor adventures.', 4),
    (5, 'Cardio Lovers', NULL, 'Dedicated to running, cycling, and all things cardio.', 5),
    (6, 'CrossFit Community', NULL, 'For those passionate about CrossFit and intense workouts.', 6);

SET IDENTITY_INSERT [dbo].[Groups] OFF

-- Group users

INSERT INTO [dbo].[GroupUsers] (UserId, GroupId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (1, 2),
    (1, 3),
    (2, 4),
    (2, 5),
    (3, 6),
    (3, 1),
    (4, 2),
    (4, 5),
    (5, 3),
    (5, 6),
    (6, 1),
    (6, 4);

-- Group Posts

INSERT INTO [dbo].[Posts](Title,Content,CreatedDate,UserId,PostVisibility,PostTag,GroupId) VALUES

('Post 1 Group 1', 'The neuroscience of creativity: how your brain generates innovative ideas and how to stimulate more of them.', '2025-03-20 19:23:45', 1, 4, 1, 1),
('Post 2 Group 1', 'Why ancient Roman concrete lasts longer than modern mixtures - the lost technology we''re rediscovering.', '2025-03-23 18:08:12', 3, 4, 2, 1),
('Post 1 Group 2', 'The hidden language of trees: how forests communicate through underground fungal networks.', '2025-03-21 22:10:05', 2, 4, 3, 2),
('Post 2 Group 2', 'How the "Pomodoro Technique" tricks your brain into extreme productivity with simple time management.', '2025-03-22 13:55:29', 1, 4, 4, 2),
('Post 1 Group 3', 'The Mandela Effect explained: why large groups of people remember events that never happened.', '2025-03-26 12:36:50', 3, 4, 1, 3),
('Post 2 Group 3', 'Why some people are naturally night owls - the evolutionary advantage of late-night productivity.', '2025-03-24 11:22:31', 5, 4, 2, 3),
('Post 1 Group 4', 'How "digital hoarding" is becoming the new clutter epidemic of the information age.', '2025-03-21 22:10:05', 4, 4, 3, 4),
('Post 2 Group 4', 'The surprising mathematics behind why snowflakes always have six sides - nature''s geometric perfection.', '2025-03-23 22:45:18', 6, 4, 4, 4),
('Post 1 Group 5', 'Why London''s Underground map is a masterpiece of deceptive design - the truth about the Tube''s geography.', '2025-03-24 05:12:21', 5, 4, 1, 5),
('Post 2 Group 5', 'How "super recognizers" can identify faces they''ve only seen briefly - the rare skill changing criminal investigations.', '2025-03-21 21:10:59', 4, 4, 2, 5),
('Post 1 Group 6', 'The psychology of queueing: why British people form orderly lines while others don''t - cultural conditioning revealed.', '2025-03-23 10:11:56', 6, 4, 3, 6),
('Post 2 Group 6', 'How "vantablack" became the world''s darkest substance - and the artistic controversy surrounding its exclusive rights.', '2025-03-26 08:44:17', 5, 4, 4, 6)


select * from Reactions;
