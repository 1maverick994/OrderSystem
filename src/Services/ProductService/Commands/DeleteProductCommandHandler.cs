using MediatR;
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
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        ProductContext _context;

        public DeleteProductCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
           

            
            var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id == request.Id, cancellationToken);

            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                throw new Exception("Unable to find product with id: " + request.Id);
            }
            
        }
    }
}
