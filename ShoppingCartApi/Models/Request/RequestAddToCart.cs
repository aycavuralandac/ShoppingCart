using System;
using System.Collections.Generic;

namespace ShoppingCartApi.Models.Request
{
    public class RequestAddToCart
    {
        public string id {get;set;}
        public int quantity {get;set;}
    }
}