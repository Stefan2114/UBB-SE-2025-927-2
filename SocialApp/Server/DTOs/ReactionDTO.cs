namespace Server.DTOs
{
    using AppCommonClasses.Enums;

    public class ReactionDTO
    {
        required public ReactionType Type { get; set; }
    }
}
