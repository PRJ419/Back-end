using System;
using Database.Interfaces;
using Database.Redundancy;
using Database.Repository_Implementations;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarOMeterContext _boMContext = new BarOMeterContext();
        private IRepository<BarEvent> _barEventRepository;
        private IRepository<Barrepresentative> _barrepresentativeRepository;
        private IRepository<Coupon> _couponRepository;
        private IRepository<Customer> _customerRepository;
        private IRepository<Drink> _drinkRepository;
        private IRepository<Review> _reviewRepository;
        private IBarRepository _barRepository;


        // The following properties check if the repository for the entities already exist. If they don't, then it instantiates a new repository
        // with the same context so we always work on the same context between the repositories.
        // 
        // This code is inspired by microsofts "Implementing the Repository and Unit of Work Patterns" page
        public IBarRepository Bars => _barRepository ?? (_barRepository = new BarRepository(_boMContext));

        public IRepository<BarEvent> BarEventRepository => _barEventRepository ?? (_barEventRepository = new Repository<BarEvent>(_boMContext));

        public IRepository<Barrepresentative> BarRepRepository => 
            _barrepresentativeRepository ?? (_barrepresentativeRepository = new Repository<Barrepresentative>(_boMContext));

        public IRepository<Coupon> CouponRepository => _couponRepository ?? (_couponRepository = new Repository<Coupon>(_boMContext));

        public IRepository<Customer> CustomerRepository => _customerRepository ?? (_customerRepository = new Repository<Customer>(_boMContext));

        public IRepository<Drink> DrinkRepository => _drinkRepository ?? (_drinkRepository = new Repository<Drink>(_boMContext));

        public IRepository<Review> ReviewRepository => _reviewRepository ?? (_reviewRepository = new Repository<Review>(_boMContext));


        // Saves the changes made to the uow
        public int Complete()
        {
            return _boMContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if(disposing)
                {
                    _boMContext.Dispose();
                }
            }
            disposing = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
