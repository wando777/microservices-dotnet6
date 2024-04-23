using GeekShopping.Web.Services;
using GeekShopping.Web.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService =
                productService ?? throw new ArgumentNullException(nameof(_productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }
    }
}
