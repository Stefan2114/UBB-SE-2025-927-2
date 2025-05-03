using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Enums;

namespace AppCommonClasses.Models
{
    public class CommentDTO
    {
        [Key]
        public long Id { get; set; }
        public required long UserId { get; set; }
        public required long PostId { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedDate { get; set; }
    }


}
