using Entities.MApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            CategoryList = new List<CategoryDO>();
            ProductDO = new ProductDO();
        }
        public ProductDO ProductDO { get; set; }
        public List<CategoryDO> CategoryList { get; set; }
    }
}
