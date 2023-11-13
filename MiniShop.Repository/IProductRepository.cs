using MiniShop.Entity;
using MiniShop.UseCase;

namespace MiniShop.Repository
{
    public interface IProductRepository
    {
        CatalogPage<Product> FindProducts(string name, int page, int pageSize, int? categoryId, int? vendorId);
        Product? GetProduct(int productId);
        bool AddProduct(Product product);
    }
}