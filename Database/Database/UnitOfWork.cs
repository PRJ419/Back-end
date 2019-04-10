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


        /// <summary>
        /// Returns the current Bar repository associated to the database, if it doesn't exist the property will create a new bar repository
        /// </summary>
        public IBarRepository BarRepository => _barRepository ?? (_barRepository = new BarRepository(_boMContext));

        /// <summary>
        /// Returns the current Bar event repository associated to the database, if it doesn't exist the property will create a new bar event repository
        /// </summary>
        public BarEventRepository BarEventRepository => 
            _barEventRepository ?? (_barEventRepository = new BarEventRepository(_boMContext));

        /// <summary>
        /// Returns the current Bar representative repository associated to the database, if it doesn't exist the property will create a new bar representative repository
        /// </summary>
        public BarRepresentativeRepository BarRepRepository => 
            _barRepresentativeRepository ?? (_barRepresentativeRepository = new BarRepresentativeRepository(_boMContext));

        /// <summary>
        /// Returns the current Coupon repository associated to the database, if it doesn't exist the property will create a new Coupon repository
        /// </summary>
        public CouponRepository CouponRepository => 
            _couponRepository ?? (_couponRepository = new CouponRepository(_boMContext));

        /// <summary>
        /// Returns the current Customer repository associated to the database, if it doesn't exist the property will create a new Customer repository
        /// </summary>
        public CustomerRepository CustomerRepository => 
            _customerRepository ?? (_customerRepository = new CustomerRepository(_boMContext));

        /// <summary>
        /// Returns the current Drink repository associated to the database, if it doesn't exist the property will create a new Drink repository
        /// </summary>
        public DrinkRepository DrinkRepository => 
            _drinkRepository ?? (_drinkRepository = new DrinkRepository(_boMContext));

        /// <summary>
        /// Returns the current  Review repository associated to the database, if it doesn't exist the property will create a new Review repository
        /// </summary>
        public ReviewRepository ReviewRepository => 
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
