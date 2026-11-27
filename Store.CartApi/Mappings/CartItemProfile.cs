using Store.CartApi.DTOs;
using Store.CartApi.Models;

namespace Store.CartApi.Mappings;

public class CartItemProfile
{
    public static CartItemDTO MappingCartItemToCartItemDTO(CartItem cartItem)
    {
        return new CartItemDTO
        {
            Id = cartItem.Id,
            CartHeaderId = cartItem.CartHeaderId,
            CartHeader = CartHeaderProfile.MappingCartHeaderToCartHeaderDTO(cartItem.CartHeader),
            ProductId = cartItem.ProductId,
            Product = ProductProfile.MappingProductToProductDTO(cartItem.Product),
            Quantity = cartItem.Quantity
        };
    }

    public static CartItem MappingCartItemDTOToCartItem(CartItemDTO cartItemDTO)
    {
        return new CartItem
        {
            Id = cartItemDTO.Id,
            CartHeaderId = cartItemDTO.CartHeaderId,
            CartHeader = CartHeaderProfile.MappingCartHeaderDTOToCartHeader(cartItemDTO.CartHeader),
            ProductId = cartItemDTO.ProductId,
            Product = ProductProfile.MappingProductDTOToProduct(cartItemDTO.Product),
            Quantity = cartItemDTO.Quantity
        };
    }
}