using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ProductService
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }


        public ProductContext() 
        {
            
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("ProductDb");
    }

    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
