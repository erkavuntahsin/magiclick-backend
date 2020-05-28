using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Interfaces.Service
{
    public interface IProductService
    {
        Product Get(Expression<Func<Product, bool>> predicate = null);

        List<Product> GetAsQueryable(Expression<Func<Product, bool>> predicate = null);

        void Add(Product entity);

        void Update(Product entity);

        void Delete(Product entity);

        bool Exist(Expression<Func<Product, bool>> predicate);

        void DeleteMultiple(List<Product> entityList);
    }
}
