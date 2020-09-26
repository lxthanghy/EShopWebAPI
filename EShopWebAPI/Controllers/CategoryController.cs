using EShopWebAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EShopWebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private EShopEntities eShop;
        // GET: api/Category
        public HttpResponseMessage Get()
        {
            try
            {
                eShop = new EShopEntities();
                return Request.CreateResponse(HttpStatusCode.OK, eShop.Categories);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // GET: api/Category/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Category cate = FindCategory(id);
                return cate != null ? Request.CreateResponse(HttpStatusCode.Found, cate) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // POST: api/Category
        public HttpResponseMessage Post([FromBody] Category category)
        {
            try
            {
                eShop = new EShopEntities();
                eShop.Categories.Add(category);
                eShop.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, eShop.Categories);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // PUT: api/Category/5
        public HttpResponseMessage Put(int id, [FromBody] Category category)
        {
            try
            {
                eShop = new EShopEntities();
                Category cate = FindCategory(id);
                if (cate != null)
                {
                    cate.CategoryName = category.CategoryName;
                    cate.Description = category.Description;
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, eShop.Categories);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // DELETE: api/Category/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Category cate = FindCategory(id);
                if (cate != null)
                {
                    eShop.Categories.Remove(cate);
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, eShop.Categories);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        private Category FindCategory(int categoryID)
        {
            try
            {
                eShop = new EShopEntities();
                Category cate = eShop.Categories.Find(categoryID);
                return cate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
