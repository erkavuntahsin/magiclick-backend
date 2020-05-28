using AutoMapper;
using CacheLayer.Attributes;
using CacheLayer.Entities;
using Core.ResultType;
using DataAccessLayer.Models;
using Entities.MApplication;
using Interfaces.Business;
using Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class ProductBL : IProductBL
    {
        public const string PRODUCT_CACHE_AREA = "PRODUCT_AREA.{";
        public const string PRODUCT_CACHE_KEY = "PRODUCT";

        ICategoryService _categoryService;
        IProductService _productService;

        public ProductBL(ICategoryService categoryService,IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [CacheInvalidate(CacheArea = PRODUCT_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<ProductDO> Add(string CacheKey,ProductDO model)
        {
            Result<ProductDO> result;
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                Product entity = Mapper.Map<ProductDO, Product>(model);
                _productService.Add(entity);
                model.Id = entity.Id;
                result = new Result<ProductDO>(true, ResultTypeEnum.Success, model, "Product Was Successfully Saved");
            }
            catch (Exception ex)
            {

                result = new Result<ProductDO>(false, ResultTypeEnum.Error, $"An Error Occured When Adding Operation Ex : {ex.ToString()}");
            }
            return result;
        }

        [CacheInvalidate(CacheArea = PRODUCT_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<bool> Delete(string CacheKey,int id)
        {
            Result<bool> result;
            try
            {
                Product deleteEntity = _productService.Get(w => w.Id == id);
                if (deleteEntity != null)
                {
                    _productService.Delete(deleteEntity);
                    result = new Result<bool>(true, ResultTypeEnum.Success, true, "Product Successfully Deleted With Related Products");
                }
                else
                {
                    result = new Result<bool>(false, ResultTypeEnum.Warning, false, "Product Not Found !");
                }
            }
            catch (Exception ex)
            {

                result = new Result<bool>(false, ResultTypeEnum.Error, $"An Error Occured When Delete Operation Ex : {ex.ToString()}");
            }
            return result;
        }

        public Result<List<ProductDO>> GetAll()
        {
            Result<List<ProductDO>> resultList;
            try
            {
                List<Product> productList = _productService.GetAsQueryable().ToList();
                List<ProductDO> productMappedList = Mapper.Map<List<Product>, List<ProductDO>>(productList);
                resultList = new Result<List<ProductDO>>(productMappedList);
            }
            catch (Exception ex)
            {

                resultList = new Result<List<ProductDO>>(false, ResultTypeEnum.Error, "An error occured when getting Product List! Ex:", ex.Message);
            }
            return resultList;
        }

        public Result<ProductDO> GetById(int id)
        {
            Result<ProductDO> result;
            try
            {
                Product product = _productService.GetAsQueryable(w => w.Id == id).FirstOrDefault();
                ProductDO productDO = Mapper.Map<Product, ProductDO>(product);
                result = new Result<ProductDO>(productDO);
            }
            catch (Exception ex)
            {

                result = new Result<ProductDO>(false, ResultTypeEnum.Error, "An error occured when getting Product ! Ex:", ex.Message);
            }
            return result;
        }

        public Result<List<ProductDO>> GetProductsByCategoryId(int categoryId)
        {
            Result<List<ProductDO>> resultList;
            try
            {
                List<Product> productList = _productService.GetAsQueryable(w => w.CategoryId == categoryId).ToList();
                List<ProductDO> productListDO = Mapper.Map<List<Product>, List<ProductDO>>(productList);
                resultList = new Result<List<ProductDO>>(productListDO);
            }
            catch (Exception ex)
            {

                resultList = new Result<List<ProductDO>>(false, ResultTypeEnum.Error, "An error occured when getting Product List! Ex:", ex.Message);
            }
            return resultList;
        }

        [CacheInvalidate(CacheArea = PRODUCT_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<ProductDO> Update(string CacheKey,ProductDO model)
        {
            Result<ProductDO> result;
            try
            {
                var updateEntity = _productService.Get(w => w.Id == model.Id);
                updateEntity.IsActive = model.IsActive;
                updateEntity.CategoryId = model.CategoryId;
                updateEntity.Description = model.Description;
                updateEntity.ImageUrl = model.ImageUrl;
                updateEntity.Name = model.Name;
                updateEntity.Price = model.Price;
                updateEntity.UpdateTime = DateTime.Now;


                _productService.Update(updateEntity);

                result = new Result<ProductDO>(model);

            }
            catch (Exception ex)
            {

                result = new Result<ProductDO>(false, ResultTypeEnum.Error, "An error occured during the updating product operation! Ex:" + ex.ToString());
            }
            return result;
        }
    }
}
