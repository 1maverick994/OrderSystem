

using Microsoft.EntityFrameworkCore;
using ProductCommon.Commands;
using ProductService.Commands;

namespace ProductService.UnitTests
{
    [TestClass]
    public class CreateProductTests
    {
        [TestMethod]
        public async Task CreateProduct()
        {

            // Arrange
            var context = ProductContextFactory.Create();
            var createProductCmd = new CreateProductCommand()
            {
                Name = "Pippo"                
            };

            var createProductCmdHandler = new CreateProductCommandHandler(context);
            var exisistingProducts = context.Products.Count();

            // Act
            var newProductDto = await createProductCmdHandler.Handle(createProductCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccessWithData(newProductDto);

            Assert.AreEqual(createProductCmd.Name, newProductDto.Data.Name);    

            Assert.AreEqual(context.Products.Count(), exisistingProducts + 1);

            var newProduct = context.Products.FirstOrDefault(o => o.Id == newProductDto.Data.Id);

            Assert.IsNotNull(newProduct);

            Assert.AreEqual(createProductCmd.Name, newProduct.Name);
            

        }
    }
}