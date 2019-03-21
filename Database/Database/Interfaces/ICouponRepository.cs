namespace Database.Interfaces
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Coupon Get(Coupon coupon);
        void Add(Coupon coupon);
        void Delete(Coupon coupon);
        void Edit(Coupon coupon);
    }
}