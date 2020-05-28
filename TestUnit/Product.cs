using BusinessLayer;
using Entities.MApplication;
using Interfaces.Business;
using Interfaces.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace TestUnit
{
    [TestClass]
    public class Product
    {
        ProductBL productBL;
        private readonly string cacheKey = "Demo-Key";
        [TestInitialize()]
        public void Initialize()
        {
            MapperBL.Initialize();


            productBL = new ProductBL(new CategoryService(), new ProductService());

        }

        [TestMethod]
        public void GetByID()
        {
            var k = productBL.GetById(10);
        }

        [TestMethod]
        public void GetAll()
        {
            var category = productBL.GetAll();
        }

        [TestMethod]
        public void Create()
        {
            var category = productBL.Add(cacheKey, new ProductDO()
            {
                Name = "mocking",
                CategoryId = 22,
                Price = 100,
                CreateUserId = "mocking",
                CreateTime = DateTime.Now,
                UpdateUserId = "mocking",
                UpdateTime = DateTime.Now,
                Description = "mocking",
                ImageUrl= "mocking.jpg",
                IsActive=true,
                CategoryDO = new CategoryDO()
                

            });
        }

        [TestMethod]
        public void Edit()
        {
            var category = productBL.Update(cacheKey, new ProductDO()
            {
                Id=10,
                Name = "mocking",
                CategoryId = 22,
                Price = 100,
                CreateUserId = "mocking",
                CreateTime = DateTime.Now,
                UpdateUserId = "mocking",
                UpdateTime = DateTime.Now,
                Description = "mocking",
                ImageUrl = "mocking.jpg",
                IsActive = true,
            });
        }

        [TestMethod]
        public void Delete()
        {
            var category = productBL.Delete(cacheKey, 10);
        }

    }
}
