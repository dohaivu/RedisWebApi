using RedisWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisWebApi.Data
{
    public interface ICustomerRepository
    {
        IList<Customer> GetAll();
        Customer Get(Guid id);
        Customer Store(Customer customer);
    }
}
