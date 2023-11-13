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
    public class HomeController: Controller
    {
        private readonly ICatalogService catalogService;

        public HomeController(ICatalogService catalogService) { 
            this.catalogService = catalogService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var model = new HomeModel
            {
                FeaturedProducts = catalogService.FindProducts(string.Empty, 0, 8)
            };

            return View(model);
        } 
    }
}
