
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string SocialMediaName { get; set; }
        public string IconPath { get; set; }
        public string Url { get; set; }
        public int StoreInfoId { get; set; }

        public StoreInfo StoreInfo { get; set; }
    }
}
