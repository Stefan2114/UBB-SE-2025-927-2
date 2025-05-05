using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.DTOs
{
    public class BodyMetricDTO
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public float? TargetWeight { get; set; }
    }
}
