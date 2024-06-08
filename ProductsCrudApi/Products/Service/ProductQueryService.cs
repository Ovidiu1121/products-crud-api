using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;
using ProductsCrudApi.Products.Service.Interfaces;
using ProductsCrudApi.System.Constant;
using ProductsCrudApi.System.Exceptions;

namespace ProductsCrudApi.Products.Service
{
    public class ProductQueryService:IProductQueryService
    {
        private IProductRepository _repository;

        public ProductQueryService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = await _repository.GetAllAsync();

            if (products.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_PRODUCTS_EXIST);
            }

            return products;
        }

        public async Task<Product> GetById(int id)
        {
            Product product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            return product;
        }

        public async Task<Product> GetByName(string name)
        {
            
            Product product= await _repository.GetByNameAsync(name);

            if(product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            return product;
        }

        public async Task<Product> GetByPrice(int price)
        {
            Product product = await _repository.GetByPriceAsync(price);

            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            return product;
        }

        public async Task<Product> GetByStock(int stock)
        {
            Product product = await _repository.GetByStokAsync(stock);

            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            return product;
        }

        public async Task<List<int>> GetIdOrderedByPriceDesc()
        {
          
            List<int> ids= await _repository.GetIdOrderedByPriceDescAsync();

            if (ids.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_PRODUCTS_EXIST);
            }

            return ids;

        }
    }
}
