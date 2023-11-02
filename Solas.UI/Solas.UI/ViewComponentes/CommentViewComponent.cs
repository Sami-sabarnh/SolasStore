using Microsoft.AspNetCore.Mvc;
using Solas.BL.Consts;
using Solas.BL.IRepositories;

namespace Solas.UI.ViewComponentes
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public CommentViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var data = await unitOfWork.Comments.GetAllAsync(x=>x.ProductId==id, new[] { "user" },x=>x.Createdate,OrderBy.Descending);
            ViewData["ProductId"] = id;
            return View(data);
        }
    }
}
