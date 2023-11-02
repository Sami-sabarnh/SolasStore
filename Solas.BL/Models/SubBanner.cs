using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class SubBanner : CommonProp
    {
        public int SubBannerId { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string UrlImage { get; set; }
         public bool IsPublish { get; set; }
    }
}
