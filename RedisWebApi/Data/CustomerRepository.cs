using RedisWebApi.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebApi.Data
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly IRedisClient _redisClient;

        public CustomerRepository(IRedisClient redisClient)
        {
            this._redisClient = redisClient;
        }

        public IList<Models.Customer> GetAll()
        {
            using (var typedClient = _redisClient.GetTypedClient<Customer>())
            {
                return typedClient.GetAll();
            }
        }

        public Models.Customer Get(Guid id)
        {
            using (var typedClient = _redisClient.GetTypedClient<Customer>())
            {
                return typedClient.GetById(id);
            }
        }

        public Models.Customer Store(Models.Customer customer)
        {
            using (var typedClient = _redisClient.GetTypedClient<Customer>())
            {
                if (customer.Id == default(Guid))
                {
                    customer.Id = Guid.NewGuid();
                }
                
                return typedClient.Store(customer);                
            }
        }
    }
}