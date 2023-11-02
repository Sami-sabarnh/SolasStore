using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF.Repositories;
using Solas.UI.Models.ViewModels;
using System.Security.Claims;

namespace Solas.UI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitofwork;
        private SignInManager<ApplicationUser> _signInManager;

        public HomeController(IUnitOfWork _unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            unitofwork = _unitOfWork;

        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AllProducts()
        {
            var products = await unitofwork.products.GetAllAsync(p => p.IsPublish, new[] { "ProductImages", "Discount" });
            var options = await unitofwork.options.GetAllAsync();
            var optionGroupss = await unitofwork.optiongroups.GetAllAsync(p => true, new[] { "Options" });
            var categories = await unitofwork.categorys.GetAllAsync();

            AllProdectViewModel allProdectViewModel = new AllProdectViewModel()
            {
                products = products,
                options = options,
                optionGroups = optionGroupss,
                categories = categories

            };
            return View(allProdectViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AllProducts(List<int> selectedOptions)
        {
            if (selectedOptions.Count == 0)
            {
                return RedirectToAction(nameof(AllProducts));
            }

            // Retrieve products based on selected options
            var products = await unitofwork.products.GetAllAsync(p => p.productOptions.Any(po => selectedOptions.Contains(po.OptionId)), new[] { "ProductImages", "Discount" });
            //var products = await unitofwork.products.GetAllAsync(p => selectedOptions.All(optionId => p.productOptions.Any(po => po.OptionId == optionId)),new[] { "ProductImages", "Discount" });
            // Retrieve other necessary data (options, option groups, categories)
            var options = await unitofwork.options.GetAllAsync();
            var optionGroups = await unitofwork.optiongroups.GetAllAsync(p => true, new[] { "Options" });
            var categories = await unitofwork.categorys.GetAllAsync();

            AllProdectViewModel allProdectViewModel = new AllProdectViewModel()
            {
                products = products,
                options = options,
                optionGroups = optionGroups,
                categories = categories
            };

            return View(allProdectViewModel);
        }
        public async Task<IActionResult> SearchByCategory(int id)
        {

            if (id == 0)
            {
                return RedirectToAction(nameof(AllProducts));
            }

            // Retrieve products based on selected options
            var products = await unitofwork.products.GetAllAsync(p => p.ProductCategories.Any(o => o.CategoryId == id), new[] { "ProductImages", "Discount" });

            // Retrieve other necessary data (options, option groups, categories)
            var options = await unitofwork.options.GetAllAsync();
            var optionGroups = await unitofwork.optiongroups.GetAllAsync(p => true, new[] { "Options" });
            var categories = await unitofwork.categorys.GetAllAsync();

            AllProdectViewModel allProdectViewModel = new AllProdectViewModel()
            {
                products = products,
                options = options,
                optionGroups = optionGroups,
                categories = categories
            };

            return View("AllProducts", allProdectViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SearchByProduct(string text)
        {

            if (text == null)
            {
                return RedirectToAction(nameof(AllProducts));
            }

            // Retrieve products based on selected options
            var products = await unitofwork.products.GetAllAsync(p=> p.ProductName.Contains(text)
            ||p.ProductShortDesc.Contains(text)|| p.ProductLongDesc.Contains(text), new[] { "ProductImages", "Discount" });

            // Retrieve other necessary data (options, option groups, categories)
            var options = await unitofwork.options.GetAllAsync();
            var optionGroups = await unitofwork.optiongroups.GetAllAsync(p => true, new[] { "Options" });
            var categories = await unitofwork.categorys.GetAllAsync();

            AllProdectViewModel allProdectViewModel = new AllProdectViewModel()
            {
                products = products,
                options = options,
                optionGroups = optionGroups,
                categories = categories
            };

            return View("AllProducts", allProdectViewModel);
        }

        public async Task<IActionResult> DetailsProducts(int Id)
        {
            ViewBag.OptionGroups = await unitofwork.optiongroups.GetAllAsync();
            var data = await unitofwork.products.Find(x => x.ProductId == Id, new[] { "ProductImages", "Discount", "productOptions.Option" });
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (_signInManager.IsSignedIn(User))
            {


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (comment == null)
                {
                    return BadRequest();
                }
                //if (ModelState.IsValid)
                //{
                comment.UserId = userId;
                comment.Createdate = DateTime.Now;
                await unitofwork.Comments.AddAsync(comment);
                unitofwork.Complete();

                return RedirectToAction("DetailsProducts", "Home", new { Id = comment.ProductId });
            }
            return RedirectToAction("DetailsProducts", "Home", new { Id = comment.ProductId });

        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
