using Solas.BL.Models;

namespace Solas.UI.Models.ViewModels
{
    public class OrderProductWishlistViewModel
    {
        public IEnumerable<OrderProduct> orderProducts { get; set; }
        public Wishlist wishlists { get; set; }
        public float TotalOriginal { get; set; }

    }
}
