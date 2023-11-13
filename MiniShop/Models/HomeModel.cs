using MiniShop.Entity;
using MiniShop.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Models
{
    public class HomeModel
    {
        public required CatalogPage<Product> FeaturedProducts { get; set; }
    }
}
