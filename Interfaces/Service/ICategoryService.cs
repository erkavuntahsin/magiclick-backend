using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Interfaces.Service
{
    public interface ICategoryService
    {
        Category Get(Expression<Func<Category, bool>> predicate = null);

        List<Category> GetAsQueryable(Expression<Func<Category, bool>> predicate = null);

        void Add(Category entity);

        void Update(Category entity);

        void Delete(Category entity);

        bool Exist(Expression<Func<Category, bool>> predicate);

        void DeleteMultiple(List<Category> entityList);
    }
}
