using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.MApplication
{
    public class CategoryDO
    {
        public CategoryDO()
        {
            ProductDO = new List<ProductDO>();
        }

        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public List<ProductDO> ProductDO { get; set; }
    }
}
