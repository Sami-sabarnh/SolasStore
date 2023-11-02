using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF.Repositories;
using System;

namespace Solas.Web.Components
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductViewComponent(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(string action)
        {
            switch (action.ToLower())
            {
                case "allproducts":
                    return await GetBestsellerproducts();
                case "related":
                    return await GetRelatedProducts();

                default:
                    return await GetALLproductsAsync();
            }
        }
        private async Task<IViewComponentResult> GetALLproductsAsync()
        {
            //not used yet

            var data = await unitOfWork.categorys.GetAllAsync(p => true, new[] { "ProductImages", "Discount" });
            return View(data);
        }
        private async Task<IViewComponentResult> GetBestsellerproducts()
        {

            var data = await unitOfWork.products.GetAllAsync(p => p.IsPublish, new[] { "ProductImages", "Discount" });
            return View("HomeListProduct", data);
        }
        private async Task<IViewComponentResult> GetRelatedProducts()
        {

            var data = await unitOfWork.products.GetAllAsync(p => p.IsPublish, new[] { "ProductImages", "Discount" });
            return View("RelatedProducts",data);
        }
    }
}
