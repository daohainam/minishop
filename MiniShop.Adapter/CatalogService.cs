using MiniShop.Entity;
using MiniShop.Repository;
using MiniShop.UseCase;

namespace MiniShop.Adapter
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository productRepository;

        public CatalogService(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public CatalogPage<Product> FindProducts(string name, int page, int pageSize, int? categoryId, int? vendorId)
        {
            return productRepository.FindProducts(name, page, pageSize, categoryId, vendorId);
        }

        public Product? GetProduct(int productId)
        {
            return productRepository?.GetProduct(productId);
        }
    }
}