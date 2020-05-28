using BusinessLayer;
using Entities.MApplication;
using Interfaces.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUnit
{
    [TestClass]
    public class Category
    {
        CategoryBL categoryBL;
        private readonly string cacheKey = "Demo-Key";
        [TestInitialize()]
        public void Initialize()
        {
            MapperBL.Initialize();


            categoryBL = new CategoryBL(new CategoryService(), new ProductService());

        }

        [TestMethod]
        public void GetByID()
        {
            var k = categoryBL.GetById(10);
        }

        [TestMethod]
        public void GetAll()
        {
            var category = categoryBL.GetAll();
        }

        [TestMethod]
        public void Create()
        {
            var category = categoryBL.Add(cacheKey, new CategoryDO()
            {
                CategoryName = "mocking",
                ParentCategoryId = 0,
                CategoryCode = "mocking",
                CreateUserId = "mocking",
                CreateTime = DateTime.Now,
                UpdateUserId = "mocking",
                UpdateTime = DateTime.Now,
                Description = "mocking",
                IsActive = true,
                DisplayOrder = 10
            });
        }

        [TestMethod]
        public void Edit()
        {
            var category = categoryBL.Update(cacheKey, new CategoryDO()
            {
                Id = 10,
                CategoryName = "mocking",
                ParentCategoryId = 0,
                CategoryCode = "mocking",
                CreateUserId = "mocking",
                CreateTime = DateTime.Now,
                UpdateUserId = "mocking",
                UpdateTime = DateTime.Now,
                Description = "mocking",
                IsActive = true,
                DisplayOrder = 10
            });
        }

        [TestMethod]
        public void Delete()
        {
            var category = categoryBL.Delete(cacheKey, 10);
        }
    }
}
