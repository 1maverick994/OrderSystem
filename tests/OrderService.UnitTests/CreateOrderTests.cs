

using Microsoft.EntityFrameworkCore;
using OrderCommon.Commands;
using OrderService.Commands;

namespace OrderService.UnitTests
{
    [TestClass]
    public class CreateOrderTests
    {
        [TestMethod]
        public async Task CreateOrder()
        {

            // Arrange
            var context = OrderContextFactory.Create();
            var createOrderCmd = new CreateOrderCommand()
            {
                Lines = new[] { new OrderLineDto() { ProductId = 12, Quantity = 5 }, new OrderLineDto() { ProductId = 4, Quantity = 2 } }
            };

            var createOrderCmdHandler = new CreateOrderCommandHandler(context);
            var exisistingOrders = context.Orders.Count();
            var exisistingOrderLines = context.OrderLines.Count();

            // Act
            var newOrderDto = await createOrderCmdHandler.Handle(createOrderCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccessWithData(newOrderDto);

            Assert.AreEqual(createOrderCmd.Lines.Length, newOrderDto.Data.Lines.Length);
            Assert.AreEqual(createOrderCmd.Lines[0].Quantity, newOrderDto.Data.Lines[0].Quantity);
            Assert.AreEqual(createOrderCmd.Lines[0].ProductId, newOrderDto.Data.Lines[0].ProductId);

            Assert.AreEqual(createOrderCmd.Lines[1].Quantity, newOrderDto.Data.Lines[1].Quantity);
            Assert.AreEqual(createOrderCmd.Lines[1].ProductId, newOrderDto.Data.Lines[1].ProductId);

            Assert.AreEqual(context.Orders.Count(), exisistingOrders + 1);
            Assert.AreEqual(context.OrderLines.Count(), exisistingOrderLines + 2);

            var newOrder = context.Orders.Include(o=>o.Lines).FirstOrDefault(o => o.Id == newOrderDto.Data.Id);

            Assert.IsNotNull(newOrder);
            Assert.IsNotNull(newOrder.Lines);

            Assert.AreEqual(createOrderCmd.Lines.Length, newOrder.Lines.Count);
            Assert.AreEqual(createOrderCmd.Lines[0].Quantity, newOrder.Lines.ElementAt(0).Quantity);
            Assert.AreEqual(createOrderCmd.Lines[0].ProductId, newOrder.Lines.ElementAt(0).ProductId);

            Assert.AreEqual(createOrderCmd.Lines[1].Quantity, newOrder.Lines.ElementAt(1).Quantity);
            Assert.AreEqual(createOrderCmd.Lines[1].ProductId, newOrder.Lines.ElementAt(1).ProductId);

        }
    }
}