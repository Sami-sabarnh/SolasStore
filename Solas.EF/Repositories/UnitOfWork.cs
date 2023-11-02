using Solas.BL.IRepositories;
using Solas.BL.Models;
using System.Net;
using System.Xml.Linq;

namespace Solas.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext db;
        public IBaseRepositories<ApplicationUser> apllicationusers { get; private set; }
        public IBaseRepositories<Category> categorys { get; private set; }
        public IBaseRepositories<Option> options { get; private set; }
        public IBaseRepositories<OptionGroup> optiongroups { get; private set; }
        public IBaseRepositories<Order> orders { get; private set; }
        public IBaseRepositories<OrderProduct> orderproducts { get; private set; }
        public IBaseRepositories<Product> products { get; private set; }
        public IBaseRepositories<ProductCategory> productcategorys { get; private set; }
        public IBaseRepositories<ProductImage> productimages { get; private set; }
        public IBaseRepositories<ProductInvintory> productinvintorys { get; private set; }
        public IBaseRepositories<ProductOption> productoptions { get; private set; }
        public IBaseRepositories<Discount> discounts { get; private set; }
        public IBaseRepositories<HomeSlider> homesliders { get; private set; }
        public IBaseRepositories<Wishlist> wishlists { get; private set; }
        public IBaseRepositories<ProductWishlist> productWishlists { get; private set; }
        public IBaseRepositories<Address> address { get; private set; }

        public IBaseRepositories<SubBanner> subbanner { get; private set; }
        public IBaseRepositories<Comment> Comments { get; private set; }

       public IBaseRepositories<Menu> menus { get; private set; }
        public IBaseRepositories<SubMenu> submenus { get; private set; }

       public IBaseRepositories<StoreInfo> storeinfos { get; private set;   }
        public IBaseRepositories<SocialMedia> socialmedias { get; private set; }
        public IBaseRepositories<CouponCode> couponcodes { get; private set; }
        public IBaseRepositories<Shipping> shippings { get; private set; }
        public IBaseRepositories<Advertisement> Advertisements { get; private set; }

        public UnitOfWork(AppDbContext _db)
        {
            db = _db;

            apllicationusers = new BaseRepositories<ApplicationUser>(db);
            categorys = new BaseRepositories<Category>(db);
            categorys = new BaseRepositories<Category>(db);
            options = new BaseRepositories<Option>(db);
            optiongroups = new BaseRepositories<OptionGroup>(db);
            orders = new BaseRepositories<Order>(db);
            orderproducts = new BaseRepositories<OrderProduct>(db);
            products = new BaseRepositories<Product>(db);
            productcategorys = new BaseRepositories<ProductCategory>(db);
            productimages = new BaseRepositories<ProductImage>(db);
            productinvintorys = new BaseRepositories<ProductInvintory>(db);
            productoptions = new BaseRepositories<ProductOption>(db);
            discounts = new BaseRepositories<Discount>(db);
            homesliders = new BaseRepositories<HomeSlider>(db);
            wishlists = new BaseRepositories<Wishlist>(db);
            productWishlists = new BaseRepositories<ProductWishlist>(db);
            address= new BaseRepositories<Address>(db);
            subbanner= new BaseRepositories<SubBanner>(db);
            Comments= new BaseRepositories<Comment>(db);
            menus= new BaseRepositories<Menu> (db);
            submenus= new BaseRepositories<SubMenu> (db);
            storeinfos=  new BaseRepositories<StoreInfo> (db);
            socialmedias=  new BaseRepositories<SocialMedia> (db);
            couponcodes= new BaseRepositories<CouponCode>(db);
            shippings = new BaseRepositories<Shipping>(db);

            Advertisements = new BaseRepositories<Advertisement>(db);

        }

        public void Complete()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
