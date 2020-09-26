using EShopWebAPI.DTO;
using EShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EShopWebAPI.Controllers
{
    public class OrderController : ApiController
    {
        private EShopEntities eShop;

        [HttpGet]
        public HttpResponseMessage GetAllOrders()
        {
            try
            {
                eShop = new EShopEntities();
                return Request.CreateResponse(HttpStatusCode.OK, eShop.Orders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetOrderByID(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Order order = FindOrder(id);
                OrderDTO orderDTO = new OrderDTO();
                orderDTO.OrderID = order.OrderID;
                orderDTO.CustomerID = order.CustomerID;
                orderDTO.OrderDate = order.OrderDate;
                orderDTO.ShipAddress = order.ShipAddress;
                orderDTO.Details = getDetails(id);
                return Request.CreateResponse(HttpStatusCode.OK, orderDTO);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                eShop = new EShopEntities();
                Order order = new Order();
                order.OrderID = orderDTO.OrderID;
                order.CustomerID = orderDTO.CustomerID;
                order.OrderDate = orderDTO.OrderDate;
                order.ShipAddress = orderDTO.ShipAddress;
                eShop.Orders.Add(order);
                eShop.SaveChanges();
                foreach (OrderDetail od in orderDTO.Details)
                {
                    OrderDetail item = new OrderDetail();
                    item.OrderID = orderDTO.OrderID;
                    item.ProductID = od.ProductID;
                    item.Quantity = od.Quantity;
                    item.Discount = od.Discount;
                    eShop.OrderDetails.Add(item);
                    eShop.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.Created, eShop.Orders);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditOrder(int id, [FromBody] OrderDTO orderDTO)
        {
            try
            {
                eShop = new EShopEntities();
                Order order = FindOrder(id);
                if (order != null)
                {
                    order.OrderDate = orderDTO.OrderDate;
                    order.ShipAddress = orderDTO.ShipAddress;
                    eShop.SaveChanges();
                    foreach (OrderDetail od in orderDTO.Details)
                    {
                        OrderDetail item = FindOrderDeatail(order.OrderID, od.ProductID);
                        item.Quantity = od.Quantity;
                        item.Discount = od.Discount;
                        eShop.SaveChanges();
                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted, eShop.Orders);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteOrder(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Order order = FindOrder(id);
                if (order != null)
                {
                    eShop.Orders.Remove(order);
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, eShop.Orders);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private Order FindOrder(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Order order = eShop.Orders.Find(id);
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private OrderDetail FindOrderDeatail(int orderID, int productID)
        {
            try
            {
                eShop = new EShopEntities();
                return eShop.OrderDetails.SingleOrDefault(dt => dt.OrderID == orderID && dt.ProductID == productID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<OrderDetail> getDetails(int idOrder)
        {
            try
            {
                eShop = new EShopEntities();
                return eShop.OrderDetails.Where(dt => dt.OrderID == idOrder).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
