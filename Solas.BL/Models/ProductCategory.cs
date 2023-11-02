using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class ProductCategory
	{


		public int ProductCategoryId { get; set; }
	     public string ProductCategoryName { get; set; }
        public int CategoryId { get; set; }

        public Category category { get; set; }
        public int ProductId { get; set; }

        public Product Products { get; set; }


	}
}
