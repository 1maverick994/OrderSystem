using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCommon.Commands;
using ProductCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Commands
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        ProductContext _context;

        public CreateProductCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {


            var product = new Product() { Name = request.Name };
            await _context.Products.AddAsync(product, cancellationToken);

            await _context.SaveChangesAsync();

            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name
            };

        }
    }
}
