using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Discount : CommonProp
    {
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }
        public string? DiscountDescription { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal DiscountPrecent { get; set; }
        public bool? IsActive { get; set;}
        public virtual ICollection<Product>? Product { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

    }
}
