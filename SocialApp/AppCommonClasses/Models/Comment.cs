using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("UserId")]
        public required long UserId { get; set; }

        [ForeignKey("PostId")]
        public required long PostId { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
