

using Microsoft.EntityFrameworkCore;
using OrderCommon.Commands;
using OrderService.Commands;

namespace OrderService.UnitTests
{
    [TestClass]
    public class UpdateOrderTests
    {
        [TestMethod]
        public async Task UpdateOrder()
        {

            // Arrange
            var context = OrderContextFactory.Create();
            var createOrderCmd = new CreateOrderCommand()
            {
                Lines = new[] { new OrderLineDto() { ProductId = 12, Quantity = 5 }, new OrderLineDto() { ProductId = 4, Quantity = 2 } }
            };

            var createOrderCmdHandler = new CreateOrderCommandHandler(context);
            var updateOrderCmdHandler = new UpdateOrderCommandHandler(context);
      

            var newOrderDto = await createOrderCmdHandler.Handle(createOrderCmd, CancellationToken.None);

            var exisistingOrders = context.Orders.Count();
            var exisistingOrderLines = context.OrderLines.Count();

            // Act
            var updateOrderCmd = new UpdateOrderCommand()
            {
                Id = newOrderDto.Data.Id,
                Lines = new[] { new OrderLineDto() { ProductId = 1, Quantity = 1 } }
            };
            var updatedOrderDto = await updateOrderCmdHandler.Handle(updateOrderCmd, CancellationToken.None);

            // Assert
            SharedFunctions.AssertSuccessWithData(updatedOrderDto);

            Assert.AreEqual(context.Orders.Count(), exisistingOrders);
            Assert.AreEqual(context.OrderLines.Count(), exisistingOrderLines - 1);

            Assert.AreEqual(updateOrderCmd.Lines.Length, updatedOrderDto.Data.Lines.Length);
            Assert.AreEqual(updateOrderCmd.Lines[0].Quantity, updatedOrderDto.Data.Lines[0].Quantity);
            Assert.AreEqual(updateOrderCmd.Lines[0].ProductId, updatedOrderDto.Data.Lines[0].ProductId);
            
            var updatedOrder = context.Orders.Include(o=>o.Lines).FirstOrDefault(o => o.Id == newOrderDto.Data.Id);

            Assert.IsNotNull(updatedOrder);
            Assert.IsNotNull(updatedOrder.Lines);

            Assert.AreEqual(updateOrderCmd.Lines.Length, updatedOrder.Lines.Count);
            Assert.AreEqual(updateOrderCmd.Lines[0].Quantity, updatedOrder.Lines.ElementAt(0).Quantity);
            Assert.AreEqual(updateOrderCmd.Lines[0].ProductId, updatedOrder.Lines.ElementAt(0).ProductId);


        }
    }
}