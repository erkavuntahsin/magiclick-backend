using Core.ResultType;
using Entities.MApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Business
{
    public interface IProductBL
    {
        Result<List<ProductDO>> GetAll();

        Result<ProductDO> GetById(int id);

        Result<List<ProductDO>> GetProductsByCategoryId(int categoryId);

        Result<ProductDO> Update(string CacheKey,ProductDO model);

        Result<bool> Delete(string CacheKey,int id);

        Result<ProductDO> Add(string CacheKey,ProductDO model);
    }
}
