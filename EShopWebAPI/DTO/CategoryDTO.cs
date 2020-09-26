using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopWebAPI.DTO
{
    public class CategoryDTO
    {
        private int categoryID;
        private string categoryName;
        private string description;
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string Description { get => description; set => description = value; }
        public CategoryDTO() { }
        public CategoryDTO(int categoryID, string categoryName, string description)
        {
            this.CategoryID = categoryID;
            this.CategoryName = categoryName;
            this.Description = description;
        }
    }
}