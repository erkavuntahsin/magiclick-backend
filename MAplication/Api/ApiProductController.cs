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
    [Route("api/Product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiProductController : BaseController
    {
        IProductBL _productBL;
        public ApiProductController(IProductBL productBL)
        {
            _productBL = productBL;
        }

        [HttpGet("GetProductById", Name = "GetProductById")]
        [Authorize(Roles = "Product-View")]
        public IActionResult GetProductById(int productId)
        {
            Result<ProductDO> result = _productBL.GetById(productId);
            return Json(result);
        }

        [HttpPost("InsertProduct", Name = "InsertProduct")]
        public IActionResult InsertProduct([FromBody] ProductDO product)
        {
            Result<ProductDO> result = _productBL.Add(CacheKey, product);
            return Json(result);
        }

        [HttpPut("UpdateProduct", Name = "UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductDO product)
        {
            Result<ProductDO> result = _productBL.Update(CacheKey, product);
            return Json(result);
        }

        [HttpGet("DeleteProduct", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            Result<bool> result = _productBL.Delete(CacheKey, productId);
            return Json(result);
        }
    }
}