using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> Get();
        Task<Coupon> Get(string id);
        Coupon Create(Coupon coupon);
        Task<bool> Update(string id, Coupon coupon);
        Task<bool> Remove(Coupon coupon);
        Task<bool> Remove(string id);
        Task<bool> Remove();
        Task<string> CreateIndex();
    }
}