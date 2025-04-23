namespace SocialApp.Entities
{
    using SocialApp.Enums;

    public class Reaction
    {
        public long UserId { get; set; }

        public long PostId { get; set; }

        public ReactionType Type { get; set; }
    }
}
