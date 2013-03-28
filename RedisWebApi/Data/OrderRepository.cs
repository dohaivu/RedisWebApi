using System;
using System.Collections.Generic;
using System.Linq;
using RedisWebApi.Models;
using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace RedisWebApi.Data
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IRedisClient _redisClient;
        public OrderRepository(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public IList<Models.Order> GetCustomerOrders(Guid customerId)
        {
            using (var typedClient = _redisClient.GetTypedClient<Models.Order>())
            {
                var orderIds = _redisClient.GetAllItemsFromSet(RedisKey.GetCustomerOrdersReferenceKey(customerId));
                IList<Models.Order> orders = typedClient.GetByIds(orderIds);
                return orders;
            }
        }

        public IList<Models.Order> StoreAll(Models.Customer customer, IList<Models.Order> orders)
        {
            foreach (var order in orders)
            {
                if (order.Id == default(Guid))
                {
                    order.Id = Guid.NewGuid();
                }
                order.CustomerId = customer.Id;
                if (!customer.Orders.Contains(order.Id))
                {
                    customer.Orders.Add(order.Id);
                }
                
                order.Lines.ForEach(l => _redisClient
                .IncrementItemInSortedSet(RedisKey.BestSellingItems,
                                                                 (string)l.Item, (long)l.Quantity));
            }
            var orderIds = orders.Select(o => o.Id.ToString()).ToList();
            using (var transaction = _redisClient.CreateTransaction())
            {
                transaction.QueueCommand(c => c.Store(customer));
                transaction.QueueCommand(c => c.StoreAll(orders));
                transaction.QueueCommand(c => c.AddRangeToSet(RedisKey.GetCustomerOrdersReferenceKey(customer.Id), orderIds));
                transaction.Commit();
            }

            return orders;
        }

        public Models.Order Store(Models.Customer customer, Models.Order order)
        {
            IList<Models.Order> result = StoreAll(customer, new List<Models.Order>() { order });
            return result.FirstOrDefault();
        }

        public IDictionary<string, double> GetBestSellingItems(int count)
        {
            return _redisClient.GetRangeWithScoresFromSortedSetDesc(RedisKey.BestSellingItems, 0, count - 1);
        }
    }
}