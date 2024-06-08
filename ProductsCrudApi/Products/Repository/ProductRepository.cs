using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsCrudApi.Data;
using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;

namespace ProductsCrudApi.Products.Repository
{
    public class ProductRepository:IProductRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(obj => obj.Name.Equals(name));  
        }

        public async Task<List<int>> GetIdOrderedByPriceDescAsync()
        {
            var products=await _context.Products
                .OrderByDescending(obj => obj.Price)
                .Select(obj => obj.Id)
                .ToListAsync();

            return products;
        }

        public async Task<Product> CreateProduct(CreateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(int id, UpdateProductRequest request)
        {

            var product = await _context.Products.FindAsync(id);

            product.Price= request.Price ?? product.Price;
            product.Stock=request.Stock ?? product.Stock;
            product.Producer=request.Producer ?? product.Producer;

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Product> GetByPriceAsync(int price)
        {
            return await _context.Products.FirstOrDefaultAsync(obj => obj.Price.Equals(price));
        }

        public async Task<Product> GetByStokAsync(int stock)
        {
            return await _context.Products.FirstOrDefaultAsync(obj => obj.Stock.Equals(stock));
        }
    }
}
