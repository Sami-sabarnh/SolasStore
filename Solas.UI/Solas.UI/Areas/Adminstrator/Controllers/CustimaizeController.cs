using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Solas.BL.IRepositories;
using Solas.BL.Models;

namespace Solas.UI.Areas.Adminstrator.Controllers
{

    [Area("Adminstrator")]
    public class CustimaizeController : Controller
    {
        private IImageService _imageService;
        private IUnitOfWork unitofwork;
        public CustimaizeController(IImageService imageService, IUnitOfWork _unitofwork)
        {
            unitofwork = _unitofwork;
            _imageService = imageService;


        }
        public IActionResult AddHomeSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHomeSlider(HomeSlider slider, IFormFile HomeSliderimage)
        {
            if (slider != null)
            {
                
             

                string data = _imageService.AddImage(HomeSliderimage);
                slider.UrlImage = data;
                unitofwork.homesliders.Add(slider);
                unitofwork.Complete();

                return RedirectToAction("Index", "Dachboard");
               
            }
            return View(slider);
        }
        public IActionResult AddSubBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSubBanner(SubBanner subBanner, IFormFile SubBannerimage)
        {
            if (subBanner != null)
            {
                string url = _imageService.AddImage(SubBannerimage);
                subBanner.UrlImage = url;
                subBanner.Createdate = DateTime.Now;
               ModelState.Clear();
                if (ModelState.IsValid)
                {
                  
                    unitofwork.subbanner.Add(subBanner);
                    unitofwork.Complete();
                    return RedirectToAction("Index", "Dachboard");

                }
                return View(subBanner);
            }
            return View(subBanner);
        }
        public IActionResult AddMenu()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMenu(Menu menu)
        {
            if (ModelState.IsValid)
            {
                unitofwork.menus.Add(menu);
                unitofwork.Complete();
            }
            return View(menu);
        }
        public IActionResult AddSubMenu()
        {
            var menus =unitofwork.menus.GetAll().ToList();
            var menuItems = menus.Select(x => new SelectListItem
            {
                Value = x.MenuId.ToString(), 
                Text = x.MenuName             
            });
          
           ViewBag.Menus = menuItems;
            return View();

        }
        [HttpPost]

        public IActionResult AddSubMenu(SubMenu submenu)
        {
            if (ModelState.IsValid)
            {
                unitofwork.submenus.Add(submenu);
                unitofwork.Complete();
                return View();
            }
            var menus = unitofwork.menus.GetAll().ToList();
            var menuItems = menus.Select(x => new SelectListItem
            {
                Value = x.MenuId.ToString(),
                Text = x.MenuName
            });

            ViewBag.Menus = menuItems;
            return View();
        }
        public IActionResult AddAds()
        {
          
            return View();

        }
        [HttpPost]
        public IActionResult AddAds(Advertisement advertisement)
        {

            if (ModelState.IsValid)
            {
                advertisement.Createdate = DateTime.Now;
                unitofwork.Advertisements.Add(advertisement);
                unitofwork.Complete();
                return RedirectToAction("Index", "Dachboard");
            }

            return View();

        }
    }
}
