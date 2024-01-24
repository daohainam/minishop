using MiniShop.Adapter;
using MiniShop.Models;
using MiniShop.UseCase;
using MiniWebServer.Mvc.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Controllers
{
    public class CartController: Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [Route("/Cart/Add")]
        public IActionResult CartAdd(int productId)
        {
            var cart = cartService.GetCartById(this.Session.Id);
            cart.Add(productId, 1);
            cartService.SaveCart(this.Session.Id, cart);

            return Json(cart);
        }
    }
}
