using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Models
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

