

using Microsoft.EntityFrameworkCore;
using OrderCommon.Commands;
using OrderService.Commands;

namespace OrderService.UnitTests
{
    [TestClass]
    public class DeleteOrderTests
    {
        [TestMethod]
        public async Task DeleteOrder()
        {

            // Arrange
            var context = OrderContextFactory.Create();
            var createOrderCmd = new CreateOrderCommand()
            {
                Lines = new[] { new OrderLineDto() { ProductId = 12, Quantity = 5 }, new OrderLineDto() { ProductId = 4, Quantity = 2 } }
            };

            var createOrderCmdHandler = new CreateOrderCommandHandler(context);
            var deleteOrderCmdHandler = new DeleteOrderCommandHandler(context);

            var newOrderDto = await createOrderCmdHandler.Handle(createOrderCmd, CancellationToken.None);
            var exisistingOrders = context.Orders.Count();
            var exisistingOrderLines = context.OrderLines.Count();

            // Act
            var deleteOrderCmd = new DeleteOrderCommand() { Id = newOrderDto.Data.Id };
            var result = await deleteOrderCmdHandler.Handle(deleteOrderCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccess(result);            

            var order = context.Orders.FirstOrDefault(o => o.Id == newOrderDto.Data.Id);

            Assert.IsNull(order);

            Assert.AreEqual(context.Orders.Count(), exisistingOrders - 1);
            Assert.AreEqual(context.OrderLines.Count(), exisistingOrderLines - createOrderCmd.Lines.Length);


        }
    }
}