using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class ProductOption
	{

        public int ProductOptionId { get; set; }
        public string ProductOptionName { get; set; }

        public int OptionId { get; set; }
		public Option Option { get; set; }

        public int ProductId { get; set; }
		public Product Product { get; set; }

        public int ProductGroupId { get; set; }



    }
}
