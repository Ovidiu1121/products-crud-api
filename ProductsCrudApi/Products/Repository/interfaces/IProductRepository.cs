using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Products.Repository.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByNameAsync(string name);
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByPriceAsync(int price);
        Task<Product> GetByStokAsync(int stock);
        Task<List<int>> GetIdOrderedByPriceDescAsync();
        Task<Product> CreateProduct(CreateProductRequest request);
        Task<Product> UpdateProduct(int id, UpdateProductRequest request);
        Task<Product> DeleteProductById(int id);

    }
}
