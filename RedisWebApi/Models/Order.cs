﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public IList<OrderLine> Lines { get; set; }
    }
}