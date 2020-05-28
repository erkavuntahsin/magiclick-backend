using Core.ResultType;
using Entities.MApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Business
{
    public interface ICategoryBL
    {
        Result<List<CategoryDO>> GetAll();

        Result<CategoryDO> GetById(int id);

        Result<List<CategoryDO>> GetSubCategoriesById(int id);

        Result<CategoryDO> GetParentCategoryById(int id);

        Result<CategoryDO> Update(string CacheKey,CategoryDO model);

        Result<bool> Delete(string CacheKey,int id);

        Result<CategoryDO> Add(string CacheKey,CategoryDO model);

    }
}
