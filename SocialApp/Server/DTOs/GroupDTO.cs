namespace Server.DTOs
{
    public class GroupDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long AdminId { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
