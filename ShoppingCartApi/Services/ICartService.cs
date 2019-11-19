using ShoppingCartApi.Models;
using Microsoft.AspNetCore.Http;

namespace ShoppingCartApi.Services
{
    public interface ICartService
    {
        Cart ShowCart();
        Cart AddToCart(string id, int quantity);
        Cart RemoveFromChart(string id);
        Cart ApplyCoupon(string id);

        void AddIHttpContextAccessor(IHttpContextAccessor _httpContextAccessor);
    }
}
 