using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.ViewModels
{
    public class ReactionViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ReactionType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
