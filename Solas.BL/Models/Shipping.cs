using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }
        public int ShippingPrice { get; set; }
        public string? ShippingCoupon { get; set; }
        public string City { get; set; }
        public int? Notes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
