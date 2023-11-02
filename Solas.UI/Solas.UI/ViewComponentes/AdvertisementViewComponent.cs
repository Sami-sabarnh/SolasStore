using Microsoft.AspNetCore.Mvc;
using Solas.BL.Consts;
using Solas.BL.IRepositories;
using System;

namespace Solas.UI.ViewComponentes
{
    public class AdvertisementViewComponent: ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public AdvertisementViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
                    var data = await unitOfWork.Advertisements.GetAllAsync(x=>x.IsPublish,null,x=>x.Createdate,OrderBy.Descending);
                    return View(data.FirstOrDefault());

        }
    }
}
