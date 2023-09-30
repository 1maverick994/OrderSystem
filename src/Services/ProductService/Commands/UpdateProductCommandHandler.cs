﻿using MediatR;
using Microsoft.EntityFrameworkCore;
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
    internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        ProductContext _context;

        public UpdateProductCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           

            
            var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id == request.Id, cancellationToken);

            if(product != null)
            {
                product.Name = request.Name;
                await _context.SaveChangesAsync();

                return new ProductDto()
                {
                    Name = product.Name,
                    Id = product.Id,
                };
            }
            else
            {
                throw new Exception("Unable to find product with id: " + request.Id);
            }
            
        }
    }
}
