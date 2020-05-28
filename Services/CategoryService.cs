using DataAccessLayer.Models;
using Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        public void Add(Category entity)
        {
          using(var _context  = new MAppDataContext())
            {
                var addEntity = _context.Entry(entity);
                addEntity.State = EntityState.Added;
                _context.SaveChanges();
            }
        }

        public void Delete(Category entity)
        {
            using(var _context = new MAppDataContext())
            {
                var deleteEntity = _context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public bool Exist(Expression<Func<Category, bool>> predicate)
        {
            using (var _context = new MAppDataContext())
            {
                return _context.Set<Category>().Any(predicate);
            }
        }

        public Category Get(Expression<Func<Category, bool>> predicate = null)
        {
           using (var _context = new MAppDataContext())
            {
                return _context.Set<Category>().Include(s=>s.Product).FirstOrDefault(predicate);
            }
        }

        public List<Category> GetAsQueryable(Expression<Func<Category, bool>> predicate = null)
        {
            using (var _context = new MAppDataContext())
            {
                if (predicate == null)
                    return _context.Set<Category>().Include(s => s.Product).ToList();
                else
                    return _context.Set<Category>().Include(s => s.Product).Where(predicate).ToList();
            }
        }

        public void Update(Category entity)
        {
            using (var _context = new MAppDataContext())
            {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteMultiple(List<Category> entityList)
        {
            using (var _context = new MAppDataContext())
            {
                _context.Set<Category>().RemoveRange(entityList);
                _context.SaveChanges();
            }
        }
    }
}
