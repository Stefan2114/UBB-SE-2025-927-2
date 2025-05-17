namespace Server.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CommentDTO
    {
        [Key]
        public long Id { get; set; }

        required public long UserId { get; set; }

        required public long PostId { get; set; }

        required public string Content { get; set; }

        required public DateTime CreatedDate { get; set; }
    }
}
