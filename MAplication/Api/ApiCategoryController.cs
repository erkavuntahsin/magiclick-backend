using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ResultType;
using Entities.MApplication;
using Interfaces.Business;
using MAplication.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MAplication.Api
{
    [Produces("application/json")]
    [Route("api/apiCategory")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiCategoryController : BaseController
    {
        ICategoryBL _categoryBL;
        public ApiCategoryController(ICategoryBL category)
        {
            _categoryBL = category;
        }

        [HttpGet("GetCategoryById", Name = "GetCategoryById")]
        public IActionResult GetCategoryById(int categoryId)
        {
            Result<CategoryDO> result =_categoryBL.GetById(categoryId);
            return Json(result);
        }

        [HttpPost("InsertCategory", Name = "InsertCategory")]
        public IActionResult InsertCategory([FromBody] CategoryDO category)
        {
            Result<CategoryDO> result = _categoryBL.Add(CacheKey,category);
            return Json(result);
        }

        [HttpPut("UpdateCategory", Name = "UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] CategoryDO category)
        {
            Result<CategoryDO> result = _categoryBL.Update(CacheKey, category);
            return Json(result);
        }

        [HttpGet("DeleteCategory", Name = "DeleteCategory")]
        public IActionResult DeleteCategory(int categoryId)
        {
            Result<bool> result = _categoryBL.Delete(CacheKey,categoryId);
            return Json(result);
        }
    }
}