using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity
{
    public class Cart: BaseEntity
    {
        public IDictionary<int, CartItem> Items = new Dictionary<int, CartItem>();
    }
}
