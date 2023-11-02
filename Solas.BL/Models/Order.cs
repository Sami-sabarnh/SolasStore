

using Solas.BL.Models.SharedProp;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solas.BL.Models
{
    public enum OrderStatus
    {
       
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        Delivered = 5,
        NoNCompleted = 0
    }
    public class Order : CommonProp
    {


		public int OrderId { get; set; }
		public string TrackingId { get; set; }

		public float OrderAmount { get; set; }
        public float? OrderAmountAfterdiscount { get; set; }


        public virtual ICollection<OrderProduct> Products { get; set; }
        public string UserId { get; set; }
		
		public ApplicationUser user { get; set; }
        public bool IsFreeShopping { get; set; }
  
        public OrderStatus Status { get; set; }

        public int? DiscountId { get; set; }

        public Discount? Discount { get; set; }

        public string? OrderNotes { get; set; }
		public int? AddressId { get; set; }

		public Address? Address { get; set; }
        public int? CouponCodeId { get; set; }

        public CouponCode? couponCode { get; set; }
        public int? ShippingId { get; set; }

        public Shipping? Shipping { get; set; }


        public float CalculateOrderTotal()
        {
            if (Products == null)
                return 0; // If no products, total is zero

            float totalAmount = Products.Sum(orderProduct => orderProduct.TotalOrginal);

            return totalAmount;

        }
        public float CalculateOrderTotalAfterDisc()
        {
            if (Products == null)
                return 0; // If no products, total is zero

            float totalAmount = 0;

            foreach (var orderProduct in Products)
            {
                float productTotal = orderProduct.Totaldiscoun > 0
                    ? orderProduct.Totaldiscoun
                    : orderProduct.TotalOrginal;

                totalAmount += productTotal;
            }
            if (CouponCodeId != null && couponCode != null)
            {
                if (couponCode.CouponCodedisscount.HasValue)
                {
                    totalAmount -= couponCode.CouponCodedisscount.Value;
                }

                if (couponCode.CouponCodepercent.HasValue)
                {

                    totalAmount -= totalAmount * ((float)couponCode.CouponCodepercent.Value / 100);

                }
            }
            if (ShippingId != null && Shipping != null)
            {
                totalAmount += Shipping.ShippingPrice;
            }

                return totalAmount;
        }
    }
}
