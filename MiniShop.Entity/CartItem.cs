using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity
{
    public class CartItem: BaseEntity
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
