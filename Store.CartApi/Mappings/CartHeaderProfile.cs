using Store.CartApi.DTOs;
using Store.CartApi.Models;

namespace Store.CartApi.Mappings;

public class CartHeaderProfile
{
    public static CartHeaderDTO MappingCartHeaderToCartHeaderDTO(CartHeader cartHeader)
    {
        return new CartHeaderDTO
        {
            Id = cartHeader.Id,
            UserId = cartHeader.UserId,
            CuponCode = cartHeader.CuponCode
        };
    }

    public static CartHeader MappingCartHeaderDTOToCartHeader(CartHeaderDTO cartHeaderDTO)
    {
        return new CartHeader
        {
            Id = cartHeaderDTO.Id,
            UserId = cartHeaderDTO.UserId,
            CuponCode = cartHeaderDTO.CuponCode
        };
    }
}