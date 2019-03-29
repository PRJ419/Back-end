﻿using System;
using Database.Interfaces;
using Database.Redundancy;
using Database.Repository_Implementations;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarOMeterContext _boMContext = new BarOMeterContext();
        private BarEventRepository _barEventRepository;
        private BarrepresentativeRepository _barrepresentativeRepository;
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

        public BarrepresentativeRepository BarRepRepository => 
            _barrepresentativeRepository ?? (_barrepresentativeRepository = new BarrepresentativeRepository(_boMContext));

        public CouponRepository CouponRepository => 
            _couponRepository ?? (_couponRepository = new CouponRepository(_boMContext));

        public CustomerRepository CustomerRepository => 
            _customerRepository ?? (_customerRepository = new CustomerRepository(_boMContext));

        public DrinkRepository DrinkRepository => 
            _drinkRepository ?? (_drinkRepository = new DrinkRepository(_boMContext));

        public ReviewRepository ReviewRepository => 
            _reviewRepository ?? (_reviewRepository = new ReviewRepository(_boMContext));



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
