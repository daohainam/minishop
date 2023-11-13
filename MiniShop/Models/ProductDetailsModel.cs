using MiniShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Models
{
    public class ProductDetailsModel
    {
        public required Product Product { get; set; }
        public required IEnumerable<Product> RelatedProducts { get; set; }
    }
}
