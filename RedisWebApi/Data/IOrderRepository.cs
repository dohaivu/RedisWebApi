using RedisWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisWebApi.Data
{
    public interface IOrderRepository
    {
        IList<Order> GetCustomerOrders(Guid customerId);
        IList<Order> StoreAll(Customer customer, IList<Order> orders);
        Order Store(Customer customer, Order order);
        IDictionary<string, double> GetBestSellingItems(int count);
    }
}
