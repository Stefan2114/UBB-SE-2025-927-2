using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCommonClasses.Models
{
    public class ReactionDTO
    {
        public required ReactionType Type { get; set; }

    }
}
