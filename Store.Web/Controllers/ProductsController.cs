using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Web.Models;
using Store.Web.Services.Contracts;

namespace Store.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var products = await _productService.GetAllProducts();

            if (products is null)
                return View("Error");

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), 
                                                "Id", 
                                                "Name");

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productCreated = await _productService.CreateProduct(productViewModel);

                if (productCreated is not null)
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(),
                                                "Id",
                                                "Name");
            return View(productViewModel);
        }
    }
}