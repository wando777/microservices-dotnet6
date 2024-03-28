using AutoMapper;
using GeekShopping.ProductAPI.Data.DTOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(long productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO product)
        {
            Product productEntity = _mapper.Map<Product>(product);
            _context.Products.Update(productEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<bool> DeleteProduct(long productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
