using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsCrudApi.Data;
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

       

    }
}
