using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebApi.Data
{
    public static class RedisKey
    {
        public static string BestSellingItems = "urn:BestSellingItems";
        public static string CustomerOrders = "urn:CustomerOrders";

        public static string GetCustomerOrdersReferenceKey(Guid customerId)
        {
            return String.Format("{0}_{1}", CustomerOrders, customerId.ToString());
        }

    }
}