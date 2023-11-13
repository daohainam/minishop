using MiniShop.Entity;

namespace MiniShop.UseCase
{
    public interface ICatalogService
    {
        CatalogPage<Product> FindProducts(string name, int page, int pageSize, int? categoryId = null, int? vendorId = null);
        Product? GetProduct(int productId);
    }
}