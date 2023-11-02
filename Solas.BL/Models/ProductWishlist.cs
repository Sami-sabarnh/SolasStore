using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class ProductWishlist
    {
        public int ProductWishlistId { get; set; }



        public int WishlistId { get; set; }
        public Wishlist wishlist  { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
