

using Microsoft.EntityFrameworkCore;
using ProductCommon.Commands;
using ProductService.Commands;

namespace ProductService.UnitTests
{
    [TestClass]
    public class ListProductTests
    {
        [TestMethod]
        public async Task ListProduct()
        {

            // Arrange
            var context = await ProductContextFactory.CreateEmpty();
            var createProductCmd = new CreateProductCommand()
            {
                Name = "Pippo"
            };

            var createProductCmdHandler = new CreateProductCommandHandler(context);
            var newProductDto = await createProductCmdHandler.Handle(createProductCmd, CancellationToken.None);

            var listProductCmd = new ListProductCommand() { Count = 10, Page = 0 };
            var listProductCmdHandler = new ListProductCommandHandler(context);            
                        
            // Act
            var ProductsDto = await listProductCmdHandler.Handle(listProductCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccessWithData(newProductDto);

            Assert.AreEqual(ProductsDto.Data.Length, 1);
            Assert.AreEqual(ProductsDto.Data[0].Name, createProductCmd.Name);
            


        }
    }
}