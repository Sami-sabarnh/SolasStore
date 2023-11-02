using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF.Repositories;
using System;

namespace Solas.UI.ViewComponentes
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryViewComponent(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
        public async Task<IViewComponentResult> InvokeAsync(string action)
        {
           
            switch (action.ToLower())
            {
                case "list":
                    return await GetListCategoury();
                case "banner":
                    return await GetALLCategouryAsync();
                
                default:
                    throw new ArgumentException("Invalid action.");
            }
        }
        private async Task<IViewComponentResult> GetALLCategouryAsync()
        {

            var data = await unitOfWork.categorys.GetAllAsync(p => true, new[] { "ProductCategories" });
            return View("Banner",data);
        }
        private async Task<IViewComponentResult> GetListCategoury()
        {

            var data = await unitOfWork.categorys.GetAllAsync(p => true, new[] { "ProductCategories" });
            return View(data);
        }
    }
}
