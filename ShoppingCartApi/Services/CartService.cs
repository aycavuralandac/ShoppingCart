using System.Threading.Tasks;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using ShoppingCartApi.Models;
using ShoppingCartApi.Repositories;
using ShoppingCartApi.Infrastructure;
using ShoppingCartApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static DicsountConstants;


namespace ShoppingCartApi.Services
{
    public class CartService : ICartService
    {
        private HttpContext context;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICouponRepository _couponRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public CartService(IProductRepository productRepository, IStockRepository stockRepository
                        , ICampaignRepository campaignRepository, ICouponRepository couponRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _campaignRepository = campaignRepository;
            _couponRepository = couponRepository;
        }

        public void AddIHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            context = _httpContextAccessor.HttpContext;
        }
        public Cart ShowCart()
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(context.Session, "cart");
            return cart;
        }

        public Cart AddToCart(string id, int quantity)
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(context.Session, "cart");
            if (cart == null)
            {
                cart = new Cart();
            }
            if (doWeHaveTheItem(id, quantity))
            {
                var item = cart.items.Where(i => i.Product.Id == id).FirstOrDefault();
                if (item != null)
                {
                    item.Quantity += quantity;
                }
                else
                {
                    cart.items.Add(new Item { Product = _productRepository.Get(id).Result, Quantity = quantity });
                }
                SessionHelper.SetObjectAsJson(context.Session, "cart", cart);
            }
            return cart;
        }

        public Cart ApplyCoupon(string id)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(context.Session, "cart");
            var coupon = _couponRepository.Get(id).Result;
            if (coupon != null)
            {
                if (cart.amount >= coupon.MinAmountForDiscount)
                {
                    cart.amount = coupon.DiscountType == DiscountType.RATE ? cart.amount - (cart.amount * coupon.AmountOrRate)
                                                           : cart.amount - coupon.AmountOrRate;
                }
            }
            SessionHelper.SetObjectAsJson(context.Session, "cart", cart);
            return cart;
        }

        public void ApplyDiscount(Cart cart)
        {
            var sum = 0.0;
            if (cart != null && cart.items != null)
            {
                foreach (var item in cart.items)
                {
                    sum += GetDiscountedAmount(item);
                }
                cart.amount = sum;
            }
            SessionHelper.SetObjectAsJson(context.Session, "cart", cart);
        }

        private double GetDiscountedAmount(Item item)
        {
            var rate_discount_amount = 0.0;
            var amount_discount_amount = 0.0;
            var rate_campaign = _campaignRepository.GetCampaignByCategory(item.Product.CategoryId, DiscountType.RATE).Result;
            if (rate_campaign != null)
            {
                rate_discount_amount = item.Product.Price * item.Quantity;
            }
            rate_discount_amount = rate_discount_amount - rate_discount_amount * rate_campaign.AmountOrRate;

            var amount_campaign = _campaignRepository.GetCampaignByCategory(item.Product.CategoryId, DiscountType.AMOUNT).Result;
            if (amount_campaign != null)
            {
                amount_discount_amount = item.Product.Price * item.Quantity;
            }
            amount_discount_amount = amount_discount_amount - rate_campaign.AmountOrRate;

            return amount_discount_amount > rate_discount_amount ? amount_discount_amount : rate_discount_amount;
        }

        public Cart RemoveFromChart(string id)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(context.Session, "cart");
            int index = isExistInCart(id);
            cart.items.RemoveAt(index);
            SessionHelper.SetObjectAsJson(context.Session, "cart", cart);
            return cart;
        }

        private int isExistInCart(string id)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(context.Session, "cart");
            for (int i = 0; i < cart.items.Count; i++)
            {
                if (cart.items[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool doWeHaveTheItem(string id, int requestedQuantity)
        {
            var stock = _stockRepository.Get(id).Result;
            if (stock == null || stock.Quantity < requestedQuantity)
            {
                return false;
            }
            else
            {
                {
                    return true;
                }
            }
        }
    }
}