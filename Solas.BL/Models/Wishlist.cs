using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Wishlist
    {

        public int WishlistId { get; set; }

        public float? WishlistAmount { get; set; }


        public virtual ICollection<ProductWishlist> Products { get; set; }
        public string UserId { get; set; }

        public ApplicationUser user { get; set; }








    }
}
