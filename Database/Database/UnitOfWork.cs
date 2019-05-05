using System;
using System.Linq;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarOMeterContext _boMContext;
        private IBarEventRepository _barEventRepository;
        private IBarRepresentativeRepository _barRepresentativeRepository;
        private ICouponRepository _couponRepository;
        private ICustomerRepository _customerRepository;
        private IDrinkRepository _drinkRepository;
        private IReviewRepository _reviewRepository;
        private IBarRepository _barRepository;


        /// <summary>
        /// Used for creating an instance of UnitOfWork with standard BarOMeterContext connectionstring.
        /// </summary>
        public UnitOfWork()
        {
            _boMContext = new BarOMeterContext();
            _boMContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Used for creating an instance of UnitOfWork with certain options for the BarOMeterContext.
        /// Primarily used for testing the database with an inMemory database.
        /// </summary>
        /// <param name="options">
        /// Takes an DbContextOptions of BarOMeterContext as the parameter. This could for example be
        /// an inMemory database.
        /// </param>
        public UnitOfWork(DbContextOptions<BarOMeterContext> options)
        {
            _boMContext = new BarOMeterContext(options);
            _boMContext.Database.EnsureCreated();
        }

        // The following properties check if the repository for the entities already exist. If they don't, then it instantiates a new repository
        // with the same context so we always work on the same context between the repositories.
        // 
        // This code is inspired by the "Implementing the Repository and Unit of Work Patterns" page by Microsoft


        /// <summary>
        /// Returns the current Bar repository associated to the database, if it doesn't exist the property will create a new bar repository
        /// </summary>
        public IBarRepository BarRepository => _barRepository ?? (_barRepository = new BarRepository(_boMContext));

        /// <summary>
        /// Returns the current Bar event repository associated to the database, if it doesn't exist the property will create a new bar event repository
        /// </summary>
        public IBarEventRepository BarEventRepository => 
            _barEventRepository ?? (_barEventRepository = new BarEventRepository(_boMContext));

        /// <summary>
        /// Returns the current Bar representative repository associated to the database, if it doesn't exist the property will create a new bar representative repository
        /// </summary>
        public IBarRepresentativeRepository BarRepRepository => 
            _barRepresentativeRepository ?? (_barRepresentativeRepository = new BarRepresentativeRepository(_boMContext));

        /// <summary>
        /// Returns the current Coupon repository associated to the database, if it doesn't exist the property will create a new Coupon repository
        /// </summary>
        public ICouponRepository CouponRepository => 
            _couponRepository ?? (_couponRepository = new CouponRepository(_boMContext));

        /// <summary>
        /// Returns the current Customer repository associated to the database, if it doesn't exist the property will create a new Customer repository
        /// </summary>
        public ICustomerRepository CustomerRepository => 
            _customerRepository ?? (_customerRepository = new CustomerRepository(_boMContext));

        /// <summary>
        /// Returns the current Drink repository associated to the database, if it doesn't exist the property will create a new Drink repository
        /// </summary>
        public IDrinkRepository DrinkRepository => 
            _drinkRepository ?? (_drinkRepository = new DrinkRepository(_boMContext));

        /// <summary>
        /// Returns the current  Review repository associated to the database, if it doesn't exist the property will create a new Review repository
        /// </summary>
        public IReviewRepository ReviewRepository => 
            _reviewRepository ?? (_reviewRepository = new ReviewRepository(_boMContext));


        /// <summary>
        /// Updates the current rating of a given bar and calculates the average rating of said bar
        /// </summary>
        /// <param name="barID">
        /// Id of the chosen bar, in this case the name of the bar
        /// </param>
        public void UpdateBarRating(string barID)
        {
            var updatedRating = ReviewRepository
                .GetAll()
                .Where(review => review.BarName == barID)
                .Average(review => review.BarPressure);

            var bar = BarRepository.Get(barID);
            bar.AvgRating = updatedRating;
        }

        /// <summary>
        /// Used in conjunction with changes made to the database, is called to save the changes made
        /// </summary>
        /// <returns>
        /// Returns the number of changes made to the database
        /// </returns>
        public int Complete()
        {
            return _boMContext.SaveChanges();
        }

        private bool disposed = false;


        /// <summary>
        /// Is used to dispose of the current database context and connection
        /// </summary>
        /// <param name="disposing">
        /// Takes a bool, if it's true the context will be disposed
        /// </param>
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

        /// <summary>
        /// Is used to dispose the current database context and connection
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
