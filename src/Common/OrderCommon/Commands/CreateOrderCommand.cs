﻿
using OrderCommon.Entities;
using ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCommon.Commands
{

    /// <summary>
    /// Create a new order
    /// </summary>
    public class CreateOrderCommand : IRequest<ServiceResult<OrderDto>>
    {
        public required OrderLineDto[] Lines { get; set; }

    }
}
