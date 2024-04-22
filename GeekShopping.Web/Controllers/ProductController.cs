using GeekShopping.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            _productService =
                _productService ?? throw new ArgumentNullException(nameof(_productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }
    }
}
