using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Roles;
using Store.Application.DTOs;
using Store.Application.Services.Interfaces;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();

            if (!productsDto.Any())
                return NotFound("Products not found");

            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var productDTO = await _productService.GetProductById(id);

            if (productDTO is null)
                return NotFound("Product not found");

            return Ok(productDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid Data");

            var ProductDTO = await _productService.AddProduct(productDTO);

            if (productDTO is null)
                return NotFound("Product not created");

            return new CreatedAtRouteResult("GetProduct", new { productDTO });
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> Put(ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest("Invalid Data");

            var productDto = await _productService.UpdateProduct(productDTO);

            if (productDTO is null)
                return NotFound("Product not updated");

            return new CreatedAtRouteResult("GetProduct", new { productDTO });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            await _productService.RemoveProduct(id);

            return Ok();
        }
    }
}