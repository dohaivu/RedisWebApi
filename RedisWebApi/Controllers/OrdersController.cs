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
    public class OrdersController : ApiController
    {
        public ICustomerRepository CustomerRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }

        public HttpResponseMessage Post([FromBody] Order order)
        {
            var customer = CustomerRepository.Get(order.CustomerId);
            var result = OrderRepository.Store(customer, order);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [ActionName("customer")]
        [HttpGet]
        public IList<Order> GetCustomerOrders(Guid id)
        {
            return OrderRepository.GetCustomerOrders(id);
        }
    }
}
