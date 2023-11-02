using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;

namespace Solas.UI.ViewComponentes
{
    public class HomeSliderViewComponent :ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeSliderViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await unitOfWork.homesliders.GetAllAsync(x=>x.IsPublish==true,null);
            return View(data);
        }
    }
}
