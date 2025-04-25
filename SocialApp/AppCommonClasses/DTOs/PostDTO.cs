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
    public class PostDTO
    {

        public required string Title { get; set; }

        public required string Content { get; set; }

        public required PostVisibility Visibility { get; set; }

        public required PostTag Tag { get; set; }

    }


}
