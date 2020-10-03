using EShopWebAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EShopWebAPI.Controllers
{
    public class ProductController : ApiController
    {

        private EShopEntities eShop;
        // GET: api/Product
        public HttpResponseMessage Get()
        {
            try
            {
                eShop = new EShopEntities();
                return Request.CreateResponse(HttpStatusCode.OK, eShop.Products);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Product/5
        public HttpResponseMessage Get(int id)
        {

            try
            {
                Product product = FindProduct(id);
                return product != null ? Request.CreateResponse(HttpStatusCode.OK, product) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Product
        public HttpResponseMessage AddProduct([FromBody] Product product)
        {
            try
            {
                eShop = new EShopEntities();
                eShop.Products.Add(product);
                eShop.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, eShop.Products);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody] Product pro)
        {
            try
            {
                eShop = new EShopEntities();
                Product product = FindProduct(id);
                if(product != null)
                {
                    product.ProductName = pro.ProductName;
                    product.UnitPrice = pro.UnitPrice;
                    product.Quantity = pro.Quantity;
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, eShop.Products);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Product product = FindProduct(id);
                if (product != null)
                {
                    eShop.Products.Remove(product);
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, eShop.Products);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        private Product FindProduct(int productID)
        {
            try
            {
                eShop = new EShopEntities();
                Product product = eShop.Products.Find(productID);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
