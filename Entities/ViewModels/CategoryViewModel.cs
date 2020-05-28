using Entities.MApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
   public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            CategoryDO = new CategoryDO();
            CategoryList = new List<CategoryDO>();
        }
        public CategoryDO CategoryDO { get; set; }
        public List<CategoryDO> CategoryList { get; set; }
    }
}
