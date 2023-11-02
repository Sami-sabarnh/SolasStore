using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models.SharedProp
{
    public class CommonProp
    {
        public CommonProp()
        {
            // Set IsPublish to true by default

            IsPublish = true;
        }
        public DateTime Createdate { get; set; }
        public DateTime? Modifieddate { get; set; }
        public DateTime? Deleteddate { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsPublish { get; set; }
        



    }
}
