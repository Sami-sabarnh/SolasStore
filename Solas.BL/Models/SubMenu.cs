using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.Models
{
    public class SubMenu
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string ActionNane { get; set; }
        public string ControllerName { get; set; }
        public int MenuId { get; set; }

        public Menu? Menu { get; set; }
    }
}
