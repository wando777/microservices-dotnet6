using GeekShopping.ProductAPI.Data.DTOs;

namespace GeekShopping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(long productId);
        Task<ProductDTO> CreateProduct(ProductDTO product);
        Task<ProductDTO> UpdateProduct(ProductDTO product);
        Task<bool> DeleteProduct(long productId);
    }
}
