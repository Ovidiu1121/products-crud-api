using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Products.Repository.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        //Task<Product> GetByNameAsync(string name);


    }
}
