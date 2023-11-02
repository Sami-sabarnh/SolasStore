using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class OrderProduct
	{
        //public OrderProduct()
        //{
        //    TotalOrginal = CalculateTotalOriginalPrice();
        //    Totaldiscoun = CalculateTotalAfterDiscount();
        //}
        public int OrderProductId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser user { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public bool IsComplete { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
         public virtual ICollection<ProductOption> productOptions { get; set; }
        [Required]
        public ICollection<Option> Options { get; set; }

        public int Quintity { get; set; }
        public float CalculateTotalOriginalPrice()
        {
            if (Product != null)
            {
                return Product.ProductPrice * Quintity;
            }
            return 0;
        }
        public float CalculateTotalAfterDiscount()
        {
            if (Product == null || Product.Discount == null)
                return CalculateTotalOriginalPrice();

            float discountedPrice = Product.ProductPrice - (Product.ProductPrice * (float)Product.Discount.DiscountPrecent);
            return discountedPrice * Quintity;
        }
       
        public float Totaldiscoun
        {
            get { return CalculateTotalAfterDiscount(); }
        }

        public float TotalOrginal
        {
            get { return CalculateTotalOriginalPrice(); }
        }




    }
}
