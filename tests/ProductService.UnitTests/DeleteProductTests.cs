

using Microsoft.EntityFrameworkCore;
using ProductCommon.Commands;
using ProductService.Commands;

namespace ProductService.UnitTests
{
    [TestClass]
    public class DeleteProductTests
    {
        [TestMethod]
        public async Task DeleteProduct()
        {

            // Arrange
            var context = ProductContextFactory.Create();
            var createProductCmd = new CreateProductCommand()
            {
                Name = "Pippo",
            };

            var createProductCmdHandler = new CreateProductCommandHandler(context);
            var deleteProductCmdHandler = new DeleteProductCommandHandler(context);

            var newProductDto = await createProductCmdHandler.Handle(createProductCmd, CancellationToken.None);
            var exisistingProducts = context.Products.Count();

            // Act
            var deleteProductCmd = new DeleteProductCommand() { Id = newProductDto.Data.Id };
            var result = await deleteProductCmdHandler.Handle(deleteProductCmd, CancellationToken.None);


            // Assert
            SharedFunctions.AssertSuccess(result);            

            var Product = context.Products.FirstOrDefault(o => o.Id == newProductDto.Data.Id);

            Assert.IsNull(Product);

            Assert.AreEqual(context.Products.Count(), exisistingProducts - 1);


        }
    }
}