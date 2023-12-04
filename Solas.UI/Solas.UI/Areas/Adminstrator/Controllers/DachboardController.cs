using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Solas.BL;
using Solas.BL.IRepositories;
using Solas.BL.Models;

namespace Solas.UI.Areas.Adminstrator.Controllers
{
    [Area("Adminstrator")]
    public class DachboardController : Controller
    {
        private IImageService _imageService;
        private IUnitOfWork unitofwork;
        public DachboardController(IImageService imageService, IUnitOfWork _unitofwork)
        {
            unitofwork = _unitofwork;
            _imageService = imageService;


        }
       
        #region Product
        public IActionResult Index()
        {
           
            return RedirectToAction(nameof(GetAllProducts));

        }
        public async Task<IActionResult> GetAllProducts()
        {
            var data = await unitofwork.products.GetAllAsync(x=>true,new[] { "ProductInvintory", "Discount" });
            return View(data);
        }
        public async Task<IActionResult> GetAllProductsIsPublish()
        {
            var data = await unitofwork.products.GetAllAsync(x => x.IsPublish, new[] { "ProductInvintory", "Discount" });
            return View("GetAllProducts", data);
        }
        public async Task<IActionResult> GetAllProductsNonPublish()
        {
            var data = await unitofwork.products.GetAllAsync(x => x.IsPublish ==false , new[] { "ProductInvintory", "Discount" });
            return View("GetAllProducts",data);
        }
        public async Task<IActionResult> GetAllProductsDeleted()
        {
            var data = await unitofwork.products.GetAllAsync(x=> x.IsDeleted == true, new[] { "ProductInvintory", "Discount" });
            return View("GetAllProducts", data);
        }
        public IActionResult CreateProduct()
        {
            ViewBag.discounts = unitofwork.discounts.GetAll();

            ViewBag.OptionGroups = unitofwork.optiongroups.GetAll(new[] { "Options" });
			ViewBag.categorys = unitofwork.categorys.GetAll();


			return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, List<IFormFile> images, List<int> categorys, List<int> selectedOptions)
        {
            if (product == null)
            {
                return View(product);

            }
            product.Createdate = DateTime.Now;
            unitofwork.products.Add(product);
            unitofwork.products.SaveData();


            ProductInvintory invintory = new ProductInvintory()
            {
                Quintity = product.ProductInvintory.Quintity,
                Product = product
            };
           
            product.ProductInvintoryId = invintory.ProductInvintoryId;
            product.ProductInvintory = invintory;

     


            List<ProductImage> productImages = new List<ProductImage>();
            if (images != null)
            {
                foreach (var image in images)
                {
                    string fileName = _imageService.AddImage(image);

                    ProductImage productImage = new()
                    {
                        ProductImageUrl = fileName,
                        ProductId = product.ProductId
                    };

                    productImages.Add(productImage);
                }

                product.ProductImages = productImages;
            }
         

            if (selectedOptions != null)
            {

                for (int i = 0; i < selectedOptions.Count; i++)
                {
                    int optionId = selectedOptions[i];
                    var option = await unitofwork.options.GetByIdAsync(selectedOptions[i]);
                    ProductOption productOption = new ProductOption
                    {
                        ProductOptionName=option.OptionName,
                        ProductGroupId = option.OptionGroupId,
                        ProductId = product.ProductId,
                        OptionId = optionId
                    };

                    unitofwork.productoptions.Add(productOption);
                    // product.productOptions.Add(productOption);
                }
            }


            for (int i = 0; i < categorys.Count; i++)
            {


                ProductCategory productCategory = new ProductCategory
                {
                    ProductCategoryName = product.ProductName,
                    CategoryId = categorys[i],
                    ProductId = product.ProductId


                };
                unitofwork.productcategorys.Add(productCategory);
                //    product.ProductCategories.Add(productCategory);

              
            }
           
            unitofwork.Complete();
            return RedirectToAction("Index");




        }


        public async Task<IActionResult> DetailsProduct(int Id)
        {
            ViewBag.OptionGroups = await unitofwork.optiongroups.GetAllAsync();
            var data = await unitofwork.products.Find(x => x.ProductId == Id, new[] { "productOptions.Option", "ProductInvintory", "ProductImages", "Discount" });
            return View(data);
        }
        public  IActionResult DeleteProduct(int Id)
        {
            if(Id == 0)
            {
                return View("Error");
            }
             unitofwork.products.Delete(x => x.ProductId == Id);
            return RedirectToAction("GetAllProducts");
        }
        #endregion
        #region Category
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category categoryy, IFormFile Categoryimage)
        {

          string data =  _imageService.AddImage(Categoryimage);
            categoryy.ProductImageUrl = data;
            categoryy.Createdate = DateTime.Now;
            unitofwork.categorys.Add(categoryy);
            unitofwork.Complete();

            return RedirectToAction("AllCategory");
        }
        public IActionResult DeleteCategory(int id)
        {
            unitofwork.categorys.Delete(id);
            return RedirectToAction("AllCategory");
        }
        public IActionResult EditCategory(int id)
        {
            unitofwork.categorys.GetByIdAsync(id);
            return View(unitofwork.categorys.GetByIdAsync(id));
        }

        [HttpPost]
        public IActionResult EditCategory(int id, Category category)
        {
            unitofwork.categorys.Update(id, category);
            return RedirectToAction("AllCategory");
        }
        public IActionResult AllCategory()
        {
            //    return RedirectToAction("Privacy", "Home" ,new { area = "" });
            var data = unitofwork.categorys.GetAll();
            return View(data);
        }
        #endregion
        #region OptionGroup
        public IActionResult AllOptionGroup()
        {
            var data = unitofwork.optiongroups.GetAll();
            return View(data);

        }
        public IActionResult CreateOptionGroup()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateOptionGroup(OptionGroup optionGroup)
        {
            if (optionGroup == null)
            {
                return View(optionGroup);
            }

            unitofwork.optiongroups.Add(optionGroup);
            unitofwork.Complete();

            return RedirectToAction("AllOptionGroup");

        }
        #endregion
        #region Options
        public IActionResult AllOption()
        {
            var data = unitofwork.options.GetAll();
            return View(data);

        }
        public IActionResult CreateOption()
        {
            ViewBag.OptionGroup = unitofwork.optiongroups.GetAll().Select(c => new SelectListItem
            {
                Value = c.OptionGroupId.ToString(),
                Text = c.OptionGroupName
            }).ToList();
            return View();

        }
        [HttpPost]
        public IActionResult CreateOption(Option option)
        {
            if (option == null)
            {
                return View(option);
            }

            unitofwork.options.Add(option);
            unitofwork.Complete();

            return RedirectToAction("AllOption");

        }
        #endregion
        #region Discount

        public IActionResult AddDiscount()
        {
            return View();

        }
        [HttpPost]
        public IActionResult AddDiscount(Discount discount)
        {
            if (discount == null)
            {
                return View(discount);
            }
            if (ModelState.IsValid)
            {



                unitofwork.discounts.Add(discount);
                unitofwork.Complete();

                return RedirectToAction("Index");
            }
            return View(discount);

        }
        #endregion

    }

}

