namespace Database.Interfaces
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Coupon Get(string coupon);
        void Add(Coupon coupon);
        void Delete(string coupon);
        void Edit(Coupon coupon);
    }
}