using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF.Repositories;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Solas.UI.ViewComponentes
{
    public class OrderViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        private SignInManager<ApplicationUser> signInManager;

        public OrderViewComponent(IUnitOfWork _unitOfWork, SignInManager<ApplicationUser> _signInManager)
        {
            signInManager = _signInManager;

            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync(string action)
        {



            switch (action.ToLower())
            {
                case "header":
                    return await Getheader();
                case "checkout":
                    return await CheckoutAsync();

                default:
                    throw new ArgumentException("Invalid action.");
            }




          


        }
        private async Task<IViewComponentResult> CheckoutAsync()
        {
          
                var userid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var data = await unitOfWork.orders.Find(x => x.UserId == userid&& x.Status==0, new[] { "Products", "Products.Product.Discount", "couponCode", "Address" });
            if (data.Address?.City != null)
            {
                var shipping = await unitOfWork.shippings.Find(x => x.City == data.Address.City);

                if (shipping != null)
                {
                    data.ShippingId = shipping.ShippingId;
                    data.Shipping = shipping;

                    if (data.ShippingId != null)
                    {
                        unitOfWork.orders.Update(data.OrderId, data);
                        unitOfWork.Complete();
                        ViewBag.Shipping = shipping.ShippingPrice;
                    }

                }
                else
                {
                    ViewBag.Shipping = 5;
                }
            }
            else
            {
                ViewBag.Shipping = 5;

            }
            return View("CheckOut",data);

         
        }
        private async Task<IViewComponentResult> Getheader()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                // User is signed in, proceed to get user's data
                var data = await unitOfWork.orderproducts.Find(x => x.UserId == userId&&x.IsComplete==false , new[] { "Product" });

                if (data == null)
                {
                    ViewBag.Wishlist = await unitOfWork.wishlists.Find(x => x.UserId == userId, new[] { "Products" });
                    return View("Default2");
                }
                ViewBag.Wishlist = await unitOfWork.wishlists.Find(x => x.UserId == userId, new[] { "Products" });

                return View("Default2",data);
            }

            return View("Default2");
        }
    }
}

