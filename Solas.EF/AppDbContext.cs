using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solas.BL.IRepositories;
using Solas.BL.Models;


namespace Solas.EF
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<Discount> discounts { get; set; }
        public DbSet<ProductInvintory> productInvintories { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<ProductOption> productOptions { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
        public DbSet<Option> optionss { get; set; }
        public DbSet<OptionGroup> optionGroups { get; set; }
        public DbSet<HomeSlider> homeSliders { get; set; }
        public DbSet<ProductWishlist> productWishlists { get; set; }
        public DbSet<Wishlist> wishlists { get; set; }
        public DbSet<Address> Addreses { get; set; }
        public DbSet<SubBanner> subBanners { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<SubMenu> subMenus { get; set; }
        public DbSet<StoreInfo> storeInfos { get; set; }
        public DbSet<SocialMedia> socialMedias { get; set; }
        public DbSet<CouponCode> couponCodes { get; set; }

          public DbSet<Shipping> shippings { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(p => p.ProductCategoryId);
                entity.Property(p => p.CategoryId).ValueGeneratedOnAdd();
                entity.Property(p => p.ProductCategoryName).IsRequired();
                entity.HasOne(x => x.Products).WithMany(x => x.ProductCategories).HasForeignKey(x => x.ProductId);
                entity.HasOne(x => x.category).WithMany(c => c.ProductCategories).HasForeignKey(x => x.CategoryId);

            });
          
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                entity.Property(entity => entity.ProductName).IsRequired();
                entity.HasOne(p => p.ProductInvintory).WithOne(x => x.Product).HasForeignKey<Product>(x => x.ProductInvintoryId);
                entity.HasMany(p => p.ProductImages).WithOne(pi => pi.Product).HasForeignKey(pi => pi.ProductId);
                entity.HasOne(p => p.Discount).WithMany(x => x.Product).HasForeignKey(x => x.DiscountId);
                entity.HasMany(p => p.Comments).WithOne(pi => pi.Product).HasForeignKey(pi => pi.ProductId);
            });
             
            builder.Entity<Discount>(entity =>
            {
                entity.HasKey(p => p.DiscountId);
                entity.Property(p => p.DiscountId).ValueGeneratedOnAdd();
                entity.Property(entity => entity.DiscountName).IsRequired();
                entity.Property(entity => entity.DiscountPrecent).IsRequired();
                entity.Property(entity => entity.IsActive).HasDefaultValue(true);
                entity.Property(d => d.DiscountPrecent).HasColumnType("decimal(2,2)").HasPrecision(2, 2);




            });
            builder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(p => p.ProductImageId);
                entity.Property(x => x.ProductImageId).ValueGeneratedOnAdd();
                entity.Property(x => x.ProductImageUrl).IsRequired();
                entity.HasOne(p => p.Product).WithMany(x => x.ProductImages).HasForeignKey(p => p.ProductId);



            });
            builder.Entity<ProductInvintory>(entity =>
            {
                entity.HasKey(p => p.ProductInvintoryId);
                entity.Property(x => x.ProductInvintoryId).ValueGeneratedOnAdd();
                entity.Property(x => x.Quintity).IsRequired();
                //entity.HasOne(p => p.Product).WithOne(x => x.ProductInvintory).HasForeignKey<ProductInvintory>(x => x.ProductInvintoryId);
            });
            builder.Entity<ProductOption>(entity =>
            {
                entity.HasKey(x => x.ProductOptionId);
                entity.HasOne(x => x.Option).WithMany(x => x.productOptions).HasForeignKey(x => x.OptionId).HasForeignKey(x => x.ProductGroupId);
                entity.HasOne(x => x.Product).WithMany(x => x.productOptions).HasForeignKey(x => x.ProductId);

            });
            builder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.OrderId);
                entity.Property(x => x.OrderId).ValueGeneratedOnAdd();
                entity.Property(x => x.OrderAmount).IsRequired();
                entity.HasOne(p => p.Discount).WithMany(x => x.Orders).HasForeignKey(x => x.DiscountId);
				entity.HasOne(x => x.Address).WithMany(x => x.orders).HasForeignKey(x => x.AddressId);
			    entity.HasOne(x => x.couponCode).WithMany(x => x.Orders).HasForeignKey(x => x.CouponCodeId);
			    entity.HasOne(x => x.Shipping).WithMany(x => x.Orders).HasForeignKey(x => x.ShippingId);



				entity.HasOne(o => o.user).WithMany(x => x.Orders).HasForeignKey(o => o.UserId);
            });
            builder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(x => x.OrderProductId);
                entity.HasOne(x => x.Order).WithMany(x => x.Products).HasForeignKey(x => x.OrderId);
                entity.HasOne(x => x.Product).WithMany(x => x.Orders).HasForeignKey(x => x.ProductId);
                entity.HasOne(x => x.user).WithMany(x => x.Products).HasForeignKey(x => x.UserId);
                entity.HasMany(op => op.Options).WithMany(o => o.orderProducts).UsingEntity(j => j.ToTable("OrderProductOptions"));

            });
            builder.Entity<ProductWishlist>(entity =>
            {
                entity.HasKey(x => x.ProductWishlistId);
                entity.HasOne(x => x.wishlist).WithMany(x => x.Products).HasForeignKey(x => x.WishlistId);
                entity.HasOne(x => x.Product).WithMany(x => x.ProductWishlists).HasForeignKey(x => x.ProductId);
            });
            builder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(x => x.WishlistId);
                entity.Property(x => x.WishlistId).ValueGeneratedOnAdd();
                entity.HasOne(x => x.user).WithOne(x => x.wishlist).HasForeignKey<Wishlist>(x => x.UserId);

            });
            builder.Entity<Option>(entity =>
            {
                entity.HasKey(x => x.OptionId);
                entity.Property(x => x.OptionName).IsRequired();
                entity.HasOne(x => x.OptionGroup).WithMany(x => x.Options).HasForeignKey(x => x.OptionGroupId);
            });
            builder.Entity<OptionGroup>(entity =>
            {
                entity.HasKey(x => x.OptionGroupId);
                entity.Property(x => x.OptionGroupName).IsRequired();

            });
            builder.Entity<HomeSlider>(entity =>
            {
                entity.HasKey(x => x.HomeSliderId);
                entity.Property(x => x.HomeSliderId).ValueGeneratedOnAdd();
                entity.Property(x => x.UrlImage).IsRequired();

            });
            builder.Entity<Address>(entity =>
            {
                entity.HasKey(p => p.AddressId);
                entity.Property(p => p.AddressId).ValueGeneratedOnAdd();
                entity.Property(p => p.Mobile).IsRequired();
                entity.HasOne(x => x.user).WithMany(x => x.addresses).HasForeignKey(x => x.UserId);
            //  entity.HasOne(x => x.order).WithOne(c => c.address).HasForeignKey<Address>(x => x.OrderId);

            });
            builder.Entity<Comment>(entity =>
            {
                entity.HasKey(p => p.CommentId);
                entity.Property(p => p.CommentId).ValueGeneratedOnAdd();
                entity.Property(p => p.CommentText).IsRequired();
                entity.Property(p => p.Rate).IsRequired();
                entity.HasOne(x => x.user).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);

            });
            builder.Entity<SubBanner>(entity =>
            {
                entity.HasKey(p => p.SubBannerId);
                entity.Property(p => p.SubBannerId).ValueGeneratedOnAdd();
                entity.Property(p => p.UrlImage).IsRequired();
             

            });
            builder.Entity<Menu>(entity =>
            {
                entity.HasKey(p => p.MenuId);
                entity.Property(p => p.MenuId).ValueGeneratedOnAdd();
                entity.Property(p => p.MenuName).IsRequired();
                entity.HasMany(x => x.subMenus).WithOne(x => x.Menu).HasForeignKey(x => x.SubMenuId);


            });
            builder.Entity<SubMenu>(entity =>
            {
                entity.HasKey(p => p.SubMenuId);
                entity.Property(p => p.SubMenuId).ValueGeneratedOnAdd();
                entity.Property(p => p.SubMenuName).IsRequired();
                entity.HasOne(x => x.Menu).WithMany(x => x.subMenus).HasForeignKey(x => x.MenuId);


            });
            builder.Entity<StoreInfo>(entity =>
            {
                entity.HasKey(p => p.StoreInfoId);
                entity.Property(p => p.StoreInfoId).ValueGeneratedOnAdd();
                entity.Property(p => p.LogoPath).IsRequired();
                entity.HasMany(x => x.socialMedias).WithOne(x => x.StoreInfo).HasForeignKey(x => x.SocialMediaId);

            });
            builder.Entity<SocialMedia>(entity =>
            {
                entity.HasKey(p => p.SocialMediaId);
                entity.Property(p => p.SocialMediaId).ValueGeneratedOnAdd();
                entity.Property(p => p.IconPath).IsRequired();
                entity.HasOne(x => x.StoreInfo).WithMany(x => x.socialMedias).HasForeignKey(x => x.StoreInfoId);

            });
            builder.Entity<CouponCode>(entity =>
            {
                entity.HasKey(p => p.CouponCodeId);
                entity.Property(p => p.CouponCodeId).ValueGeneratedOnAdd();
                entity.Property(p => p.CouponCodeText).IsRequired();
               // entity.HasMany(x => x.Orders).WithOne(x => x.couponCode).HasForeignKey(x => x.OrderId);

            });
            builder.Entity<Shipping>(entity =>
            {
                entity.HasKey(p => p.ShippingId);
                entity.Property(p => p.ShippingId).ValueGeneratedOnAdd();
                entity.Property(p => p.ShippingPrice).IsRequired();

            });
            builder.Entity<Advertisement>(entity =>
            {
                entity.HasKey(p => p.AdvertisementId);
                entity.Property(p => p.AdvertisementId).ValueGeneratedOnAdd();
                entity.Property(p => p.AdvertisementText).IsRequired();

            });
            //// Add Admin Role
            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = "1",
            //    Name = "Admin",
            //    NormalizedName = "ADMIN"
            //});

            //// Add Admin User
            //var hasher = new PasswordHasher<ApplicationUser>();
            //builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            //{
            //    Id = "1",
            //    UserName = "admin",
            //    NormalizedUserName = "ADMIN",
            //    Email = "sabarnh123@example.com",
            //    NormalizedEmail = "ADMIN@EXAMPLE.COM",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "P@$$w0rd"),
            //    SecurityStamp = string.Empty,
            //    ConcurrencyStamp = string.Empty
            //});

            //// Assign Admin User to Admin Role
            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = "1",
            //    UserId = "1"
            //});



            base.OnModelCreating(builder);
        }
    }

}
