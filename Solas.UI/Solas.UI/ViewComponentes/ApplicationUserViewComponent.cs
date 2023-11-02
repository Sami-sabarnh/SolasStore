using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using System.Security.Claims;

namespace Solas.UI.ViewComponentes
{
    public class ApplicationUserViewComponent : ViewComponent
    {

        private readonly IUnitOfWork unitOfWork;
        public ApplicationUserViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = await unitOfWork.apllicationusers.Find(x=>x.Id== userid, new[] { "addresses" }); 
            return View(data);
        }
    }
}
