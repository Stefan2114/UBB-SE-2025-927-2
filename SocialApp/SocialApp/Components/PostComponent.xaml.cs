using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using AppCommonClasses.Enums;
using SocialApp.Repository;
using SocialApp.Services;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Linq;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System.Drawing;
using Windows.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using SocialApp.Proxies;

namespace SocialApp.Components
{
    public sealed partial class PostComponent : UserControl
    {
        private string title;
        private PostVisibility visibility;
        private long userId;
        private string content;
        private DateTime createdDate;
        private long postId;
        private PostTag tag;
        private AppController AppController;

        private ReactionService reactionService;
        private CommentService commentService;

        public DateTime PostCreationTime { get; set; }

        public string TimeSincePost
        {
            get
            {
                var timeSpan = DateTime.Now - PostCreationTime;
                if (timeSpan.TotalDays >= 1)
                    return $"{(int)timeSpan.TotalDays} days ago";
                else if (timeSpan.TotalHours >= 1)
                    return $"{(int)timeSpan.TotalHours} hours ago";
                else
                    return $"{(int)timeSpan.TotalMinutes} minutes ago";
            }
        }

        public PostVisibility PostVisibility
        {
            get => visibility;
            set
            {
                visibility = value;
                VisibilityText.Text = visibility.ToString();
            }
        }

        public PostComponent()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.reactionService = new ReactionService(new ReactionRepositoryProxy());
            this.commentService = new CommentService(new CommentRepositoryProxy(), new PostRepositoryProxy(), new UserServiceProxy());

            this.AppController = App.Services.GetService<AppController>();
        }

        public PostComponent(string title, PostVisibility visibility, long userId, string content, DateTime createdDate, PostTag tag, long postId = 0)
        {
            this.title = title;
            this.DataContext = this;
            this.InitializeComponent();
            this.PostVisibility = visibility;
            this.userId = userId;
            this.content = content;
            this.createdDate = createdDate;
            this.postId = postId;
            this.PostCreationTime = createdDate;
            this.tag = tag;

            this.reactionService = new ReactionService(new ReactionRepositoryProxy());
            this.commentService = new CommentService(new CommentRepositoryProxy(), new PostRepositoryProxy(), new UserServiceProxy());

            this.AppController = App.Services.GetService<AppController>();

            Title.Text = title;
            TimeSince.Text = TimeSincePost; // Use the property for time display
            // change background color based on tag
            switch (tag)
            {
                case PostTag.Food:
                    PostBorder.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case PostTag.Workout:
                    PostBorder.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
                default:
                    break;
            }
            SetContent(); // Set text or image
            LoadReactionCounts();
        }

        private async void SetContent()
        {
            const string imagePrefix = "image://";
            if (content.StartsWith(imagePrefix))
            {
                string base64Image = content.Substring(imagePrefix.Length);
                PostImage.Source = await AppController.DecodeBase64ToImageAsync(base64Image);
                PostImage.Visibility = Visibility.Visible;
                Content.Visibility = Visibility.Collapsed;
            }
            else
            {
                Content.Text = content;
                PostImage.Visibility = Visibility.Collapsed;
                Content.Visibility = Visibility.Visible;
            }
        }

        private void LoadReactionCounts()
        {
            var reactions = reactionService.GetReactionsForPost(postId);
            LikeCount.Text = reactions.Count(r => r.Type == ReactionType.Like).ToString();
            LoveCount.Text = reactions.Count(r => r.Type == ReactionType.Love).ToString();
            LaughCount.Text = reactions.Count(r => r.Type == ReactionType.Laugh).ToString();
            AngryCount.Text = reactions.Count(r => r.Type == ReactionType.Anger).ToString();
        }

        private void LoadComments()
        {
            var comments = commentService.GetCommentsByPostId(postId);
            CommentsListView.ItemsSource = comments;
        }

        private void OnLikeButtonClick(object sender, RoutedEventArgs e)
        {
            if(AppController.CurrentUser != null)
            {
                reactionService.AddReaction(AppController.CurrentUser.Id, postId, ReactionType.Like);
                LoadReactionCounts();
            }

        }

        private void OnLoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (AppController.CurrentUser != null)
            {
                reactionService.AddReaction(AppController.CurrentUser.Id, postId, ReactionType.Love);
                LoadReactionCounts();
            }

        }

        private void OnLaughButtonClick(object sender, RoutedEventArgs e)
        {
            if (AppController.CurrentUser != null)
            {
                reactionService.AddReaction(AppController.CurrentUser.Id, postId, ReactionType.Laugh);
                LoadReactionCounts();
            }

        }

        private void OnAngryButtonClick(object sender, RoutedEventArgs e)
        {
            if (AppController.CurrentUser != null)
            {
                reactionService.AddReaction(AppController.CurrentUser.Id, postId, ReactionType.Anger);
                LoadReactionCounts();
            }
        }

        private void OnCommentButtonClick(object sender, RoutedEventArgs e)
        {
            LoadComments();
            CommentSection.Visibility = Visibility.Visible;
        }

        private void OnSubmitCommentButtonClick(object sender, RoutedEventArgs e)
        {
            string commentText = CommentTextBox.Text;
            if (!string.IsNullOrEmpty(commentText))
            {
                commentService.AddComment(commentText, userId, postId);
                CommentTextBox.Text = string.Empty;
                CommentSection.Visibility = Visibility.Collapsed;
            }
        }
    }
}