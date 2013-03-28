using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebApi.Models
{
    public class OrderLine
    {
        public string Item { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}