namespace Server.DTOs
{
    using AppCommonClasses.Enums;

    public class ReactionDTO
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public ReactionType Type { get; set; }
    }
}
