using Microsoft.EntityFrameworkCore;
using Solas.BL.Models;

namespace Solas.BL.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepositories<ApplicationUser> apllicationusers { get; }
        IBaseRepositories<Category> categorys { get; }
        IBaseRepositories<Option> options { get; }
        IBaseRepositories<OptionGroup> optiongroups { get; }
        IBaseRepositories<Order> orders { get; }
        IBaseRepositories<OrderProduct> orderproducts { get; }
        IBaseRepositories<Product> products { get; }
        IBaseRepositories<ProductCategory> productcategorys { get; }

        IBaseRepositories<ProductImage> productimages { get; }
        IBaseRepositories<ProductInvintory> productinvintorys { get; }
        IBaseRepositories<ProductOption> productoptions { get; }
        IBaseRepositories<Discount> discounts { get; }
        IBaseRepositories<HomeSlider> homesliders { get; }
        IBaseRepositories<Wishlist> wishlists { get; }
        IBaseRepositories<ProductWishlist> productWishlists { get; }
        IBaseRepositories<Address> address { get; }
        IBaseRepositories<SubBanner> subbanner { get; }
        IBaseRepositories<Comment> Comments { get; }
        IBaseRepositories<Menu> menus { get; }
        IBaseRepositories<SubMenu> submenus { get; }
        IBaseRepositories<StoreInfo> storeinfos { get; }
        IBaseRepositories<SocialMedia> socialmedias { get; }
        IBaseRepositories<CouponCode> couponcodes { get; }
        IBaseRepositories<Shipping> shippings { get; }
        IBaseRepositories<Advertisement> Advertisements { get; }


        void Complete();


    }
}
