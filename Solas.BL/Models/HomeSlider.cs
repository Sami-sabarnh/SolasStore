using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class HomeSlider:CommonProp
    {
        public int HomeSliderId { get; set; }
        public string UrlImage { get; set; }
        [Range(0, 30, ErrorMessage = "ShortTitle percentage must be between 0 and 30.")]

        public string? ShortTitle { get; set; }
        [Range(0, 30, ErrorMessage = "LongTitle percentage must be between 0 and 30.")]

        public string? LongTitle { get; set; }
    }
}
