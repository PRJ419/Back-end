using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Redundancy;

namespace Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBarRepository Bars { get; }

        IRepository<BarEvent> BarEventRepository { get; }

        IRepository<Barrepresentative> BarRepRepository { get; }

        IRepository<Coupon> CouponRepository { get; }

        IRepository<Customer> CustomerRepository { get; }

        IRepository<Drink> DrinkRepository { get; }

        IRepository<Review> ReviewRepository { get; }

        int Complete();
    }
}
