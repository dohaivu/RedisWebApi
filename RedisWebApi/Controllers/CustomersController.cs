using RedisWebApi.Data;
using RedisWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedisWebApi.Controllers
{
    public class CustomersController : ApiController
    {
        public ICustomerRepository CustomerRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }

        public IQueryable<Customer> GetAll()
        {
            return CustomerRepository.GetAll().AsQueryable();
        }

        public Customer Get(Guid id)
        {
            var customer = CustomerRepository.Get(id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            var result = CustomerRepository.Store(customer);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        public HttpResponseMessage Put(Guid id, [FromBody] Customer customer)
        {
            var existingEntity = CustomerRepository.Get(id);
            if (existingEntity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customer.Id = id;
            CustomerRepository.Store(customer);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
