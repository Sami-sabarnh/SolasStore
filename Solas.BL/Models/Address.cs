using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Address
    {

        public int AddressId { get; set; }
        public string? State { get; set; }
        [MaxLength(10)]

        public string City { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        public bool IsMobileConfirm { get; set; }
        [MaxLength(10)]

        public string FirstName { get; set; }
        [MaxLength(10)]

        public string LasttName { get; set; }
        public string FirstAddressName { get; set; }
        public string? SecondtAddressName { get; set; }
        [MaxLength(15)]

        public string? Apartment { get; set; }

        public string UserId { get; set; }

        public ApplicationUser user { get; set; }
        public virtual ICollection<Order> orders { get; set; }

        //public int OrderId { get; set; }
        //public Order order { get; set; }

    }
}
