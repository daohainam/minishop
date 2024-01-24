using MiniShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.UseCase
{
    public interface ICartService
    {
        Cart GetCartById(string id);
        void SaveCart(string id, Cart cart);
    }
}
