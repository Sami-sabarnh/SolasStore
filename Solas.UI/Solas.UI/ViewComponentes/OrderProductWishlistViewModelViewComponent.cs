using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.UI.Models.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace Solas.UI.ViewComponentes
{
    public class OrderProductWishlistViewModelViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        private SignInManager<ApplicationUser> _signInManager;

        public OrderProductWishlistViewModelViewComponent(IUnitOfWork _unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager=signInManager;
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn((ClaimsPrincipal)User))
            {

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId))
                {
                    var orderproductdata = await unitOfWork.orderproducts.GetAllAsync(x => x.UserId == userId && x.IsComplete == false, new[] { "Product" });
                   // var orderproductdata = await dataTask;
                    // var total = data.Sum(item => item.TotalOrginal);
                    float total = 0;
                    if (orderproductdata != null && orderproductdata.Any())
                    {
                        total =  orderproductdata.Sum(zz => zz.TotalOrginal);
                    }
					else
					{
						total = 0;
					}
					var  Wishlistdata = await unitOfWork.wishlists.Find(x => x.UserId == userId, new[] { "Products" });
                    var viewModel = new OrderProductWishlistViewModel
                    {
                        orderProducts = orderproductdata,
                        wishlists = Wishlistdata,
                        TotalOriginal = total
                    };

                    return View(viewModel);
                }
            }
            var viewModell = new OrderProductWishlistViewModel
            {
                orderProducts = null,
                wishlists = null,
                TotalOriginal = 0
            };
            var model = new OrderProductWishlistViewModel();
            return View(model);
        }
    }
}
