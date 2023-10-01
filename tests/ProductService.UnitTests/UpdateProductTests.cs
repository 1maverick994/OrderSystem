

using Microsoft.EntityFrameworkCore;
using ProductCommon.Commands;
using ProductService.Commands;

namespace ProductService.UnitTests
{
    [TestClass]
    public class UpdateProductTests
    {
        [TestMethod]
        public async Task UpdateProduct()
        {

            // Arrange
            var context = ProductContextFactory.Create();
            var createProductCmd = new CreateProductCommand()
            {
                Name = "Pippo"
            };

            var createProductCmdHandler = new CreateProductCommandHandler(context);
            var updateProductCmdHandler = new UpdateProductCommandHandler(context);
      

            var newProductDto = await createProductCmdHandler.Handle(createProductCmd, CancellationToken.None);

            var exisistingProducts = context.Products.Count();

            // Act
            var updateProductCmd = new UpdateProductCommand()
            {
                Id = newProductDto.Data.Id,
               Name="Pluto"
            };
            var updatedProductDto = await updateProductCmdHandler.Handle(updateProductCmd, CancellationToken.None);

            // Assert
            SharedFunctions.AssertSuccessWithData(updatedProductDto);

            Assert.AreEqual(context.Products.Count(), exisistingProducts);

            Assert.AreEqual(updateProductCmd.Name, updatedProductDto.Data.Name);
                        
            var updatedProduct = context.Products.FirstOrDefault(o => o.Id == newProductDto.Data.Id);

            Assert.IsNotNull(updatedProduct);

            Assert.AreEqual(updateProductCmd.Name, updatedProduct.Name);


        }
    }
}