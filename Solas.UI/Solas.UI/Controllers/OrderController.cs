using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.UI.Models.ViewModels;
using System.Security.Claims;

namespace Solas.UI.Controllers
{
    public class OrderController : Controller
    {
        private IUnitOfWork unitofwork;
        private SignInManager<ApplicationUser> signInManager;

        public OrderController(IUnitOfWork _unitOfWork, SignInManager<ApplicationUser> _signInManager)
        {
            signInManager = _signInManager;
            unitofwork = _unitOfWork;

        }

        public async Task<IActionResult> AddWishlist(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var product = await unitofwork.products.GetByIdAsync(id);
                if (product == null || string.IsNullOrEmpty(userId))
                {
                    return BadRequest();
                }
                if (product != null)
                {
                    var data = await unitofwork.wishlists.Find(x => x.UserId == userId);

                    if (data != null )
                    {
                        var productWishlistsold = await unitofwork.productWishlists.Find(x => x.ProductId == id && data.WishlistId == x.WishlistId);
                        if(productWishlistsold == null)
                        {

                      
                        ProductWishlist productWishlist = new ProductWishlist()
                        {
                            ProductId = id,
                            WishlistId = data.WishlistId

                        };
                        //this line adding new
                        data.WishlistAmount += product.ProductPrice;
                        unitofwork.productWishlists.Add(productWishlist);
                        unitofwork.Complete();
                        } 

                    }
                    else if (data == null )
                    {


                        Wishlist wishlist = new()
                        {
                            UserId = userId,
                            Products = new List<ProductWishlist> { new ProductWishlist { ProductId = id } },
                            WishlistAmount = product.ProductPrice
                        };
                        unitofwork.wishlists.Add(wishlist);
                        unitofwork.Complete();
                    }
                    else
                    {
                        return RedirectToAction("WishList");

                    }
                 

                    return RedirectToAction("WishList");

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public async Task<IActionResult> WishList()
        {
            if (signInManager.IsSignedIn(User))
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var data = await unitofwork.wishlists.Find(x => x.UserId == userid, new[] { "Products", "Products.Product.ProductImages" });
                return View(data);

            }
            return RedirectToAction("Login", "Account");


        }



        public async Task<IActionResult> DeleteWishList(int ProductId)
        {
            if (ProductId == 0)
            {
                return BadRequest("id is null");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentwishlists = await unitofwork.wishlists.Find(x => x.UserId == userId);

            var productOrder = await unitofwork.productWishlists.Find(x => x.WishlistId == currentwishlists.WishlistId &&  x.ProductId == ProductId );
            
            unitofwork.productWishlists.Delete(x => x.ProductId == ProductId);
            unitofwork.Complete();


            return RedirectToAction(nameof(WishList));

        }


        [HttpPost]
        public async Task<IActionResult> AddCart(OrderProduct orderproduct, int ProductId, List<int> OptionsId)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (orderproduct == null)
                {
                    orderproduct = new OrderProduct(); // Initialize if it's null
                }

                if (orderproduct.Options == null)
                {
                    orderproduct.Options = new List<Option>(); // Initialize as an empty list if it's null
                }

                foreach (var item in OptionsId)
                {
                    var option = await unitofwork.options.Find(x => x.OptionId == item);
                    if (option != null)
                    {
                        orderproduct.Options.Add(option);
                    }
                }

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                orderproduct.UserId = userid;
                orderproduct.ProductId = ProductId;

                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    unitofwork.orderproducts.Add(orderproduct);
                    unitofwork.Complete();
                    return RedirectToAction("Cart");
                }
                return RedirectToAction("DetailsProducts", "Home", new { id = orderproduct.ProductId });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            if (signInManager.IsSignedIn(User))
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var data = await unitofwork.orderproducts.Findbyid(x => x.UserId == userid&&x.IsComplete==false, new[] { "Product", "Product.ProductImages", "Product.Discount" , "Options" });
                if (data == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(data);

            }
            return RedirectToAction("Login", "Account");

        }
        public async Task<IActionResult> DeleteOrder(int orderProductId)
        {
            if (orderProductId == 0)
            {
                return BadRequest("id is null");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var productOrder = await unitofwork.orderproducts.Find(x => x.OrderProductId == orderProductId && x.UserId == userId);
            if (productOrder == null)
            {
                return NotFound("Product order not found");
            }
            unitofwork.orderproducts.Delete(productOrder.OrderProductId);
            unitofwork.Complete();


            return RedirectToAction(nameof(Cart));

        }

        public async Task<IActionResult> Checkout()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid == null)
                {
                    return BadRequest("user id is null");
                }
                var orderproducts = await unitofwork.orderproducts.Findbyid(x => x.UserId == userid && x.IsComplete == false);
                var currentorder = await unitofwork.orders.Find(x => x.UserId == userid&&x.Status ==0);
               if (currentorder != null) {
                    currentorder.Products=orderproducts.ToList();
                    currentorder.Modifieddate = DateTime.Now;
                    unitofwork.orders.Update(currentorder.OrderId, currentorder);
                    unitofwork.Complete();
                    ViewBag.Addresses = await unitofwork.address.Findbyid(x => x.UserId == userid);

                    return View();

                }
				//string trackingNumber = "#solas" + new Random().Next(1000 * 1000 * 1000, 1000 * 1000 * 1000 * 10).ToString();

				string trackingNumber = "#solas" + Guid.NewGuid().ToString();
				Order order = new()
                {
                    UserId = userid,
                    Products = orderproducts.ToList(),
                    TrackingId=trackingNumber,
                   Createdate= DateTime.Now
                    
                };
                if (ModelState.IsValid)
                {
                    unitofwork.orders.Add(order);
                    unitofwork.Complete();
                    ViewBag.Addresses = await unitofwork.address.Findbyid(x => x.UserId == userid);
                    return View();
                }

                //   var data = 
                ViewBag.Addresses = await unitofwork.address.Findbyid(x => x.UserId == userid);
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
		public async Task<IActionResult> Trackinginput()
		{
			return View();
		}
        [HttpPost]
		public async Task<IActionResult> Tracking(string trackingid)
        {
            var ordertrack = await unitofwork.orders.Find(x => x.TrackingId == trackingid);
            if (ordertrack == null)
            {
                return View("Error", new { Error = "Tracking Number Is Not Found!" });
            }
            return View(ordertrack);
        }


        [HttpPost]
		public async Task<IActionResult> CompleteOrder(Order order)
        {
            if (signInManager.IsSignedIn(User))
            {
				//if (selectedAddress == 0)
				//{
				//	return RedirectToAction("Checkout");
				//}
				var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid == null)
                {
                    return BadRequest("user id is null");
                }
                var orderproducts = await unitofwork.orderproducts.GetAllAsync(x => x.UserId == userid && x.IsComplete == false);
                foreach (var item in orderproducts)
                {
                  item.IsComplete = true;
                }

                var currentorder = await unitofwork.orders.Find(x => x.UserId == userid && x.Status ==0);
                currentorder.Status = OrderStatus.Pending;

                currentorder.OrderNotes = order.OrderNotes;
                unitofwork.orders.Update(currentorder.OrderId,currentorder);
                unitofwork.Complete();
                return View(currentorder);
            }
			return RedirectToAction("Login", "Account");

		}
        [HttpPost]
        public async Task<IActionResult> AddAddressOrder( int selectedAddress)
        {
            bool isValid = false;

            if (selectedAddress != 0)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentorder = await unitofwork.orders.Find(x => x.UserId == userid && x.Status == 0);

                if (currentorder != null)
                {
                    currentorder.AddressId = selectedAddress;
                    unitofwork.orders.Update(currentorder.OrderId, currentorder);
                    unitofwork.Complete();
                    isValid = true;
                }

                return Json(new { isValid });
            }

            return Json(new { isValid });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAllQuantities([FromBody] List<QuantityUpdateViewModel> quantities)
        {
            try
            {
                foreach (var quantityUpdate in quantities)
                {
                    var orderProduct = await unitofwork.orderproducts.Find(x => x.OrderProductId == quantityUpdate.OrderProductId);

                    if (orderProduct != null && orderProduct.ProductId == quantityUpdate.ProductId)
                    {
                        orderProduct.Quintity = quantityUpdate.Quantity;
                        unitofwork.orderproducts.Update(orderProduct.OrderProductId, orderProduct);
                    }
                }

                // Save changes and return a success response
                unitofwork.Complete();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckCoupon(string couponCode)
        {
            var coupon = await unitofwork.couponcodes.Find(c => c.CouponCodeText == couponCode && c.IsActive);

            bool isValid;
            if (coupon != null)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var order =await  unitofwork.orders.Find(x => x.UserId == userid&&x.Status==0);
                     if (order != null)
                {
                    order.CouponCodeId = coupon.CouponCodeId;
                    order.couponCode = coupon;
                    unitofwork.orders.Update(order.OrderId, order);
                    unitofwork.Complete();
                }
                isValid = true;
            }
            else
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var order = await unitofwork.orders.Find(x => x.UserId == userid && x.Status == 0);
                if (order.CouponCodeId != null)
                {
                    order.CouponCodeId = null;
                    unitofwork.orders.Update(order.OrderId, order);
                    unitofwork.Complete();
                }
                    isValid = false;
            }
            return Json(new { isValid });
        }


    }
}
