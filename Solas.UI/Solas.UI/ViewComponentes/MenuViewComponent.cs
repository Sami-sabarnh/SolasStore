using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;

namespace Solas.UI.ViewComponentes
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public MenuViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await unitOfWork.menus.GetAllAsync(x => true, new[] { "subMenus" });
            return View(data);
        }
    }
}
