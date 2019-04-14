using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class DrinksRepositoryTest
    {
        private DrinkRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void Edit_Entity()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditDrink")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new DrinkRepository(_context);


            _repository.Add(new Drink()
            {
                BarName = "TestBar",
                DrinksName = "TestDrink",
                Price = 100
            });
            _context.SaveChanges();

            Drink newDrink = new Drink()
            {
                BarName = "TestBar",
                DrinksName = "TestDrink",
                Price = 600
            };

            _repository.Edit(newDrink);
            _context.SaveChanges();
            
            Assert.AreEqual(600, _repository.Get("TestBar","TestDrink").Price);

        }
    }
}
