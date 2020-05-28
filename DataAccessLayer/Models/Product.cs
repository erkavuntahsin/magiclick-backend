using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public Category Category { get; set; }
    }
}
