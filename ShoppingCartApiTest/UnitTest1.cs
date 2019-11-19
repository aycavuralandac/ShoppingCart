using System;
using Xunit;
using Moq;
using ShoppingCartApi.Services;
using ShoppingCartApi.Models;
using ShoppingCartApi.Repositories;

namespace ShoppingCartApiTest
{
    public class UnitTest1
    {
        
        [Fact]

        public void CartService_AddToCart()
        {
            var mocPro = new Mock<ProductRepository>(); 
            Task<Product> product = new Product();
            mocPro.Setup(p => p.Get("5dcb0b0ad1e4079b69a897d5")).Returns(product);  
            var mocStock = new Mock<StockRepository>(); 
            var mocCamp = new Mock<CampaignRepository>(); 
            var mocCoupon = new Mock<CouponRepository>();
            Cart cart = new Cart();
            CartService cartService = new CartService(mocPro.Object,mocStock.Object,mocCamp.Object,mocCoupon.Object);
            cart = cartService.AddToCart("5dcb0b0ad1e4079b69a897d5",2);

            Assert.NotNull(cart);
        }
    }
}
