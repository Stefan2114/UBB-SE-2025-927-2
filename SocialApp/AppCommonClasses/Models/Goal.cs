using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Models
{
    [Table("goals")]
    public class Goal
    {
        [Column("g_id")]
        public int GoalId { get; set; }

        [Column("g_description")]
        public string Description { get; set; }
        public Goal(int goalId, string description)
        {
            GoalId = goalId;
            Description = description;
        }
    }
}
