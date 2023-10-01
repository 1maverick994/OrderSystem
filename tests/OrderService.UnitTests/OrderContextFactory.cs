using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.UnitTests
{
    internal class OrderContextFactory
    {

        public static OrderContext Create()
        {
            var orderContext = new OrderContext();

            return orderContext;
        }

        public static async Task<OrderContext> CreateEmpty()
        {

            var orderContext = Create();

            await orderContext.OrderLines.ForEachAsync(o => orderContext.Remove(o));
            await orderContext.Orders.ForEachAsync(o => orderContext.Remove(o));

            orderContext.SaveChanges();

            return orderContext;
        }

    }
}
