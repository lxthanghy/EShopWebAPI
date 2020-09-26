using EShopWebAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EShopWebAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private EShopEntities eShop;

        [HttpPost]
        //Add Customer
        public HttpResponseMessage AddCustomer([FromBody] Customer cus)
        {
            try
            {
                eShop = new EShopEntities();
                eShop.Customers.Add(cus);
                eShop.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, eShop.Customers);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        // Get All Customer
        public HttpResponseMessage GetAllCustomers()
        {
            try
            {
                eShop = new EShopEntities();
                return Request.CreateResponse(HttpStatusCode.OK, eShop.Customers);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        //Get customers by address
        public HttpResponseMessage GetCustomerByAddress(string address)
        {
            try
            {
                eShop = new EShopEntities();
                IQueryable<Customer> result = eShop.Customers.Where(cus => cus.Address.ToLower().IndexOf(address.ToLower()) >= 0);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        // Get Customer By ID
        public HttpResponseMessage GetCustomerByID(int id)
        {
            try
            {
                Customer customer = FinCustomer(id);
                return customer != null ? Request.CreateResponse(HttpStatusCode.Found, customer) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        //Update customer
        public HttpResponseMessage EditCustomer(int id, [FromBody] Customer cus)
        {
            try
            {
                eShop = new EShopEntities();
                Customer customer = FinCustomer(id);
                if (customer != null)
                {
                    customer.Fullname = cus.Fullname;
                    customer.Gender = cus.Gender;
                    customer.Birthday = cus.Birthday;
                    customer.Address = cus.Address;
                    customer.Email = cus.Email;
                    customer.PhoneNumber = cus.PhoneNumber;
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, eShop.Customers);
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
        // Delete customer
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                eShop = new EShopEntities();
                Customer customer = FinCustomer(id);
                if (customer != null)
                {
                    eShop.Customers.Remove(customer);
                    eShop.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, eShop.Customers);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Find customer
        private Customer FinCustomer(int id)
        {
            try
            {
                eShop = new EShopEntities();
                return eShop.Customers.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
