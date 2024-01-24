using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Entity
{
    public class Cart: BaseEntity
    {
        private IDictionary<int, CartItem> items = new Dictionary<int, CartItem>();

        public void Add(int productId, int quantity)
        {
            if (items.TryGetValue(productId, out var item)) { 
                item.Quantity += quantity;
            }
            else
            {
                items.Add(productId, new CartItem()
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
        }

        public IEnumerable<CartItem> Items { 
            get { 
                return items.Values;
            } 
        }
    }
}
