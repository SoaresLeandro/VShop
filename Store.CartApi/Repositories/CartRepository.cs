using Microsoft.EntityFrameworkCore;
using Store.CartApi.Context;
using Store.CartApi.DTOs;
using Store.CartApi.Mappings;
using Store.CartApi.Models;
using Store.CartApi.Repositories.Interfaces;
using System.Reflection.PortableExecutable;

namespace Store.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _appDbContext;

    public CartRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        Cart cart = new Cart
        {
            CartHeader = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartItems = await _appDbContext.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
                                                      .Include(c => c.Product)
                                                      .ToListAsync();

        return CartProfile.MappingCartToCartDTO(cart);
    }

    public async Task<CartDTO> UpdateCartAsync(CartDTO cartDto)
    {
        Cart cart = CartProfile.MappingCartDTOToCart(cartDto);

        //Se o produto não existir, salvar na base de dados
        await SaveProductInDataBase(cartDto, cart);

        //Verifica se o CartHeader é null
        CartHeader cartHeader = await _appDbContext.CartHeaders.AsNoTracking()
                                                               .FirstOrDefaultAsync(c => 
                                    c.UserId == cart.CartHeader.UserId);

        if(cartHeader is null)
        {
            //Cria o header e os itens
            await CreateHeaderWithItems(cart);
        }
        else
        {
            await UpdateQuantityAndItems(cartDto, cart, cartHeader);
        }

        return CartProfile.MappingCartToCartDTO(cart);
    }

    private async Task UpdateQuantityAndItems(CartDTO cartDto, Cart cart, CartHeader cartHeader)
    {
        //Se CartHeader não é null, verifica se CartItems possuem o mesmo produto
        var cartDetail = await _appDbContext.CartItems.AsNoTracking()
                                                      .FirstOrDefaultAsync(p =>
                                                      p.ProductId == cartDto.CartItems.FirstOrDefault().ProductId
                                                      && p.CartHeaderId == cartHeader.Id);

        if(cartDetail is null)
        {
            //Cria o CartItems
            cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
            cart.CartItems.FirstOrDefault().Product = null;
            _appDbContext.CartItems.Add(cart.CartItems.FirstOrDefault());
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            //Atualiza a quantidade e o CartItems
            cart.CartItems.FirstOrDefault().Product = null;
            cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
            cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
            cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
            
            _appDbContext.CartItems.Update(cart.CartItems.FirstOrDefault());
            await _appDbContext.SaveChangesAsync();
        }
    }

    private async Task CreateHeaderWithItems(Cart cart)
    {
        _appDbContext.CartHeaders.Add(cart.CartHeader);

        await _appDbContext.SaveChangesAsync();

        cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
        cart.CartItems.FirstOrDefault().Product = null;

        _appDbContext.CartItems.Add(cart.CartItems.FirstOrDefault());

        await _appDbContext.SaveChangesAsync();
    }

    private async Task SaveProductInDataBase(CartDTO cartDto, Cart cart)
    {
        Product product = await _appDbContext.Products.FirstOrDefaultAsync(p =>
            p.Id == cartDto.CartItems.FirstOrDefault().ProductId);

        if(product is null)
        {
            _appDbContext.Products.Add(cart.CartItems.FirstOrDefault().Product);

            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> CleanCartAsync(string userId)
    {
        try
        {
            CartHeader cartHeader = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => 
                c.UserId == userId);

            //List<CartItem> cartItems = await _appDbContext.CartItems.Where(c => c.CartHeaderId == cartHeader.Id)
            //                                             .ToListAsync();

            //foreach (CartItem cartItem in cartItems)
            //    _appDbContext.CartItems.Remove(cartItem);

            _appDbContext.CartItems.RemoveRange(_appDbContext.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));
            

            _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteItemCartAsync(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _appDbContext.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem is null) return false;

            int total = _appDbContext.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId)
                                                     .Count();

            _appDbContext.CartItems.Remove(cartItem);

            if (total == 1)
            {
                CartHeader cartHeaderRemove = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c =>
                    c.Id == cartItem.CartHeaderId);

                _appDbContext.CartHeaders.Remove(cartHeaderRemove);
            }

            _appDbContext.SaveChangesAsync();

            return true;
        } catch (Exception ex) 
        {
            return false;
        }
    }

    public Task<bool> ApplyCuponAsync(string userId, string cuponCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCuponAsync(string userId)
    {
        throw new NotImplementedException();
    }
}