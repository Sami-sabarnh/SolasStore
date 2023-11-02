using Microsoft.AspNetCore.Identity;

namespace Solas.BL.Models
{
	public class ApplicationUser:IdentityUser
	{



        public  string FirstName { get; set; }
		public string LastName { get; set; }
      //  public bool EmailConfirmed { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderProduct> Products { get; set; }

        public Wishlist wishlist { get; set; }

        //public int AddressId { get; set; }
        public virtual ICollection<Address> addresses { get; set; }
        public virtual ICollection<Comment>  Comments { get; set; }


    }
}
