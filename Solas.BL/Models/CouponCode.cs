using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class CouponCode
    {
        public int CouponCodeId { get; set; }
        public int? CouponCodedisscount { get; set; }
        public int? CouponCodepercent { get; set; }
        
        public string CouponCodeText { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
