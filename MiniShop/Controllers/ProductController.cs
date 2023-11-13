using MiniShop.Models;
using MiniShop.UseCase;
using MiniWebServer.Mvc.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebServer.Controllers
{
    public class ProductController: Controller
    {
        private readonly ICatalogService catalogService;

        public ProductController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }
        
        [Route("/p")]
        public IActionResult ProductDetails(int id)
        {
            var product = catalogService.GetProduct(id);
            if (product == null)
            {
                return Ok("Not Found"); // we should return a 404 NotFound or redirect to a soft Not Found page, but Mini-Web-Server hasn't supported it yet
            }
            else
            {
                return View(new ProductDetailsModel() { 
                    Product = product,
                    RelatedProducts = catalogService.FindProducts(string.Empty, 0, 4).Items
                });
            }
        } 
    }
}
