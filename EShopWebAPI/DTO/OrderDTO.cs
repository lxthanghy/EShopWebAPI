using EShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopWebAPI.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public OrderDTO() { }
        public List<OrderDetail> Details { get; set; }
    }
}