using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class StoreInfo
    {
        public int StoreInfoId { get; set; }
        public int SupportCenterNumber { get; set; }
        public string SupportCenterAvailable { get; set; }
        public  string LogoPath { get; set; }
        public ICollection<SocialMedia> socialMedias  { get; set; }


    }
}
