using System;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ShoppingCartApi.Models;
using ShoppingCartApi.Models.Request;
using ShoppingCartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _cartService.AddIHttpContextAccessor(httpContextAccessor);
        }
       
        // GET api/cart/id
        // example: http://localhost:5000/api/cart/
        [HttpGet]
        public ActionResult<Cart> ShowCart()
        {
            return _cartService.ShowCart();
        }

        // POST api/cart/id
        // example: http://localhost:5000/api/cart
        [HttpPost]
        [Route("AddToCart")]
        public Cart AddToCart([FromBody] RequestAddToCart requestAddToCart)
        {
            return _cartService.AddToCart(requestAddToCart.id, requestAddToCart.quantity);
        }

        
        [HttpDelete]
        public Cart RemoveFromChart(string id)
        {
            return _cartService.RemoveFromChart(id);
        }

        // POST api/cart/id
        // example: http://localhost:5000/api/cart/49902812
        [HttpPost("{id}")]
        public Cart ApplyCoupon(string id)
        {
            return _cartService.ApplyCoupon(id);
        }
    }
}