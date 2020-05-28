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
   public class ProductService: IProductService
    {
        public void Add(Product entity)
        {
            using (var _context = new MAppDataContext())
            {
                var addEntity = _context.Entry(entity);
                addEntity.State = EntityState.Added;
                _context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (var _context = new MAppDataContext())
            {
                var deleteEntity = _context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public bool Exist(Expression<Func<Product, bool>> predicate)
        {
            using (var _context = new MAppDataContext())
            {
                return _context.Set<Product>().Any(predicate);
            }
        }

        public Product Get(Expression<Func<Product, bool>> predicate = null)
        {
            using (var _context = new MAppDataContext())
            {
                return _context.Set<Product>().Include(s => s.Category).FirstOrDefault(predicate);
            }
        }

        public List<Product> GetAsQueryable(Expression<Func<Product, bool>> predicate = null)
        {
            using (var _context = new MAppDataContext())
            {
                if (predicate == null)
                    return _context.Set<Product>().Include(s => s.Category).ToList();
                else
                    return _context.Set<Product>().Include(s => s.Category).Where(predicate).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (var _context = new MAppDataContext())
            {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public void DeleteMultiple(List<Product> entityList)
        {
            using (var _context = new MAppDataContext())
            {
                _context.Set<Product>().RemoveRange(entityList);
                _context.SaveChanges();
            }
        }
    }
}
