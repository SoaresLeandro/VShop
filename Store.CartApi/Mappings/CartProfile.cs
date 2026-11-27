using Store.CartApi.DTOs;
using Store.CartApi.Models;

namespace Store.CartApi.Mappings;

public class CartProfile
{
    public static CartDTO MappingCartToCartDTO(Cart cart)
    {
        return new CartDTO
        {
            CartHeader = CartHeaderProfile.MappingCartHeaderToCartHeaderDTO(cart.CartHeader),
            CartItems = cart.CartItems.Select(c => CartItemProfile.MappingCartItemToCartItemDTO(c)),
        };
    }

    public static Cart MappingCartDTOToCart(CartDTO cartDTO)
    {
        return new Cart
        {
            CartHeader = CartHeaderProfile.MappingCartHeaderDTOToCartHeader(cartDTO.CartHeader),
            CartItems = cartDTO.CartItems.Select(c => CartItemProfile.MappingCartItemDTOToCartItem(c))
        };
    }
}