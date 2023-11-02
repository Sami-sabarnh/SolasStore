using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class ProductInvintory
	{
		public int ProductInvintoryId { get; set; }
		public int Quintity { get; set; }


        public Product Product { get; set; }

	}
}
