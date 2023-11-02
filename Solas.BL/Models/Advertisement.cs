using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Advertisement:CommonProp
    {
        public int AdvertisementId { get; set; }
        public string  AdvertisementText { get; set; }

    }
}
