using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Products.Service.Interfaces
{
    public interface IProductQueryService
    {

        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetByName(string name);
        Task<Product> GetByPrice(int price);
        Task<Product> GetByStock(int stock);
        Task<Product> GetById(int id);
        Task<List<int>> GetIdOrderedByPriceDesc();

    }
}
