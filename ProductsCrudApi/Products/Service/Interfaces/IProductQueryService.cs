using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Products.Service.Interfaces
{
    public interface IProductQueryService
    {

        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetByName(string name);
        Task<List<int>> GetIdOrderedByPriceDesc();

    }
}
