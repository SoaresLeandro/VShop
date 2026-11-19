using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Web.Models;
using Store.Web.Roles;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var products = await _productService.GetAllProducts();

            if (products is null)
                return View("Error");

            return View(products);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), 
                                                "Id", 
                                                "Name");

            return View();
        }


        [HttpPost]
        [Authorize]
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Update(int id)
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(),
                                                "Id",
                                                "Name");

            var product = await _productService.GetProductById(id);

            if(product == null) return View("Error");

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Update(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var productUpdated = await _productService.UpdateProduct(productViewModel);

                if(productUpdated is not null) return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> Delete(int id)
        {
            var productViewModel = await _productService.GetProductById(id);

            if (productViewModel is null) return View("Error");

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductbyId(id);

            if (result == false) return View("Error");

            return RedirectToAction("Index");
        }
    }
}