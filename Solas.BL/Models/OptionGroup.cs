using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
	public class OptionGroup
	{

        public int OptionGroupId { get; set; }
		public string OptionGroupName { get; set; }

		public virtual ICollection<Option> Options { get; set; }
        public bool MustSelected { get; set; }

    }
}
