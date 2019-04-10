using System;
using System.Linq;
using Database.Interfaces;
using Database.Repository_Implementations;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarOMeterContext _boMContext = new BarOMeterContext();
        private BarEventRepository _barEventRepository;
        private BarRepresentativeRepository _barRepresentativeRepository;
        private CouponRepository _couponRepository;
        private CustomerRepository _customerRepository;
        private DrinkRepository _drinkRepository;
        private ReviewRepository _reviewRepository;
        private IBarRepository _barRepository;


        // The following properties check if the repository for the entities already exist. If they don't, then it instantiates a new repository
        // with the same context so we always work on the same context between the repositories.
        // 
        // This code is inspired by microsofts "Implementing the Repository and Unit of Work Patterns" page
        public IBarRepository BarRepository => _barRepository ?? (_barRepository = new BarRepository(_boMContext));

        public BarEventRepository BarEventRepository => 
            _barEventRepository ?? (_barEventRepository = new BarEventRepository(_boMContext));

        public BarRepresentativeRepository BarRepRepository => 
            _barRepresentativeRepository ?? (_barRepresentativeRepository = new BarRepresentativeRepository(_boMContext));

        public CouponRepository CouponRepository => 
            _couponRepository ?? (_couponRepository = new CouponRepository(_boMContext));

        public CustomerRepository CustomerRepository => 
            _customerRepository ?? (_customerRepository = new CustomerRepository(_boMContext));

        public DrinkRepository DrinkRepository => 
            _drinkRepository ?? (_drinkRepository = new DrinkRepository(_boMContext));

        public ReviewRepository ReviewRepository => 
            _reviewRepository ?? (_reviewRepository = new ReviewRepository(_boMContext));

        public void UpdateBarPressure(string barID)
        {
            var updatedRating = ReviewRepository
                .GetAll()
                .Where(review => review.BarName == barID)
                .Average(review => review.BarPressure);

            var bar = BarRepository.Get(barID);
            bar.AvgRating = updatedRating;
        }

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
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
