using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.UnitTests
{
    internal class ProductContextFactory
    {

        public static ProductContext Create()
        {
            var ProductContext = new ProductContext();

            return ProductContext;
        }

        public static async Task<ProductContext> CreateEmpty()
        {

            var ProductContext = Create();

            await ProductContext.Products.ForEachAsync(o => ProductContext.Remove(o));

            ProductContext.SaveChanges();

            return ProductContext;
        }

    }
}
