using System.Collections.Generic;

namespace ShoppingCartApi.Models
{
    public class Cart
    {
        public List<Item> items {get;set;} = new List<Item>();

        public double amount {get;set;} = 0.0;


    }
}