

using Microsoft.EntityFrameworkCore;
using OrderCommon.Commands;
using OrderService.Commands;

namespace OrderService.UnitTests
{
    [TestClass]
    public class ListOrderTests
    {
        [TestMethod]
        public async Task ListOrder()
        {

            // Arrange
            var context = await OrderContextFactory.CreateEmpty();
            var createOrderCmd = new CreateOrderCommand()
            {
                Lines = new[] { new OrderLineDto() { ProductId = 12, Quantity = 5 }, new OrderLineDto() { ProductId = 4, Quantity = 2 } }
            };

            var createOrderCmdHandler = new CreateOrderCommandHandler(context);
            var newOrderDto = await createOrderCmdHandler.Handle(createOrderCmd, CancellationToken.None);

            var listOrderCmd = new ListOrderCommand() { Count = 10, Page = 0 };
            var listOrderCmdHandler = new ListOrderCommandHandler(context);            
                        
            // Act
            var ordersDto = await listOrderCmdHandler.Handle(listOrderCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccessWithData(newOrderDto);

            Assert.AreEqual(ordersDto.Data.Length, 1);
            Assert.AreEqual(ordersDto.Data[0].Lines.Length, createOrderCmd.Lines.Length);
            Assert.AreEqual(createOrderCmd.Lines[0].Quantity, ordersDto.Data[0].Lines[0].Quantity);
            Assert.AreEqual(createOrderCmd.Lines[0].ProductId, ordersDto.Data[0].Lines[0].ProductId);

            Assert.AreEqual(createOrderCmd.Lines[1].Quantity, ordersDto.Data[0].Lines[1].Quantity);
            Assert.AreEqual(createOrderCmd.Lines[1].ProductId, ordersDto.Data[0].Lines[1].ProductId);


        }
    }
}