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
    public class CategoryBL : ICategoryBL
    {
        public const string CATEGORY_CACHE_AREA = "CATEGORY_AREA.{";
        public const string CATEGORY_CACHE_KEY = "CATEGORY";

        ICategoryService _categoryService;
        IProductService _productService;
        public CategoryBL(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [CacheInvalidate(CacheArea = CATEGORY_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<CategoryDO> Add(string CacheKey,CategoryDO model)
        {
            Result<CategoryDO> result;
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                Category entity = Mapper.Map<CategoryDO, Category>(model);
                _categoryService.Add(entity);
                model.Id = entity.Id;
                result = new Result<CategoryDO>(true, ResultTypeEnum.Success, model, "Category Was Successfully Saved");
            }
            catch (Exception ex)
            {

                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, $"An Error Occured When Adding Operation Ex : {ex.ToString()}");
            }
            return result;
        }

        [CacheInvalidate(CacheArea = CATEGORY_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<bool> Delete(string CacheKey,int id)
        {
            Result<bool> result;
            try
            {
                Category deleteEntity = _categoryService.Get(w => w.Id == id);
                if (deleteEntity != null)
                {
                    List<Product> deleteProdsList = _productService.GetAsQueryable(w => w.CategoryId == id);
                    List<Category> deleteSubCategoriesList = _categoryService.GetAsQueryable(w => w.ParentCategoryId == id);
                    _productService.DeleteMultiple(deleteProdsList);
                    _categoryService.DeleteMultiple(deleteSubCategoriesList);
                    _categoryService.Delete(deleteEntity);
                    result = new Result<bool>(true, ResultTypeEnum.Success, true, "Category Successfully Deleted With Related Products and Related Categories");
                }
                else
                {
                    result = new Result<bool>(false, ResultTypeEnum.Warning, false, "Category Not Found !");
                }
            }
            catch (Exception ex)
            {

                result = new Result<bool>(false, ResultTypeEnum.Error, $"An Error Occured When Delete Operation Ex : {ex.ToString()}");
            }
            return result;
        }

        public Result<List<CategoryDO>> GetAll()
        {
            Result<List<CategoryDO>> resultList;
            try
            {
                List<Category> categoryList = _categoryService.GetAsQueryable().ToList();
                List<CategoryDO> categoryMappedList = Mapper.Map<List<Category>,List<CategoryDO>>(categoryList);
                resultList = new Result<List<CategoryDO>>(categoryMappedList);
            }
            catch (Exception ex)
            {

                resultList = new Result<List<CategoryDO>>(false, ResultTypeEnum.Error, "An error occured when getting Category List! Ex:", ex.Message);
            }
            return resultList;
        }

        public Result<CategoryDO> GetById(int id)
        {
            Result<CategoryDO> result;
            try
            {
                Category category = _categoryService.GetAsQueryable(w => w.Id == id).FirstOrDefault();
                CategoryDO categoryDO = Mapper.Map<Category, CategoryDO>(category);
                result = new Result<CategoryDO>(categoryDO);
            }
            catch (Exception ex)
            {

                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, "An error occured when getting Category ! Ex:", ex.Message);
            }
            return result;
        }

        public Result<CategoryDO> GetParentCategoryById(int id)
        {
            Result<CategoryDO> result;
            try
            {
                Category parentCategory = _categoryService.GetAsQueryable(w => w.ParentCategoryId == id).FirstOrDefault();
                CategoryDO parentCategoryDO = Mapper.Map<Category, CategoryDO>(parentCategory);
                result = new Result<CategoryDO>(parentCategoryDO);

            }
            catch (Exception ex)
            {

                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, "An error occured when getting Parent Category! Ex:", ex.Message);
            }
            return result;
        }

        public Result<List<CategoryDO>> GetSubCategoriesById(int id)
        {
            Result<List<CategoryDO>> resultList;
            try
            {
                List<Category> subCategoryList = _categoryService.GetAsQueryable(w => w.ParentCategoryId == id).ToList();
                List<CategoryDO> subCategoryListDO = Mapper.Map<List<Category>, List<CategoryDO>>(subCategoryList);
                resultList = new Result<List<CategoryDO>>(subCategoryListDO);
            }
            catch (Exception ex)
            {

                resultList = new Result<List<CategoryDO>>(false, ResultTypeEnum.Error, "An error occured when getting Sub Category List! Ex:", ex.Message);
            }
            return resultList;
        }

        [CacheInvalidate(CacheArea = CATEGORY_CACHE_AREA, CacheSetting = CacheInvalidateSettingsEnum.UsePropertyWithCacheName, PropertyName = "CacheKey")]
        public Result<CategoryDO> Update(string CacheKey,CategoryDO model)
        {
            Result<CategoryDO> result;
            try
            {
                var updateEntity = _categoryService.Get(w => w.Id == model.Id);
                updateEntity.IsActive = model.IsActive;
                updateEntity.ParentCategoryId = model.ParentCategoryId;
                updateEntity.UpdateTime = DateTime.Now;
                updateEntity.CategoryCode = model.CategoryCode;
                updateEntity.Description = model.Description;
                updateEntity.DisplayOrder = model.DisplayOrder;
                updateEntity.CategoryName = model.CategoryName;

                _categoryService.Update(updateEntity);

                result = new Result<CategoryDO>(model);

            }
            catch (Exception ex)
            {

                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, "An error occured during the updating category operation! Ex:" + ex.ToString());
            }
            return result;
        }
    }
}
