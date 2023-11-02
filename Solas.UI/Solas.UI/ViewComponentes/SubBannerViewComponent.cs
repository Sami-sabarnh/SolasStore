using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;

namespace Solas.UI.ViewComponentes
{
    public class SubBannerViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public SubBannerViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await unitOfWork.subbanner.GetAllAsync(x => x.IsPublish,null);
            return View(data);
        }
    }
}
