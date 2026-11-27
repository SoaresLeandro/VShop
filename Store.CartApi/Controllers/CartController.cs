using Microsoft.AspNetCore.Mvc;
using Store.CartApi.DTOs;
using Store.CartApi.Repositories.Interfaces;

namespace Store.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("getcart/{userId}")]
        public async Task<ActionResult<CartDTO>> GetByUserId(string userId)
        {
            CartDTO cartDTO = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cartDTO is null) return NotFound();

            return Ok(cartDTO);
        }

        [HttpPost("addcart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO) 
        {
            CartDTO cartDTOAdd = await _cartRepository.UpdateCartAsync(cartDTO);

            if (cartDTOAdd is null) return BadRequest();

            return Ok(cartDTOAdd);
        }

        [HttpPut("updatecart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            CartDTO cartDTOAdd = await _cartRepository.UpdateCartAsync(cartDTO);

            if (cartDTOAdd is null) return BadRequest();

            return Ok(cartDTOAdd);
        }

        [HttpPost("deletecart/{id}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            bool removed = await _cartRepository.DeleteItemCartAsync(id);

            if (!removed) return BadRequest();

            return Ok();
        }
    }
}
