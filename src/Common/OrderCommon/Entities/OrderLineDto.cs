﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCommon.Entities
{
    public class OrderLineDto
    {

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public bool Equals(OrderLineDto? other)
        {
            return (other != null && other.Quantity == Quantity && other.ProductId == ProductId);
        }

    }
}
