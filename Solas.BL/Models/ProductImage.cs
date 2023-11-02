using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class ProductImage
	{
		public int ProductImageId { get; set; }
        public string? ProductImageColor { get; set; }

        public string ProductImageUrl { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

	}
}
