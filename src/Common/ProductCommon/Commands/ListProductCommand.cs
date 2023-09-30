using MediatR;
using ProductCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommon.Commands
{
    public class ListProductCommand : IRequest<ProductDto[]>
    {

        public int Count { get; set; }

        public int Page { get; set; }

    }
}
