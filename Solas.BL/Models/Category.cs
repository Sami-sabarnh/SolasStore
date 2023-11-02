using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class Category: CommonProp
	{
		
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

	}
}
