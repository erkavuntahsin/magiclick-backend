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
using Microsoft.AspNetCore.Mvc;
using UI.Extentions;

namespace MAplication.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CategoryController : BaseController
    {
        ICategoryBL _categoryBL;
        IHostingEnvironment _hostingEnvironment;
        public CategoryController(ICategoryBL categoryBL, IHostingEnvironment hostingEnvironment)
        {
            _categoryBL = categoryBL;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index(int id=0)
        {
            return View(id);
        }

        public async Task<IActionResult> List(int id=0)
        {
            if (id!=0)
            {
                Result<List<CategoryDO>> result = _categoryBL.GetSubCategoriesById(id);
                result.Html = await PartialView("_List", result).ToString(ControllerContext);
                return Json(result);
            }
            else
            {
                Result<List<CategoryDO>> result = _categoryBL.GetAll();
                result.Html = await PartialView("_List", result).ToString(ControllerContext);
                return Json(result);
            }
         
        }

        public IActionResult Add()
        {
            CategoryViewModel viewModel = new CategoryViewModel();
            Result<List<CategoryDO>> categoryListResult = _categoryBL.GetAll();
            if (categoryListResult.IsSuccess)
                viewModel.CategoryList = categoryListResult.Data;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Add(CategoryViewModel model)
        {
            model.CategoryDO.CreateUserId = GetLoggedUserId();
            model.CategoryDO.UpdateUserId = GetLoggedUserId();
            Result<CategoryDO> result = _categoryBL.Add(CacheKey,model.CategoryDO);
            return Json(result);
        }

        public IActionResult Edit(int id)
        {
            Result<CategoryViewModel> result;
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            Result<CategoryDO> categoryResult = _categoryBL.GetById(id);

            if (categoryResult.IsSuccess)
            {
                Result<List<CategoryDO>> categoryList = _categoryBL.GetAll();
                if (categoryList.IsSuccess)
                {
                    categoryViewModel.CategoryDO = categoryResult.Data;
                    categoryViewModel.CategoryList = categoryList.Data;
                    result = new Result<CategoryViewModel>(categoryViewModel);
                }
                else
                {
                    result = new Result<CategoryViewModel>(categoryList.IsSuccess, categoryList.ResultType, "An error occured" + " " + categoryList.Message);
                }

            }
            else
            {
                result = new Result<CategoryViewModel>(categoryResult.IsSuccess, categoryResult.ResultType, "An error occured" + " " + categoryResult.Message);
            }
            
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Result<CategoryViewModel> model)
        {
            model.Data.CategoryDO.UpdateUserId = GetLoggedUserId();
            Result<CategoryDO> result = _categoryBL.Update(CacheKey,model.Data.CategoryDO);
            return Json(result);
        }

        public IActionResult Delete(int id)
        {
          Result<bool> result=  _categoryBL.Delete(CacheKey,id);
            return Json(result);
        }
        
    }
}