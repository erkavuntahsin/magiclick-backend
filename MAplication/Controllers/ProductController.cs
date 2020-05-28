using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResultType;
using Entities.MApplication;
using Entities.ViewModels;
using Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Extentions;

namespace MAplication.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProductController : BaseController
    {
        IProductBL _productBL;
        ICategoryBL _categoryBL;
        IHostingEnvironment _hostingEnvironment;
        public ProductController(IProductBL productBL, ICategoryBL categoryBL, IHostingEnvironment hostingEnvironment)
        {
            _productBL = productBL;
            _categoryBL = categoryBL;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            Result<List<ProductDO>> result = _productBL.GetAll();
            result.Html = await PartialView("_List", result).ToString(ControllerContext);
            return Json(result);
        }

        public IActionResult Add()
        {
            ProductViewModel viewModel = new ProductViewModel();
            Result<List<CategoryDO>> categoryList = _categoryBL.GetAll();
            if (categoryList.IsSuccess)
                viewModel.CategoryList = categoryList.Data;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model, IFormFile pic)
        {
            Result<ProductDO> result;
            ImageExtentions imageHelper = new ImageExtentions(_hostingEnvironment);

            model.ProductDO.ImageUrl =await imageHelper.ImageUploader(pic, "Cdn\\"); ;
            model.ProductDO.CreateUserId = GetLoggedUserId();
            model.ProductDO.UpdateUserId = GetLoggedUserId();
            result = _productBL.Add(CacheKey,model.ProductDO);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(int id)
        {
            ProductViewModel viewModel = new ProductViewModel();
            Result<List<CategoryDO>> categoryListResult = _categoryBL.GetAll();
            Result<ProductDO> productResult = _productBL.GetById(id);
            if (productResult.IsSuccess)
                viewModel.ProductDO = productResult.Data;
            if (categoryListResult.IsSuccess)
                viewModel.CategoryList = categoryListResult.Data;
          
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model, IFormFile pic)
        {
            ImageExtentions imageHelper = new ImageExtentions(_hostingEnvironment);
            model.ProductDO.ImageUrl = await imageHelper.ImageUploader(pic, "Cdn\\"); ;
            model.ProductDO.UpdateUserId = GetLoggedUserId();
            _productBL.Update(CacheKey,model.ProductDO);
            return RedirectToAction("Index", "Product");
        }

        [Route("[Controller]/{easyUrl}/{id:int}")]
        [Authorize(Roles = "Product-View")]
        public IActionResult ProductDetail(string easyUrl, int id)
        {
            ProductDO model = new ProductDO();
            Result<ProductDO> productResult = _productBL.GetById(id);
            if (productResult.IsSuccess)
                model = productResult.Data;
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Result<bool> result = _productBL.Delete(CacheKey,id);
            return Json(result);
        }
    }
}