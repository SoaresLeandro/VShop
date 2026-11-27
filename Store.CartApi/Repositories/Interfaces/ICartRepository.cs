using Store.CartApi.DTOs;

namespace Store.CartApi.Repositories.Interfaces;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserIdAsync(string userId);
    Task<CartDTO> UpdateCartAsync(CartDTO cart);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteItemCartAsync(int cartItemId);
    Task<bool> ApplyCuponAsync(string userId, string cuponCode);
    Task<bool> DeleteCuponAsync(string userId);
}