namespace Server.DTOs
{
    public class UserModelDTO
    {
        required public string Name { get; set; }

        required public string Email { get; set; }

        required public string HashPassword { get; set; }

        required public string Image { get; set; }
    }
}
