using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;

namespace Solas.UI.Areas.Adminstrator.Controllers
{
    [Area("Adminstrator")]

    public class UsersController : Controller
    {
        private IImageService _imageService;
        private IUnitOfWork unitofwork;
        public UsersController(IImageService imageService, IUnitOfWork _unitofwork)
        {
            unitofwork = _unitofwork;
            _imageService = imageService;


        }
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await unitofwork.apllicationusers.GetAllAsync();
            return View(data);
        }
    }
}
