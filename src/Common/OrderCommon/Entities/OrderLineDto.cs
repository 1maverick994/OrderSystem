using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCommon.Entities
{

    /// <summary>
    /// Represent an order line
    /// </summary>
    public class OrderLineDto
    {

        public int ProductId { get; set; }

        public double Quantity { get; set; }
     

    }
}
