namespace Server.DTOs
{
    using AppCommonClasses.Enums;

    public class PostDTO
    {

        required public string Title { get; set; }

        required public string Content { get; set; }

        required public PostVisibility Visibility { get; set; }

        required public PostTag Tag { get; set; }
    }
}