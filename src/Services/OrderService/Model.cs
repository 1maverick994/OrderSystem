using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace OrderService
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }


        public OrderContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var order1 = new Order() { CreationDate = new DateTime(2022, 01, 02), Id = 1 };
            order1.Lines = new OrderLine[] {
                    new OrderLine() { Id = 1, IdProduct = 2, Order = order1, Quantity = 10 } ,
                    new OrderLine() { Id = 2, IdProduct = 3, Order = order1, Quantity = 3 }
            };

            var order2 = new Order() { CreationDate = new DateTime(2023, 07, 29), Id = 2 };
            order1.Lines = new OrderLine[] {
                    new OrderLine() { Id = 3, IdProduct = 1, Order = order2, Quantity =1 } ,
                    new OrderLine() { Id = 4, IdProduct = 2, Order = order2, Quantity = 1 }
            };


            modelBuilder.Entity<Order>().HasData(order1, order2);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("OrderDb");
        }

    }

    public class Order
    {
        public int Id { get; set; }

        public required DateTime CreationDate { get; set; }

        public ICollection<OrderLine>? Lines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }

        public int IdProduct { get; set; }

        public double Quantity { get; set; }

        public Order? Order { get; set; }
    }
}
