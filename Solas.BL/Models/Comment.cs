using Solas.BL.Models.SharedProp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class Comment : CommonProp
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int Rate { get; set; }

        public string UserId { get; set; }

        public ApplicationUser user { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
