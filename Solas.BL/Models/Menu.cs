namespace Solas.BL.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string? AreaName { get; set; }

        public bool AdminMenu { get; set; }
        public bool AdminJustShow { get; set; }
        public virtual ICollection<SubMenu>? subMenus { get; set; }

    }
}
