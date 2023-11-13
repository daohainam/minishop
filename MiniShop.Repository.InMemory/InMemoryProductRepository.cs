using MiniShop.Entity;
using MiniShop.UseCase;

namespace MiniShop.Repository.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> products = new();

        public bool AddProduct(Product product)
        {
            if (products.Where(p => p.Id == product.Id).Any()) return false;

            products.Add(product);

            return true;
        }

        public CatalogPage<Product> FindProducts(string name, int page, int pageSize, int? categoryId, int? vendorId)
        {
            var products = from p in this.products select p;
            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }
            if (vendorId != null)
            {
                products = products.Where(p => p.VendorId == vendorId);
            }

            int start = page * pageSize;
            if (start < 0)
            {
                start = 0;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            int total = products.Count();
            int pageCount = (total / pageSize) + (total % pageSize > 0 ? 1 : 0);

            products = products.Skip(start).Take(pageSize);

            return new CatalogPage<Product>() { 
                Items = products,
                Count = products.Count(),
                Page = page,
                PageSize = pageSize,
                PageCount = pageCount
            };
        }

        public Product? GetProduct(int productId)
        {
            return products.Where(p => p.Id == productId).FirstOrDefault();
        }
    }
}