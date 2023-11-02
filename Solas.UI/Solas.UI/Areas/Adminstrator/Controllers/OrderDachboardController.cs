using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;

namespace Solas.UI.Areas.Adminstrator.Controllers
{
    [Area("Adminstrator")]

    public class OrderDachboardController : Controller
    {
        private IImageService _imageService;
        private IUnitOfWork unitofwork;
        public OrderDachboardController(IImageService imageService, IUnitOfWork _unitofwork)
        {
            unitofwork = _unitofwork;
            _imageService = imageService;
        }
        public async Task<IActionResult> GetAllOrder(OrderStatus orderStatus,string search)
        {
            if(search == null)
            {
                var orders = await unitofwork.orders.GetAllAsync(X => true, new[] { "user", "Address", "Products", "Products.Product.Discount", "couponCode" });
                return View(orders);
            }
            var filter = await unitofwork.orders.GetAllAsync(x => x.Status== orderStatus, new[] { "user", "Address" });
            return View(filter);
        }
        public async Task<IActionResult> EditOrder(int Id)
        {
         var order = await unitofwork.orders.Find(X => X.OrderId==Id, new[] { "user", "Address", "Products", "Products.Product.Discount", "couponCode" });
            return View(order);

        }
     


        public async Task<JsonResult> EditStatus(OrderStatus orderStatus, int OrderId)
        {
            try
            {
                var order = await unitofwork.orders.Find(X => X.OrderId == OrderId);
                if (order != null)
                {
                    order.Status = orderStatus;
                    unitofwork.orders.Update(OrderId, order);
                    unitofwork.Complete();
                    return Json("success");
                }
                else
                {
                    return Json("Order not found");
                }
            }
            catch (Exception ex)
            {
                return Json("An error occurred: " + ex.Message);
            }
        }



    }
}
