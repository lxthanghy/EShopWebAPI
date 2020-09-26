using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopWebAPI.DTO
{
    public class ProductDTO
    {
        private int productID;
        private string productName;
        private int categoryID;
        private int unitPrice;
        private int quantity;
        public int ProductID { get => productID; set => productID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public int UnitPrice { get => unitPrice; set => unitPrice = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public ProductDTO() { }
        public ProductDTO(int productID, string productName, int categoryID, int
        unitPrice, int quantity)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.CategoryID = categoryID;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
        }
    }
}