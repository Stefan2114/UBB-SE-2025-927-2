namespace Server.DTOs
{
    public class CalorieDto
    {
        public long U_Id { get; set; }

        public DateTime Today { get; set; }

        public float DailyIntake { get; set; }

        public float CaloriesConsumed { get; set; }

        public float CaloriesBurned { get; set; }
    }
}