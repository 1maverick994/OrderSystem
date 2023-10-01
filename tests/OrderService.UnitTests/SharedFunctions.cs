using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.UnitTests
{
    internal static class SharedFunctions
    {

        internal static void AssertSuccessWithData<T>(ServiceCommon.ServiceResult<T> result)
        {
            AssertSuccess(result);            
            Assert.IsNotNull(result.Data);
        }

        internal static void AssertSuccess(ServiceCommon.ServiceResult result)
        {
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }

        internal static void AssertEqualOrderLine(OrderLineDto orderLineDto, OrderLine orderLine)
        {
            Assert.AreEqual(orderLineDto.Quantity, orderLine.Quantity);
            Assert.AreEqual(orderLineDto.ProductId, orderLine.ProductId);
          
        }

    }
}
