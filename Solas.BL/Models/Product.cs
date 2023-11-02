using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class Product :CommonProp
	{
		public int ProductId { get; set; }
        public string ProductName { get; set; }
		public string ProductCartDesc { get; set; }
        public string ProductShortDesc { get; set; }

		public string ProductLongDesc { get; set; }
		public float ProductPrice { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public int? DiscountId { get; set; }

        public Discount? Discount { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderProduct>? Orders { get; set; }
        public virtual ICollection<ProductWishlist> ProductWishlists { get; set; }

        public int ProductInvintoryId { get; set; }
        public ProductInvintory ProductInvintory { get; set; }
        public virtual ICollection<ProductOption> productOptions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
