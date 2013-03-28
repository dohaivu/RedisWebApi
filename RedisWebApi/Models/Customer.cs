using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebApi.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Guid> Orders { get; set; }
        public Address Address { get; set; }
    }
}