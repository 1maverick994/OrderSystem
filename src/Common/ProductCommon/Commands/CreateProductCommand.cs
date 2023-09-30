﻿using MediatR;
using ProductCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommon.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public required string Name { get; set; }

    }
}
