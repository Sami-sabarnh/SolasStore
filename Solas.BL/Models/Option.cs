using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class Option
	{

        public int OptionId { get; set; }
		public string OptionName { get; set; }
        public int OptionGroupId { get; set; }
		public OptionGroup OptionGroup { get; set; }

        public ICollection<OrderProduct> orderProducts { get; set; }

        public ICollection<ProductOption> productOptions { get; set; }
	     }
}
